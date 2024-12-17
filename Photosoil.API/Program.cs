using Microsoft.EntityFrameworkCore;
using Photosoil.Service.Abstract;
using Photosoil.Service.Data;
using Photosoil.Service.Helpers;
using Photosoil.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Photosoil.Core.Models.Second;
using Photosoil.Service.Services.Second;
using Microsoft.OpenApi.Models;
using Photosoil.Core.Models;
using Microsoft.Extensions.FileProviders;


namespace PhotosoilAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


            // 👇 Configuring the Authorization Service

            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options => {

                options.SwaggerDoc("Main", new OpenApiInfo
                {
                    Title = "",
                    Version = "v1",
                    Description = "",

                });
                options.SwaggerDoc("Second", new OpenApiInfo
                {
                    Title = "",
                    Version = "v1",
                    Description = "",

                });

            });




            builder.Services.AddScoped<ISoilObjectService, SoilObjectService>();
            builder.Services.AddScoped<TagsService>();
            builder.Services.AddScoped<NewsService>();
            builder.Services.AddScoped<PhotoService>();
            builder.Services.AddScoped<ClassificationService>();
            builder.Services.AddScoped<PublicationService>();
            builder.Services.AddScoped<EcoSystemService>();

            builder.Services.AddScoped<TermsService>();
            builder.Services.AddScoped<Classification>();
            builder.Services.AddScoped<ArticleService>();
            builder.Services.AddScoped<AuthorService>();
            builder.Services.AddScoped<NewsService>();
            builder.Services.AddScoped<RulesService>();
            builder.Services.AddScoped<AuthorRequestService>();




            builder.Services.AddScoped<AccountService>();


            builder.Services.AddScoped<IEnumService, EnumService>();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


            builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddDbContext<ApplicationDbContext>(x =>
                x.UseNpgsql(connectionString, builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);

                })
            );

            builder.Services.AddAuthentication(options =>
            {


                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidAudience = AuthOptions.AUDIENCE,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
                };
            });


            //         builder.Services
            //  .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //  .AddJwtBearer(options =>
            //  {
            //      options.Authority = "https://securetoken.google.com/cloudcfd-df4f5";
            //      options.TokenValidationParameters = new TokenValidationParameters
            //      {
            //          ValidateIssuer = true,
            //          ValidIssuer = "https://securetoken.google.com/cloudcfd-df4f5",
            //          ValidateAudience = true,
            //          ValidAudience = "cloudcfd-df4f5",
            //          ValidateLifetime = true
            //      };
            //  })
            //  .AddCookie();
            //builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            //    {
            //
            //        options.Tokens.AuthenticatorIssuer = JwtBearerDefaults.AuthenticationScheme;
            //        options.User.RequireUniqueEmail= true;
            //        options.Password.RequiredLength = 8;
            //    }).AddEntityFrameworkStores<ApplicationDbContext>().AddRoles<IdentityRole>()
            //    .AddDefaultTokenProviders();

            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //     .AddJwtBearer(options =>
            //     {
            //         options.TokenValidationParameters = new TokenValidationParameters
            //         {
            //             ValidateIssuer = true,
            //             ValidateAudience = true,
            //             ValidateLifetime = true,
            //             ValidateIssuerSigningKey = true,
            //             ValidIssuer = AuthOptions.ISSUER,
            //             ValidAudience = AuthOptions.ISSUER,
            //             IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
            //         };
            //     });

            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(
            //    options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,
            //            ValidIssuer = AuthOptions.ISSUER,
            //            ValidAudience = AuthOptions.AUDIENCE,
            //            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            //        };
            //    }
            //);

            //builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            //    {
            //
            //        options.Tokens.AuthenticatorIssuer = JwtBearerDefaults.AuthenticationScheme;
            //        options.User.RequireUniqueEmail= true;
            //        options.Password.RequiredLength = 8;
            //    }).AddEntityFrameworkStores<ApplicationDbContext>().AddRoles<IdentityRole>();
            //
            //builder.Services.AddAuthorization();
            //
            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //});

            // builder.Services.AddAuthentication(options =>
            // {
            //     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            // }).AddJwtBearer(options =>
            // {
            //     options.TokenValidationParameters = new TokenValidationParameters
            //     {
            //         ValidateIssuer = true,
            //         ValidIssuer = "yourIssuer",
            //         ValidateAudience = true,
            //         ValidAudience = "yourAudience",
            //         ValidateLifetime = true,
            //         ValidateIssuerSigningKey = true,
            //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("yourSecretKey"))
            //     };
            // });
            //
            // builder.Services.AddAuthorization().AddIdentity<ApplicationUser, IdentityRole>(options =>
            //    {
            //        options.Tokens.AuthenticatorIssuer = JwtBearerDefaults.AuthenticationScheme;
            //        options.User.RequireUniqueEmail= true;
            //        options.Password.RequiredLength = 8;
            //    }).AddEntityFrameworkStores<ApplicationDbContext>().AddRoles<IdentityRole>();


            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                    });
            });
            builder.Services.AddAutoMapper(typeof(AppMappingProfile));



            var app = builder.Build();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/Main/swagger.json", "Main");
                c.SwaggerEndpoint("/swagger/Second/swagger.json", "Second");

            });
            // Configure the HTTP request pipeline.
                app.UseSwagger();
                app.UseSwaggerUI();

            //app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    "/Storage"),
                RequestPath = "/Storage"
            });
            app.UseCors();


            app.MapControllers();

            app.Run();
        }
    }
}