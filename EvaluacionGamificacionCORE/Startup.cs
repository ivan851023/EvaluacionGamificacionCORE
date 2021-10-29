using EvaluacionGamificacionCORE.DAL.Context;
using EvaluacionGamificacionCORE.DAL.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string conStr = this.Configuration.GetConnectionString("conEvaluacion");
            services.AddDbContext<EvaluacionContext>(options => options.UseOracle(conStr));
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IPerfil, PerfilDAL>();
            services.AddScoped<ITipoMascota, TipoMascotaDAL>();
            services.AddScoped<IPuntuacion, PuntuacionDAL>();
            services.AddScoped<IVwPuntuacion, VwPuntuacionDAL>();
            services.AddScoped<IUsuario, UsuarioDAL>();
            services.AddControllersWithViews();
            services.AddMvc();
            services.AddMvcCore();

            services.AddSingleton<IConfiguration>(Configuration);


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x => x.LoginPath = "/autenticacion/login");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Login",
                    pattern: "{controller=Autenticacion}/{action=Login}/{id?}");
            });
        }
    }
}