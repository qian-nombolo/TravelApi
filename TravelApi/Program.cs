using TravelApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TravelApiCall.Keys;

using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

//if want to continue cors
// var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
//newcodeaddcors
// builder.Services.AddCors(options =>
// {
//  options.AddPolicy(name: MyAllowSpecificOrigins,
//   policy  =>  {
//   policy.WithOrigins("https://localhost:5001;http://localhost:5000"); });
// });
//newcodeaddcors
// app.UseCors(MyAllowSpecificOrigins);

var builder = WebApplication.CreateBuilder(args);

// add cors
builder.Services.AddCors(c=> {
  c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Add services to the container.
builder.Services.AddControllers();

// add database
builder.Services.AddDbContext<TravelApiContext>(
        dbContextOptions => dbContextOptions
          .UseMySql(
            builder.Configuration["ConnectionStrings:DefaultConnection"], 
            ServerVersion.AutoDetect(builder.Configuration["ConnectionStrings:DefaultConnection"]
          )
        )
      );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

//Add versionining
builder.Services.AddApiVersioning(opt =>
  {
    opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1,0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(), new HeaderApiVersionReader("x-api-version"), new MediaTypeApiVersionReader("x-api-version"));

  });

builder.Services.AddVersionedApiExplorer(setup =>
{
  setup.GroupNameFormat = "'v'VVV";
  setup.SubstituteApiVersionInUrl = true;
});

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

// Add Authentication services for JWT tokens 
builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
  options.SaveToken = true;
  options.RequireHttpsMetadata = false;
  options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidAudience = "http://localhost:5000/api/travels",
      ValidIssuer = "http://localhost:5000/api/travels",
      ClockSkew = TimeSpan.Zero,// It forces tokens to expire exactly at token expiration time instead of 5 minutes later
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentVariables.ApiKey))
    };
    
});

var app = builder.Build();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(options => 
  {
    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
    {
      options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
    }
  });
    
}
else
{
  app.UseHttpsRedirection();
}

// allow cors
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
