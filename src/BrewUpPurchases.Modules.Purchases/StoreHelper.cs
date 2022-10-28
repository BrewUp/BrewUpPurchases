using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Events;
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Validators;
using BrewUpPurchases.Modules.Purchases.Abstracts;
using BrewUpPurchases.Modules.Purchases.Concretes;
using BrewUpPurchases.Modules.Purchases.EventsHandlers;
using BrewUpPurchases.Modules.Purchases.Factories;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Factories;
using Muflone.Messages.Events;

namespace BrewUpPurchases.Modules.Purchases;

public static class StoreHelper
{
    public static IServiceCollection AddStoreModule(this IServiceCollection services)
    {
        services.AddScoped<ValidationHandler>();
        services.AddFluentValidation(options =>
            options.RegisterValidatorsFromAssemblyContaining<OrdineFornitoreValidator>());

        services.AddScoped<IStoreOrchestrator, StoreOrchestrator>();
        services.AddScoped<IIngredientsService, IngredientsService>();
        services.AddScoped<IStoreService, StoreService>();

        services.AddScoped<IDomainEventHandlerFactoryAsync, DomainEventHandlerFactoryAsync>();
        services.AddScoped<ICommandHandlerFactoryAsync, CommandHandlerFactoryAsync>();

        services.AddScoped<IDomainEventHandlerAsync<OrdineFornitoreInserito>, OrdineFornitoreInseritoEventHandler>();

        return services;
    }
}