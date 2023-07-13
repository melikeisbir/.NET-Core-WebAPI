using Business.Validation;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Repository;
using Entity;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseInMemoryDatabase("InMemoryDatabaseName"));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiDatabase")));
builder.Services.AddScoped<IDersRepository, DersRepository>(); // dependency injection gereði
                                                               // ( Baðýmlýlýk Enjeksiyonu, bir yazýlým
                                                               // tasarým deseni veya ilkesidir. Bu desen,
                                                               // bir bileþenin gereksinim duyduðu diðer
                                                               // bileþenleri doðrudan yaratmak yerine,
                                                               // bu baðýmlýlýklarýn dýþarýdan saðlanmasýný
                                                               // saðlar. Bu sayede bileþenler arasýndaki
                                                               // sýký baðýmlýlýklar azalýr, yazýlým daha
                                                               // esnek, test edilebilir ve sürdürülebilir
                                                               // hale gelir. Baðýmlýlýk Enjeksiyonu üç
                                                               // yöntemle uygulanabilir: Constructor
                                                               // Injection (Yapýcý Enjeksiyon), Setter
                                                               // Injection (Setter Enjeksiyonu) ve
                                                               // Interface Injection (Arayüz Enjeksiyonu)
builder.Services.AddScoped<IKonuRepository, KonuRepository>();
builder.Services.AddScoped<ISoruRepository, SoruRepository>();

builder.Services.AddScoped<IValidator<Ders>, DersValidator>();
builder.Services.AddScoped<IValidator<Konu>, KonuValidator>();
builder.Services.AddScoped<IValidator<Soru>, SoruValidator>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
       .AddEntityFrameworkStores<AppDbContext>()
       .AddDefaultTokenProviders();

builder.Services.AddControllers()
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()); // otomatik validasyon
                                                                                // (fluent api) için gerekli
                                                                                // (Otomatik validasyon, bir
                                                                                // yazýlým bileþeninin giriþ
                                                                                // verilerinin doðruluðunu
                                                                                // kontrol etmek için otomatik
                                                                                // olarak yapýlan bir süreçtir.
                                                                                // Bu süreç, hatalý veya eksik
                                                                                // verilerin tespit edilmesi ve
                                                                                // istenmeyen sonuçlarýn önlenmesi
                                                                                // amacýyla kullanýlýr.)
    });



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

app.UseAuthorization();
app.UseAuthentication();


app.MapControllers();

app.Run();
