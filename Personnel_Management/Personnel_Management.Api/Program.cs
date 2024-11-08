
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Personnel_Management.Business.AuthService;
using Personnel_Management.Business.NhanVienService;
using Personnel_Management.Data.BaseRepository;
using Personnel_Management.Data.EntityRepository;
using Personnel_Management.Models.Models;
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
				options.AddPolicy("AllowAllOrigins",
					builder =>
					{
						builder.AllowAnyOrigin()
							   .AllowAnyMethod()
							   .AllowAnyHeader();
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

			builder.Services.AddAuthorization();

			// Add distributed memory cache to store session data
			builder.Services.AddDistributedMemoryCache();

			// Add session services
			builder.Services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
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

			app.UseHttpsRedirection();

			app.UseCors("AllowAllOrigins");


			app.UseAuthentication();
			app.UseSession();


			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
