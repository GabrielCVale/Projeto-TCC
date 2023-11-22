using Microsoft.EntityFrameworkCore;
using Projeto_TCC.Data;
using Tcc.Data;

namespace Projeto_TCC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication("MyCookieAuthenticationScheme")
            .AddCookie("MyCookieAuthenticationScheme", config =>
        {
        config.Cookie.Name = "MyCookie";
        config.LoginPath = "/Home/Login";
        config.AccessDeniedPath = "/Home/Login";
        });

            // Configura��o da inje��o de depend�ncia do MySqlConnectionHelper
            builder.Services.AddScoped<MySqlConnectionHelper>(provider => new MySqlConnectionHelper(
                "server=localhost;initial catalog=cadastro;uid=root;")); // Substitua pela sua string de conex�o real

            // Adiciona servi�os ao cont�iner.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configura��o do pipeline de requisi��o HTTP.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // O valor padr�o HSTS � de 30 dias. Voc� pode querer alterar isso para cen�rios de produ��o, veja https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}