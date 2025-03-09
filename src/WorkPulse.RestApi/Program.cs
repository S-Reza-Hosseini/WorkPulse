using Microsoft.EntityFrameworkCore;
using WorkPulse.Persistence.DataContext;
using WorkPulse.Persistence.UnitOfWorks;
using WorkPulse.Persistence.Users;
using WorkPulse.Services.UnitOfWorks;
using WorkPulse.Services.Users;
using WorkPulse.Services.Users.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddDbContext<EfDataContext>(
        option => option.UseSqlServer(
            builder.Configuration
                .GetConnectionString(
                    "DefaultConnection")));

builder.Services.AddScoped<IUserService, UserAppService>();
builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();

builder.Services.AddScoped<IUserRepository, EfUserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapControllers();
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseHttpsRedirection();


app.Run();
