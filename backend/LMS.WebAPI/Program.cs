using LMS.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.Text;
using LMS.DAL.POS;
using LMS.Service.POS;
using LMS.DAL;
using LMS.Service;
using LMS.WebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddScoped<IJWTTokenGenerator, JWTTokenGenerator>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
ConfigurationManager configuration = builder.Configuration;
//builder.Services.Configure<KestrelServerOptions>(configuration.GetSection("Kestrel"));

var serverVersion = new MySqlServerVersion(new Version(8, 0, 26)); // Get the value from SELECT VERSION()
// Read connection string from environment variable
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                      ?? configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"Using Connection String: {connectionString}"); // Debugging
builder.Services.AddDbContext<ApplicationDbContext>(c => c.UseMySql(connectionString, serverVersion));

var backendUrl = Environment.GetEnvironmentVariable("BACKEND_URL") ?? "http://0.0.0.0:6100";
builder.WebHost.UseUrls(backendUrl);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/NLog.config"));
//var logger =NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();

//var  log =LogManager.GetCurrentClassLogger();

#region Register Services Receivable

//builder.Services.AddScoped<IRcvService, RecevService>();

#endregion
#region Register DAL Reaceivable

//builder.Services.AddScoped<IRcvDAL, ReceivableDAL>();

#endregion

#region Register Services Inquiry

//builder.Services.AddScoped<IInqService, InqService>();

#endregion
#region Register DAL  Inquiry

//builder.Services.AddScoped<IInqDAL, InquiryDAL>();

#endregion


#region Register Services CTL

//builder.Services.AddScoped<ICtlService, CtlService>();

#endregion
#region Register DAL CTL

//builder.Services.AddScoped<ICtlDAL, CTLDAL>();
#endregion

#region Register Services POS

builder.Services.AddScoped<ISaleService, SaleService>();

#endregion
#region Register DAL POS

builder.Services.AddScoped<ISaleDAL, SaleDAL>();
#endregion
builder.Services.AddHttpClient();
builder.Services.AddScoped<CoreService>();
builder.Services.AddScoped<CatalogueService>();
//builder.Services.AddScoped<ClientService>();


#region Register Services TMS

//builder.Services.AddScoped<ITmsService, TmsService>();

#endregion
#region Register DAL  TMS

//builder.Services.AddScoped<ITmsDAL, TMSDAL>();
#endregion




//builder.Services.AddIdentity<User, IdentityRole>(
//        options =>
//        {
//            options.User.RequireUniqueEmail = true;
//            options.SignIn.RequireConfirmedAccount = false;
//            options.Password.RequiredLength = 4;
//            options.Password.RequireLowercase = false;
//            options.Password.RequireUppercase = false;
//            options.Password.RequireNonAlphanumeric = false;
//            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@._-"; // Allow special characters like '@', '.', and '-'
//        }
//        )
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthentication(cfg =>
{
    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{

    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
        ValidIssuer = configuration["Token:Issuer"],
        ValidAudience = configuration["Token:Issuer"],
        ValidateIssuer = true,
        ValidateAudience = true,
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ManagerOnly", policy =>
    {
        policy.RequireRole("Manager");
    });

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x

          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
