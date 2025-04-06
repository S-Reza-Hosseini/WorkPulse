using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WorkPulse.Persistence.DataContext;
using WorkPulse.RestApi.Config;


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


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()); 

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new DependencyInjectionModule());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapScalarApiReference();
    app.MapControllers();
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseHttpsRedirection();


app.Run();
