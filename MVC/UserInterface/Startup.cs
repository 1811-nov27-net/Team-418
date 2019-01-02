using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserInterface.Filters;
using UserInterface.Models;

namespace UserInterface
{
    public class Startup
    {
        public void SyncFromDB()
        {
            // Yep.
            HttpClient client = new HttpClient();
            SongViewModel.SyncSongsAsync(client);
            UserViewModel.SyncUsersAsync(client);
            ArtistViewModel.SyncArtistAsync(client);
            AlbumViewModel.SyncAlbumsAsync(client);
            PendingSongViewModel.SyncPendingSongsAsync(client);
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            SyncFromDB();

            // Dummy Data
            //if (AlbumViewModel.Albums.Count == 0)
            if(false)
            {
                Random rand = new Random(DateTime.Now.TimeOfDay.Milliseconds);

                ArtistViewModel twentyonepilots = new ArtistViewModel
                {
                    Name = "Twenty One Pilots"
                };
                AlbumViewModel trench = new AlbumViewModel
                {
                    Name = "Trench"
                };
                AlbumViewModel blurryface = new AlbumViewModel
                {
                    Name = "Blurryface"
                };
                trench.Artist = twentyonepilots.Name;
                blurryface.Artist = twentyonepilots.Name;
                twentyonepilots.Albums.Add(trench);
                twentyonepilots.Albums.Add(blurryface);

                // Actual songs.
                SongViewModel song = new SongViewModel()
                {
                    Album = trench.Name,
                    Artist = twentyonepilots.Name,
                    Name = "Chlorine",
                    PlayTime = new TimeSpan(0, 5, 24),
                    Link = "Wc79sjzjNuo"

                };
                trench.Songs.Add(song);
                song = new SongViewModel()
                {
                    Album = trench.Name,
                    Artist = twentyonepilots.Name,
                    Name = "Pet Cheetah",
                    PlayTime = new TimeSpan(0, 3, 18),
                    Link = "VGMmSOsNAdc"
                };
                trench.Songs.Add(song);
                song = new SongViewModel()
                {
                    Album = blurryface.Name,
                    Artist = twentyonepilots.Name,
                    Name = "Ride",
                    PlayTime = new TimeSpan(0, 3, 34),
                    Link = "Pw-0pbY9JeU"
                };
                blurryface.Songs.Add(song);
                song = new SongViewModel()
                {
                    Album = blurryface.Name,
                    Artist = twentyonepilots.Name,
                    Name = "Polarize",
                    PlayTime = new TimeSpan(0, 3, 46),
                    Link = "MiPBQJq49xk"
                };
                blurryface.Songs.Add(song);

                for (int i = 0; i < 2; ++i)
                {
                    ArtistViewModel artist = new ArtistViewModel
                    {
                        Name = "ArtistName " + (rand.Next() % 100).ToString(),
                    };

                    for (int j = 0; j < 2; ++j)
                    {

                        AlbumViewModel album = new AlbumViewModel
                        {
                            Name = "AlbumName " + (rand.Next() % 100).ToString(),
                        };


                        List<SongViewModel> songs = new List<SongViewModel>();
                        for (int k = 0; k < 3; ++k)
                        {
                            songs.Add(new SongViewModel
                            {
                                Album = album.Name,
                                Artist = artist.Name,
                                Name = "Name " + (rand.Next() % 100).ToString(),
                                PlayTime = new TimeSpan(0, rand.Next() % 5, rand.Next() % 59),
                                Link = "invalid link." + (rand.Next() % 100).ToString()
                            });
                        }
                        album.Artist = artist.Name;
                        artist.Albums.Add(album);
                    }

                    /*
                    UserViewModel user = new UserViewModel
                    {
                        Name = "John",
                        Favorites = twentyonepilots.Songs
                    };
                    UserViewModel.CurrentUser = user;
                    */
                }
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<HttpClient>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services
                .AddMvc(options =>
                {
                    options.Filters.Add(typeof(GetLoggedInUserFilter));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

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
                app.UseExceptionHandler("/Shared/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Song}/{action=SongIndex}/{id?}");
            });
        }
        
    }
}
