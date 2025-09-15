using System.Reflection;
using Serilog;
using StreamPlay.Api.Extensions;
using StreamPlay.Common.Application;
using StreamPlay.Common.Presentation.Endpoints;
using StreamPlay.Module.Users.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    // builder.Host.UseSerilog((context, loggerConfig) =>
    //     loggerConfig
    //         .ReadFrom
    //         .Configuration(context.Configuration));

    builder.Services.AddProblemDetails();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    Assembly[] modulesApplicationAssemblies = [
        StreamPlay.Module.Users.Application.AssemblyReference.Assembly,
    ];

    builder.Services.AddApplication(modulesApplicationAssemblies);

    builder.Configuration.AddModuleConfiguration(["users"]);
    builder.Services.AddUsersModules(builder.Configuration);
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.MapEndpoints();

    app.Run();
}
