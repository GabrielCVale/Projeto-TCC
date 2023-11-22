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

            // Configuração da injeção de dependência do MySqlConnectionHelper
            builder.Services.AddScoped<MySqlConnectionHelper>(provider => new MySqlConnectionHelper(
                "server=localhost;initial catalog=cadastro;uid=root;")); // Substitua pela sua string de conexão real

            // Adiciona serviços ao contêiner.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configuração do pipeline de requisição HTTP.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // O valor padrão HSTS é de 30 dias. Você pode querer alterar isso para cenários de produção, veja https://aka.ms/aspnetcore-hsts.
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