using Azurite.EventSourcingExample.Services;
using Azurite.EventSourcingExample.Services.Abstract;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;

namespace Azurite.EventSourcingExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpClient<IEventService, EventService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:7071");
            });

            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}