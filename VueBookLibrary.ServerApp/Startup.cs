using System;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using VueBookLibrary.ServerApp.Models;
using VueBookLibrary.ServerApp.Helpers;

namespace VueBookLibrary.ServerApp
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
            services.AddCors();
            services.AddControllers();

            services.AddDbContext<DataContext>(opts => opts.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddDbContext<IdentityContext>(opts => opts.UseSqlServer(Configuration["ConnectionStrings:IdentityConnection"]));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();
            services.Configure<IdentityOptions>(opts =>
            {
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
                opts.User.RequireUniqueEmail = true;
                opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz";
            });
            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opts =>
            {
                opts.RequireHttpsMetadata = false;
                opts.SaveToken = true;
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["jwtSecret"])),
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
                opts.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async ctx =>
                    {
                        var usrmgr = ctx.HttpContext.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
                        var signinmgr = ctx.HttpContext.RequestServices.GetRequiredService<SignInManager<IdentityUser>>();
                        string userEmail = ctx.Principal.FindFirst(ClaimTypes.Email)?.Value;
                        IdentityUser idUser = await usrmgr.FindByEmailAsync(userEmail);
                        ctx.Principal = await signinmgr.CreateUserPrincipalAsync(idUser);
                    }
                };
            });

            // configure swagger UI
            services.AddSwaggerGen(s => {                                                                 //for Swagger (Testing API Controller) including Authorization
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "VueSportsStore ServerApp", Version = "v1" });
                s.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.",
                });
                //////Add Operation Specific Authorization///////
                s.OperationFilter<AuthOperationFilter>();
                ////////////////////////////////////////////////
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => builder.AllowAnyOrigin()//WithOrigins("http://localhost:1337")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "VueBookLibrary API");
            });

            SeedData.SeedDatabase(services.GetRequiredService<DataContext>());
            IdentitySeedData.CreateAdminAccount(app.ApplicationServices, Configuration);
        }
    }
}
