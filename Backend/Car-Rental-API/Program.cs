using CarRent.BusinessLayer.AppService.Account;
using CarRent.BusinessLayer.AppService.CarService;
using CarRent.BusinessLayer.AppService.RentAgreement;
using CarRent.DataAccessLayer.Context;
using CarRent.DataAccessLayer.Model;
using CarRent.DataAccessLayer.Repository.Account;
using CarRent.DataAccessLayer.Repository.Car;
using CarRent.DataAccessLayer.Repository.RentAgreement;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using static CarRent.BusinessLayer.AppService.Account.AccountService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//CORS
builder.Services.AddCors(option =>
{
    option.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
       .AllowAnyHeader();
    });
});

//DB CONNECTION
builder.Services.AddDbContext<AppDbContext>(option => {
    option.UseSqlServer("Data Source=IN-PG03521Y;Initial Catalog=CarRental;Integrated Security=True;");
});

// IdentityFramework
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
               .AddJwtBearer(option =>
               {
                   option.SaveToken = true;
                   option.RequireHttpsMetadata = false;
                   option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidAudience = configuration["JWT:ValidAudience"],
                       ValidIssuer = configuration["JWT:ValidIssuer"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                   };
               });

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = false;


});
builder.Services.AddAuthentication("CustomCookieScheme")
    .AddCookie("CustomCookieScheme", options =>
    {
        options.Cookie.Name = "UserCookie";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
    });

//Repostiories
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountServices, AccountServices>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IRentRepository, RentRepository>();
builder.Services.AddScoped<IRentService, RentService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//Seed->Adding Roles
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    // //Admin - 1
    string email = "admin@admin.com";
    string password = "Admin@123";

    // //User - 1
    string email1 = "user1@user.com";
    string password1 = "User@123";

    // //User - 2
    string email2 = "user2@user.com";
    string password2 = "User@123";



    if (await userManager.FindByEmailAsync(email2) == null)
    {
        var admin = new ApplicationUser
        {
            UserName = email,
            Email = email,
            Name = "Admin"
        };

        var user1 = new ApplicationUser
        {
            UserName = email1,
            Email = email1,
            Name = "User1"
        };
        var user2 = new ApplicationUser
        {
            UserName = email2,
            Email = email2,
            Name = "User2"
        };


        if (admin != null)
        {
            await userManager.CreateAsync(admin, password);
            await userManager.AddToRoleAsync(admin, "Admin");
        }

        if (user1 != null)
        {
            await userManager.CreateAsync(user1, password1);
            await userManager.AddToRoleAsync(user1, "User");
        }
        if (user2 != null)
        {
            await userManager.CreateAsync(user2, password1);
            await userManager.AddToRoleAsync(user2, "User");
        }
    }

}


app.UseCors("MyPolicy");
app.Run();
