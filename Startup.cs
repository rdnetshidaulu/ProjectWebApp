using Ids;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApp
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
            services.AddIdentityServer()
                .AddInMemoryClients(Config.Clients)
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiResources(Config.ApiResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddTestUsers(Config.Users)
                .AddDeveloperSigningCredential();

            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());


            /// Get Token
            #region GetToken
            /// <summary>
            /// Get DebtCloud Token
            /// </summary>
            /// <returns></returns>
            //public static string GetToken()
            //{
            //    var client = new RestClient("https://localhost:44385/connect/token");
            //    client.Timeout = -1;
            //    var request = new RestRequest(Method.POST);
            //    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            //    request.AddParameter("grant_type", "client_credentials");
            //    request.AddParameter("client_id", "m2m.client");
            //    request.AddParameter("client_secret", "SuperSecretPassword");
            //    IRestResponse response = client.Execute(request);

            //    JObject mainObject = JObject.Parse(response.Content);
            //    JValue authToken = (JValue)mainObject["access_token"];

            //    var authTokenSting = authToken.ToString();

            //    return authTokenSting;
            //}
            #endregion

        }
    }
}
