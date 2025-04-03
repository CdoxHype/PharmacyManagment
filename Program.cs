using Microsoft.EntityFrameworkCore;
using Pharmacy.Data;
using Pharmacy.Services;
using Microsoft.OpenApi; //Required for swagger

//(responsible to get up the app) intializes the app 
var builder = WebApplication.CreateBuilder(args);

//It enables API endpoints defined in controller classes 
builder.Services.AddControllers();

//Configure SQL Server
builder.Services.AddDbContext<PrescriptionContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));//Retrives crendential from appsettings.json

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Ensuring the database is created and migrated


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//Adding this line to handle the request to "/" 
app.MapGet("/",() => "Welcome to the Pharmacy Benefit Manager API!");

app.Run();