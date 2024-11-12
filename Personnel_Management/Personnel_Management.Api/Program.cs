using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Personnel_Management.Business.AuthService;
using Personnel_Management.Business.DiemDanhService;
using Personnel_Management.Business.LuongService;
using Personnel_Management.Business.NhanVienService;
using Personnel_Management.Data.BaseRepository;
using Personnel_Management.Data.EntityRepository;
using Personnel_Management.Models.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using System.Text.Json.Serialization;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		// add CORS
		builder.Services.AddCors(options =>
		{
			options.AddPolicy("AllowAngularApp",
				builder =>
				{
					builder.WithOrigins("http://localhost:4200", "http://localhost:5008")
						   .AllowAnyMethod()
						   .AllowAnyHeader()
						   .AllowCredentials(); // Enables cookies and credentials
				});
		});




		// Add services to the container.
		builder.Services.AddDbContext<QuanLyNhanSuContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        builder.Services.AddScoped<INhanVienService, NhanVienService>();
        builder.Services.AddScoped<INhanVienRepository, NhanVienRepository>();
        builder.Services.AddScoped<IThuongPhatRepository, ThuongPhatRepository>();
        builder.Services.AddScoped<ILuongService, LuongService>();
        builder.Services.AddScoped<ILuongRepository, LuongRepository>();
        builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        builder.Services.AddScoped<IDepartmentService, DepartmentService>();
		builder.Services.AddScoped<IDiemDanhService, DiemDanhService>();
		builder.Services.AddScoped<IDiemDanhRepository, DiemDanhRepository>();
		builder.Services.AddControllers();
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

		builder.Services.AddDistributedMemoryCache();

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
        app.UseCors("AllowAngularApp");
        app.UseHttpsRedirection();

        

        app.UseAuthentication();
        app.UseAuthorization();
		app.UseSession();

		app.MapControllers();

        app.Run();
    }
}

