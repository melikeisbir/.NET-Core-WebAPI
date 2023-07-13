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












builder.Services.AddScoped<IDersRepository, DersRepository>(); // dependency injection gereði
builder.Services.AddScoped<IKonuRepository, KonuRepository>();
builder.Services.AddScoped<ISoruRepository, SoruRepository>();
builder.Services.AddScoped<ISoruRepository, SoruRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IKullaniciRepository, KullaniciRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();


builder.Services.AddScoped<IValidator<Ders>, DersValidator>();
builder.Services.AddScoped<IValidator<Konu>, KonuValidator>();
builder.Services.AddScoped<IValidator<Soru>, SoruValidator>();
builder.Services.AddScoped<IValidator<Kullanici>, KullaniciValidator>(); // Baðýmlýlýk enjeksiyonu kaydýný yapýlandýrýr. IValidator<Kullanici> arayüzüne sahip bir baðýmlýlýðýn
                                                                         // KullaniciValidator sýnýfýyla çözülmesini saðlar. Baðýmlýlýk enjeksiyonu, bir bileþenin
                                                                         // (servis, sýnýf, arayüz vb.) baþka bir bileþene ihtiyaç duyduðu durumlarda kullanýlýr. Bu kod parçasýnda,
                                                                         // IValidator<Kullanici> arabirimi, KullaniciValidator sýnýfýyla eþleþtirilir. Yani KullaniciValidator,
                                                                         // IValidator<Kullanici> arabirimini uygular ve bu arabirime ihtiyaç duyan diðer bileþenler KullaniciValidator
                                                                         // ile çözümlenir.

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


builder.Services.AddAuthentication(options =>     // JWT tabanlý kimlik doðrulama ve yetkilendirme için gerekli yapýlandýrmalarý yapýyor
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





// Dependency Ýnjection : Dependency injection (DI), bir yazýlým tasarým desenidir. Bu desen, bir bileþenin (sýnýf veya hizmet) dýþ baðýmlýlýklarýný baþka
// bileþenlerden almasýný saðlar. Böylece, bileþenler arasýndaki baðýmlýlýklar azalýr ve kodun test edilebilirliði, yeniden kullanýlabilirliði ve deðiþtirilebilirliði
// artar. DI, baðýmlýlýklarýn dýþarýdan enjekte edildiði bir mekanizmayý kullanýr ve genellikle bir enjektör (injector) aracýlýðýyla gerçekleþtirilir. Bu desen
// sayesinde kod daha esnek, sürdürülebilir ve test edilebilir hale gelir.




//Otomatik validasyon, bir yazýlým sisteminde giriþ verilerinin doðruluðunu kontrol etmek için kullanýlan bir süreçtir. Bu süreç, kullanýcý
//tarafýndan girilen verilerin belirli bir kural setine uygun olup olmadýðýný otomatik olarak kontrol eder. 