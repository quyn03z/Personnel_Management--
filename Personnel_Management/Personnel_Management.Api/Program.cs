
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Personnel_Management.Business.AuthService;
using Personnel_Management.Business.NhanVienService;
using Personnel_Management.Business.PhongBanService;
using Personnel_Management.Data.BaseRepository;
using Personnel_Management.Data.EntityRepository;
using Personnel_Management.Models.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace Personnel_Management.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			//test
			// Add services to the container.

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAngularApp",
					builder =>
					{
						builder.WithOrigins("http://localhost:4200", "http://localhost:5008") // Use the URL where your Angular app is running
							   .AllowAnyMethod()
							   .AllowAnyHeader()
							   .AllowCredentials(); // Enables cookies and credentials
					});
			});



			builder.Services.AddControllers();

			builder.Services.AddDbContext<QuanLyNhanSuContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

			builder.Services.AddScoped<INhanVienRepository, NhanVienRepository>();
			builder.Services.AddScoped<INhanVienService, NhanVienService>();
			builder.Services.AddScoped<BaseRepository<NhanVien>>();

			builder.Services.AddScoped<IPhongBanRepository, PhongBanRepository>();
			builder.Services.AddScoped<IPhongBanService, PhongBanService>();
			builder.Services.AddScoped<BaseRepository<PhongBan>>();

			builder.Services.AddScoped<IAuthService, AuthService>();





			// Authentication

			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = builder.Configuration["Jwt:Issuer"],
					ValidAudience = builder.Configuration["Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
				};
			});

			builder.Services.AddSwaggerGen(options =>
			{
				options.AddSecurityDefinition("cookieAuth", new OpenApiSecurityScheme
				{
					Name = "Cookie",
					Type = SecuritySchemeType.ApiKey,
					In = ParameterLocation.Header,
					Description = "Session-based authentication using cookies"
				});

				options.OperationFilter<AddRequiredHeadersParameter>(); // To add headers and cookies if necessary
			});


			builder.Services.AddAuthorization();

			// Add distributed memory cache to store session data
			builder.Services.AddDistributedMemoryCache();

			// Add session services
			builder.Services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(30);  // Increase as needed for development
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
				options.Cookie.SameSite = SameSiteMode.Lax;  // Consider using None if cross-origin
			});

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			else
			{
				app.UseHttpsRedirection();
			}

			app.UseHttpsRedirection();

			app.UseCors("AllowAngularApp");


			app.UseAuthentication();
			app.UseSession();


			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
public class AddRequiredHeadersParameter : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		if (operation.Parameters == null)
			operation.Parameters = new List<OpenApiParameter>();

		// Adds a Cookie header to Swagger requests to handle session-based auth
		operation.Parameters.Add(new OpenApiParameter
		{
			Name = "Cookie",
			In = ParameterLocation.Header,
			Description = "Session-based authentication using cookies",
			Required = false,
			Schema = new OpenApiSchema
			{
				Type = "string"
			}
		});
	}
}
