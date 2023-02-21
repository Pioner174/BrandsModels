using BrandsModels.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddMvc();

builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:B&MConnection"]);
});

builder.Services.AddDbContext<IdentityContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:IdentityConnection"]);
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(opts =>
{
    // ��������� ������ ������
    opts.Password.RequiredLength = 6;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;

    opts.User.RequireUniqueEmail = true; // ����� ������ ���� ���������� � ������� ������������

   
});

builder.Services.AddAuthentication().AddCookie(cfg =>
{
    cfg.SlidingExpiration = true;
    //cfg.LoginPath = "/Account/SignIn";
})
    .AddJwtBearer(jwtOpts =>
{
    jwtOpts.RequireHttpsMetadata = false; // ��������� ���������� https ���������
    jwtOpts.SaveToken = true;

    jwtOpts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,    // ��������� ����� ������������
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(builder.Configuration["jwtSecret"]!)),   // ��������� ����� ������������, ����� ����� �� ��������
        ValidateAudience = false,   // ����� �� �������������� ����������� ������
        ValidateIssuer = false,     // ��������, ����� �� �������������� �������� ��� ��������� ������
        ValidateLifetime = true     //����� �� �������������� ����� �������������
    };
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


var _dataContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
SeedData.SeedDatabase(_dataContext);

app.Run();
