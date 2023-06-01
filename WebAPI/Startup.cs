using Core.DependecnyResolvers;
using Core.Extensions;
using Core.Utilities.Ioc;
using Core.Utilities.Security.Encyption;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
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
            //servisler

            services.AddControllers();
            services.AddCors(options =>//izin vermediğimiz kullanıcıların girmemesi kontrolü burda yapılır
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://localhost:3000"));//domain verildiği yer
            });

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>(); 

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer= true,
                    ValidateAudience= true,
                    ValidateLifetime= true,
                    ValidIssuer= tokenOptions.Issuer,
                    ValidAudience= tokenOptions.Audience,
                    ValidateIssuerSigningKey= true, 
                    IssuerSigningKey=SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
                };
            });
            services.AddDependencyResolvers(new ICoreModule[]
            {
                new CoreModule()
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
  
            }

            //Bu kod parçacıkları, ASP.NET Core uygulamasında genellikle Startup.cs dosyasının Configure metodu içinde yer alır
            //Bu sıralama, HTTPS yönlendirmesini etkinleştirir, isteklerin yönlendirme ve kimlik doğrulama/
            //otorizasyon işlemlerinden geçmesini sağlar.Bu sayede, güvenli iletişim sağlanır ve kullanıcıların kimlik 
            //doğrulaması ve yetkilendirme işlemleri gerçekleştirilir.(middleware)

            //bunların sırası önemli
            app.UseCors(builder=>builder.WithOrigins("http://localhost:3000").AllowAnyHeader());//burdan gelen talebe cevap ver demek

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();//eve girmek için anahtar
            app.UseAuthorization();//evin içinde ne yapabilir
            


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
