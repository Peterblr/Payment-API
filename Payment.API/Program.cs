using Microsoft.EntityFrameworkCore;
using Payment.API.Data;
using Payment.API.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "PaymentsDetail",
        policy =>
            policy.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin()
        );
});

// Add services to the container.

builder.Services.AddDbContext<PaymentDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("PaymentsDetail");

app.MapControllers();

app.Run();
