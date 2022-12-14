using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using System;
using System.Text;
using Trainin_App_for_Repository.Data;
using Trainin_App_for_Repository.Mapping;
using Trainin_App_for_Repository.Repository;
using Trainin_App_for_Repository.Repository.Address;
using Trainin_App_for_Repository.Repository.Brands;
using Trainin_App_for_Repository.Repository.Cars;
using Trainin_App_for_Repository.Repository.FavStation;

namespace Trainin_App_for_Repository
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
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionScopedJobFactory();
                var jobKey = new JobKey("CollectApiJob");

                q.AddJob<CollectApiJob>(opts => opts.WithIdentity(jobKey));
                q.AddTrigger(opts => opts
                                        .ForJob(jobKey)
                                        .WithIdentity("CollectApiJob")
                                        .WithDailyTimeIntervalSchedule(time => time
                                                                                .WithIntervalInHours(24)
                                                                                .OnEveryDay()
                                                                                .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0)))); //run every day at 00.00 
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            services.AddControllers();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddMediatR(typeof(Startup));
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ICarsRepository, CarsRepository>();
            services.AddScoped<IBrandsRepository, BrandsRepository>();
            services.AddScoped<IFavStationsRepository, FavStationsRepository>();
            services.AddDbContext<FuelPriceDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DatabaseContext")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LoginSystem", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {

            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
                    }
                    });
            });
            var key = Encoding.ASCII.GetBytes("SuperStrongOverPowerKey");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(x =>
            {

                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Training App for Repository v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<FuelPriceDbContext>())
                {
                    context.Database.EnsureCreated();
                }
            }
        }
    }
}
