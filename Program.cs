using Microsoft.EntityFrameworkCore;
using Pharmacy.Data;

//(responsible to get up the app)
var builder = WebApplication.CreateBuilder(args);

//It enables API endpoints defined in controller classes 
builder.Services.AddControllers();

//Configure SQL Server
builder.Services.AddDbContext<PrescriptionContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();