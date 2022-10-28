﻿using BrewUpPurchases.ReadModel.Abstracts;
using BrewUpPurchases.ReadModel.MongoDb.Repositories;
using BrewUpPurchases.Shared.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Muflone.Eventstore.Persistence;

namespace BrewUpPurchases.ReadModel.MongoDb;

public static class MongoDbHelper
{
    public static IServiceCollection AddEventstoreMongoDb(this IServiceCollection services, MongoDbSettings mongoDbSettings)
    {
        services.AddSingleton<IEventStorePositionRepository>(x =>
            new EventStorePositionRepository(x.GetService<ILogger<EventStorePositionRepository>>(), mongoDbSettings));

        return services;
    }

    public static IServiceCollection AddMongoDb(this IServiceCollection services, MongoDbSettings mongoDbParameter)
    {
        services.AddSingleton<IMongoClient>(provider => new MongoClient(mongoDbParameter.ConnectionString));
        services.AddScoped(provider =>
            provider.GetService<IMongoClient>()
                ?.GetDatabase(mongoDbParameter.DatabaseName)
                .WithWriteConcern(WriteConcern.W1));

        services.AddScoped<IPersister, Persister>();

        return services;
    }
}