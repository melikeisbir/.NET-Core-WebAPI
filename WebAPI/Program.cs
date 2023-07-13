using Business.Validation;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Repository;
using Entity;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);


var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

builder.Services.AddDbContext<AppDbContext>(options =>
{
var environment = builder.Environment.EnvironmentName;
if (environment == Environments.Development)
{
    options.UseInMemoryDatabase("InMemoryDatabaseName");
}
else if (environment == Environments.Staging)
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiDatabase"));
    }
});












builder.Services.AddScoped<IDersRepository, DersRepository>(); // dependency injection gere�i
builder.Services.AddScoped<IKonuRepository, KonuRepository>();
builder.Services.AddScoped<ISoruRepository, SoruRepository>();
builder.Services.AddScoped<ISoruRepository, SoruRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IKullaniciRepository, KullaniciRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();


builder.Services.AddScoped<IValidator<Ders>, DersValidator>();
builder.Services.AddScoped<IValidator<Konu>, KonuValidator>();
builder.Services.AddScoped<IValidator<Soru>, SoruValidator>();
builder.Services.AddScoped<IValidator<Kullanici>, KullaniciValidator>(); // Ba��ml�l�k enjeksiyonu kayd�n� yap�land�r�r. IValidator<Kullanici> aray�z�ne sahip bir ba��ml�l���n
                                                                         // KullaniciValidator s�n�f�yla ��z�lmesini sa�lar. Ba��ml�l�k enjeksiyonu, bir bile�enin
                                                                         // (servis, s�n�f, aray�z vb.) ba�ka bir bile�ene ihtiya� duydu�u durumlarda kullan�l�r. Bu kod par�as�nda,
                                                                         // IValidator<Kullanici> arabirimi, KullaniciValidator s�n�f�yla e�le�tirilir. Yani KullaniciValidator,
                                                                         // IValidator<Kullanici> arabirimini uygular ve bu arabirime ihtiya� duyan di�er bile�enler KullaniciValidator
                                                                         // ile ��z�mlenir.

builder.Services.AddIdentity<Kullanici, Role>(options =>
{
    //options.Password.RequiredLength = 8;
    //options.Password.RequireUppercase = true;
    //options.Password.RequireLowercase = true;
    //options.Password.RequireNonAlphanumeric = true;
    //options.Lockout.MaxFailedAccessAttempts = 5;
    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
})
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

//builder.Services.AddIdentity<Kullanici, Role>()
//        .AddUserManager<Microsoft.AspNetCore.Identity.UserManager<KullaniciRole>>();


builder.Services.AddAuthentication(options =>     // JWT tabanl� kimlik do�rulama ve yetkilendirme i�in gerekli yap�land�rmalar� yap�yor
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };

});
builder.Services.AddAuthorization();


builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()); // otomatik validasyon
    });



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();


app.MapControllers();

app.Run();





// Dependency �njection : Dependency injection (DI), bir yaz�l�m tasar�m desenidir. Bu desen, bir bile�enin (s�n�f veya hizmet) d�� ba��ml�l�klar�n� ba�ka
// bile�enlerden almas�n� sa�lar. B�ylece, bile�enler aras�ndaki ba��ml�l�klar azal�r ve kodun test edilebilirli�i, yeniden kullan�labilirli�i ve de�i�tirilebilirli�i
// artar. DI, ba��ml�l�klar�n d��ar�dan enjekte edildi�i bir mekanizmay� kullan�r ve genellikle bir enjekt�r (injector) arac�l���yla ger�ekle�tirilir. Bu desen
// sayesinde kod daha esnek, s�rd�r�lebilir ve test edilebilir hale gelir.




//Otomatik validasyon, bir yaz�l�m sisteminde giri� verilerinin do�rulu�unu kontrol etmek i�in kullan�lan bir s�re�tir. Bu s�re�, kullan�c�
//taraf�ndan girilen verilerin belirli bir kural setine uygun olup olmad���n� otomatik olarak kontrol eder. 