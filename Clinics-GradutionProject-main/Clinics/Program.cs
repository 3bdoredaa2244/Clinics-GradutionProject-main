 using Clinics.Data;
using Clinics.EF.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System.Text;
using Clinics.Core.Interfaces;
using Clinics.EF;
using Clinics.Core;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using Clinics.Core.Models.Authentication;
using Microsoft.OpenApi.Models;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddMvc().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
//adding SignalR
builder.Services.AddSignalR();

//Add Identity 
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(config =>
{
    //config.SignIn.RequireConfirmedAccount = false;
    config.User.RequireUniqueEmail = true;
    config.Tokens.AuthenticatorIssuer = "JWT";

    // Add this line for Email confirmation
    config.SignIn.RequireConfirmedEmail = true;

}).AddEntityFrameworkStores<ClinicContext>()
.AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider); ;
//configue identity passwrod options
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;

});

//Add JWT Authentication
builder.Configuration.GetSection("JWT");
//JWT Configuration DefaultAuthenticateScheme
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
   .AddJwtBearer(opt =>
    {
        opt.RequireHttpsMetadata = false;
        opt.SaveToken = false;
        opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };

    });

//Maping between IAuthService and AuthService
//if you are not using Unit of work use this
//builder.Services.AddTransient(typeof(IRecipe<>), typeof(Recipe<>));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();




//To use DB Context
builder.Services.AddDbContext<ClinicContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ClinicConnection"),
    b => b.MigrationsAssembly(typeof(ClinicContext).Assembly.FullName)));

// To use AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//Patch Json
builder.Services.AddControllersWithViews().AddNewtonsoftJson(s => {
    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

//hangFiiiiiiiiiiire
builder.Services.AddHangfire(x =>x.UseSqlServerStorage("Server=DESKTOP-5SUCKG0;Initial Catalog=ClinicDB;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true;user ID =CommanderAPI;Password=SQL1234"));
builder.Services.AddHangfireServer();




var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{    
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors(policy => policy
    .WithOrigins("http://localhost:4200")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials());


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard("/dashboard");
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotificationHub>("/notificationHub");
    // other endpoints...
});

app.MapControllers();
app.MapRazorPages();

app.Run();


