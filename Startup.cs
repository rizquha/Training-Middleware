using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Training_Middleware
{
    public class Startup
    {
        private IConfiguration configuration;
        

        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    ValidateIssuer=false,
                    ValidateAudience=false,
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            

            // app.Use(async (context,next)=>
            // {
            //     // await context.Response.WriteAsync("Ini middleware pertama");
            //     //next invoke digunakan untuk melanjutkan ke middleware selanjutnya


            //     Console.WriteLine(context.Request.Query["page"]);
            //     await next.Invoke();  

            // });
            // app.Use(async (context,next)=>
            // {
            //     await context.Response.WriteAsync("Ini middleware kedua");
            //     //next invoke digunakan untuk melanjutkan ke middleware selanjutnya
            //     await next.Invoke();  
            // });

            // //Run cuman 1 saja, sedangkan use bisa lebih dari 1, Run biasanya digunakan untuk final middleware
            // app.Run(async context =>
            // {
            //     await context.Response.WriteAsync("Ini middleware lagi");
            // });


            // app.Use(async(context,next)=>
            // {
            //     // var form = context.Request.Form;
            //     // Console.WriteLine(form["username"]);
                
            //     //untuk melihat system sedang dalam under construction
            //     var status = File.ReadLines("status.txt").First();
            //     if(status=="down")
            //     {
            //         var data = new{
            //             message = "Sedang under-construction"
            //         };
            //         var responseJson = JsonSerializer.Serialize(data);
            //         await context.Response.WriteAsync(responseJson);
            //         return;
            //     }
                
                
            //     await next.Invoke();
            // });

            // app.Map("/api/posts",HandlePostMiddleware);

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("!");
                });
            });
        }

        // private void HandlePostMiddleware(IApplicationBuilder app)
        // {
        //     app.Use(async(context,next)=>
        //     {
        //         if(context.Request.Query["passcode"]=="1234")
        //         {
        //             await next.Invoke();
        //             return;
        //         }
        //         var data = new
        //         {
        //             message = "Anda tidak boleh masuk tanpa passcode"
        //         };
        //         var json = JsonSerializer.Serialize(data);
        //         await context.Response.WriteAsync(json);
        //     });
        // }
    }
}
