using InteriorCoffee.Application.Constants;
using InteriorCoffee.Application.Services.Implements;
using InteriorCoffee.Application.Services.Interfaces;
using InteriorCoffee.Domain.Models;
using InteriorCoffee.Infrastructure.Repositories.Implements;
using InteriorCoffee.Infrastructure.Repositories.Interfaces;
using InteriorCoffeeAPIs.Extensions;
using InteriorCoffeeAPIs.Middlewares;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsConstant.PolicyName,
        policy => { policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod(); });
});

builder.Services.AddControllers();

// Register MongoDB client and database
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var connectionString = builder.Configuration.GetSection("MongoDbSection:ConnectionURI").Value;
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new ArgumentNullException(nameof(connectionString), "MongoDB connection string cannot be null or empty.");
    }
    return new MongoClient(connectionString);
});

builder.Services.AddScoped(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var databaseName = builder.Configuration.GetSection("MongoDbSection:DatabaseName").Value;
    if (string.IsNullOrEmpty(databaseName))
    {
        throw new ArgumentNullException(nameof(databaseName), "MongoDB database name cannot be null or empty.");
    }
    return client.GetDatabase(databaseName);
});

builder.Services.Configure<MongoDBContext>(builder.Configuration.GetSection("MongoDbSection"));

// Register repositories and services
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddServices(builder.Configuration);
builder.Services.AddJwtValidation(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddConfigSwagger();

var app = builder.Build();

#region OldCode
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//	app.UseSwagger();
//	app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();
#endregion

app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionHandlingMiddleware>();

//app.UseHttpsRedirection();
app.UseCors(CorsConstant.PolicyName);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
