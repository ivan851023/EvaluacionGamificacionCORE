using EvaluacionGamificacionCORE.DAL.Context;
using EvaluacionGamificacionCORE.DAL.Entity;
using EvaluacionGamificacionCORE.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE.Controllers
{
    public class AutenticacionController : Controller
    {
                
        private IConfiguration _configuration;
        private EvaluacionContext Context { get; }

        public AutenticacionController(IConfiguration configuration, IUsuario iusuario)
        {
            _usuario = iusuario;
            _configuration = configuration;
        }

        private readonly IUsuario _usuario;

        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                string encrypPass = ComputeSha256Hash(model.Password);
                var usuario = _usuario.GetUsuarios().Where(x => x.Documento == model.Documento && x.Password == encrypPass).FirstOrDefault();

                if (usuario != null)
                {
                    var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(usuario.Id)),
                        new Claim(ClaimTypes.Name, usuario.Nombre_Completo),
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = true
                    });

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMensaje = "Documento o Password incorrectos";
                    return View();
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Registro(UsuarioModel model)
        {
            DateTime dtFechaNacimiento;

            try
            {
                var user = _usuario.GetUsuarios().Where(x => x.Documento == model.Documento).FirstOrDefault();
                if (user != null)
                {
                    return Json(new { success = false, responseText = "El usuario ya se encuentra registrado" });
                }
                
                dtFechaNacimiento = DateTime.Parse(model.Fecha_Nacimiento, CultureInfo.InvariantCulture);

                Usuario tablaUsuario = new Usuario();
                tablaUsuario.Documento = model.Documento;
                tablaUsuario.Nombre_Completo = model.Nombre_Completo;
                tablaUsuario.Email = model.Email;
                tablaUsuario.Direccion = model.Direccion;
                tablaUsuario.Fecha_Nacimiento = dtFechaNacimiento;
                tablaUsuario.Telefono = model.Telefono;
                tablaUsuario.Password = ComputeSha256Hash(model.Password);
                tablaUsuario.FechaCreacion = DateTime.Now;

                _usuario.Insert(tablaUsuario);

                return Json(new { success = true, responseText = "El usuario se ha creado exitosamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message });
            }
        }



        private string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public async Task<IActionResult> LogOut()
        {
            //SignOutAsync is Extension method for SignOut    
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Redirect to home page    
            return RedirectToAction("Login", "Autenticacion");
        }
    }
}
