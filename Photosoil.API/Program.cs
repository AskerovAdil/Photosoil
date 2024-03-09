using Microsoft.EntityFrameworkCore;
using Photosoil.Service.Abstract;
using Photosoil.Service.Data;
using Photosoil.Service.Helpers;
using Photosoil.Service.Services;
using AutoMapper;
using Newtonsoft.Json;
using Photosoil.Core.Models.Second;
using Photosoil.Service.Services.Second;
using Microsoft.OpenApi.Models;
using Photosoil.API.Controllers.Second;

namespace PhotosoilAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options => {
                options.SwaggerDoc("Second", new OpenApiInfo
                {
                    Title = "",
                    Version = "v1",
                    Description = "",

                });
                options.SwaggerDoc("Main", new OpenApiInfo
                {
                    Title = "",
                    Version = "v1",
                    Description = "",

                });


            });


            builder.Services.AddTransient<ISoilObjectService, SoilObjectService>();
            builder.Services.AddTransient<PhotoService>();
            builder.Services.AddTransient<ClassificationService>();
            builder.Services.AddTransient<PublicationService>();
            builder.Services.AddTransient<EcoSystemService>();

            builder.Services.AddTransient<TermsService>();
            builder.Services.AddTransient<Classification>();
            builder.Services.AddTransient<ArticleService>();
            builder.Services.AddTransient<AuthorService>();
            builder.Services.AddTransient<IEnumService, EnumService>();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(x =>
                x.UseNpgsql(connectionString, builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);

                })


            );

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



            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/Second/swagger.json", "Second");
                c.SwaggerEndpoint("/swagger/Main/swagger.json", "Main");
            });
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}