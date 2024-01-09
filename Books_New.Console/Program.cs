using Azure;
using Books_New.ContextFactory;
using Books_New.Extensions;
using Entities.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Service;
using Service.Contracts;
using System;

var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

var configuration = builder.Build();

var services = new ServiceCollection();

services.ConfigureRepositoryManager();
services.ConfigureServiceManager();
services.ConfigureSqlContext(configuration);

var serviceProvider = services.BuildServiceProvider();

Console.WriteLine("Enter path to file");
string path = Console.ReadLine();

Update(path, serviceProvider);

static void Update(string path, ServiceProvider servicesProvider)
{
    while (!File.Exists(path))
    {
        Console.WriteLine("Specify a valid file path or press C to exit.");

        path = Console.ReadLine();

        if (path.ToUpper() == "C")
        {
            Environment.Exit(0);
        }
    }
    if (path != null && File.Exists(path))
    {
        Console.Write("File detected\n");
        string[] lines = File.ReadAllLines(path);
        var serviceManager = servicesProvider.GetService<IServiceManager>();

        foreach (var line in lines)
        {
            string[] fields = line.Split(',');

            if (DataCheck(fields))
            {
                try
                {
                    serviceManager.AuthorService.CreateAuthor(fields[4].ToString());
                    Console.WriteLine($"Author: {fields[4]} has been added");
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Author: {fields[4]} is duplicate!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
}

//0-title 1-pages 2-genre 3-ReleaseData 4-Author 5-publisher

static bool DataCheck(string[] fields)
{
    if (fields[0] is string && int.TryParse(fields[1], out int page) && fields[2] is string && DateTime.TryParse(fields[3], out DateTime time) && fields[4] is string && fields[5] is string)
    {
        return true;
    }

    return false;
}



