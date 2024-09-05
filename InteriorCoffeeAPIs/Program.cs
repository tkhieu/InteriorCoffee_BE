using InteriorCoffee.Application.Constants;
using InteriorCoffeeAPIs.Extensions;
using InteriorCoffeeAPIs.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsConstant.PolicyName,
        policy => { policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod(); });
});

builder.Services.AddControllers();
builder.Services.AddDatabase(builder.Configuration);
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
