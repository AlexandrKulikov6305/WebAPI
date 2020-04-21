using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SoundCloudLite.BLL.Contracts;
using SoundCloudLite.BLL.Implementations;
using SoundCloudLite.DAL;

namespace SoundCloudLite.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<IArtistBll, ArtistBll>();
            services.AddSingleton<IRepository, Repository>();
            services.AddSingleton<ITrackBll, TrackBll>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                // Default Route
                routes.MapRoute(
                    name: "ArtistById",
                    template: "{controller=Artists}/{action=Get}/{id?}");
                routes.MapRoute(
                    name: "ArtistByGenre",
                    template: "{controller=Artists}/{action=Get}/{key}&{genre}");
                routes.MapRoute(
                    name: "ArtistAdd",
                    template: "{controller=Artists}/{action=Post}/");
                routes.MapRoute(
                    name: "ArtistChangeGenre",
                    template: "{controller=Artists}/{action=Put}/{id}");
                routes.MapRoute(
                    name: "TrackById",
                    template: "{controller=Tracks}/{action=Get}/{id}");
                routes.MapRoute(
                    name: "TrackByArtist",
                    template: "{controller=Tracks}/{action=Get}/");
            });
        }
    }
}