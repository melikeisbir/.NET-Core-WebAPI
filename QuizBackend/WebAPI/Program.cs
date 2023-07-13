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
builder.Services.AddScoped<IDersRepository, DersRepository>(); // dependency injection gere�i
                                                               // ( Ba��ml�l�k Enjeksiyonu, bir yaz�l�m
                                                               // tasar�m deseni veya ilkesidir. Bu desen,
                                                               // bir bile�enin gereksinim duydu�u di�er
                                                               // bile�enleri do�rudan yaratmak yerine,
                                                               // bu ba��ml�l�klar�n d��ar�dan sa�lanmas�n�
                                                               // sa�lar. Bu sayede bile�enler aras�ndaki
                                                               // s�k� ba��ml�l�klar azal�r, yaz�l�m daha
                                                               // esnek, test edilebilir ve s�rd�r�lebilir
                                                               // hale gelir. Ba��ml�l�k Enjeksiyonu ��
                                                               // y�ntemle uygulanabilir: Constructor
                                                               // Injection (Yap�c� Enjeksiyon), Setter
                                                               // Injection (Setter Enjeksiyonu) ve
                                                               // Interface Injection (Aray�z Enjeksiyonu)
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
                                                                                // (fluent api) i�in gerekli
                                                                                // (Otomatik validasyon, bir
                                                                                // yaz�l�m bile�eninin giri�
                                                                                // verilerinin do�rulu�unu
                                                                                // kontrol etmek i�in otomatik
                                                                                // olarak yap�lan bir s�re�tir.
                                                                                // Bu s�re�, hatal� veya eksik
                                                                                // verilerin tespit edilmesi ve
                                                                                // istenmeyen sonu�lar�n �nlenmesi
                                                                                // amac�yla kullan�l�r.)
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
