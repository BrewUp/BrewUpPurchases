using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Commands;
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Events;
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Validators;
using BrewUpPurchases.Modules.Purchases.Abstracts;
using BrewUpPurchases.Modules.Purchases.Concretes;
using BrewUpPurchases.Modules.Purchases.EventsHandlers;
using BrewUpPurchases.Modules.Purchases.Factories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Factories;
using Muflone.Messages.Events;

namespace BrewUpPurchases.Modules.Purchases;

public static class PurchaseHelper
{
    public static IServiceCollection AddPurchaseModule(this IServiceCollection services)
    {
        services.AddScoped<ValidationHandler>();

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<OrdineFornitoreValidator>();

        services.AddScoped<IStoreOrchestrator, StoreOrchestrator>();
        services.AddScoped<IIngredientsService, IngredientsService>();
        services.AddScoped<ISupplierExpositionService, SupplierExpositionService>();
        services.AddScoped<IPurchaseService, PurchaseService>();

        services.AddScoped<IDomainEventHandlerFactoryAsync, DomainEventHandlerFactoryAsync>();
        services.AddScoped<ICommandHandlerFactoryAsync, CommandHandlerFactoryAsync>();

        //services.AddScoped<IDomainEventHandlerAsync<OrdineFornitoreInserito>, OrdineFornitoreInseritoEventHandler>();
        //services.AddScoped<IDomainEventHandlerAsync<OrdineFornitoreInserito>, OrdineFornitoreInseritoForExpositionEventHandler>();

        services.AddScoped<IDomainEventHandlerAsync<OrdineFornitoreEvaso>, OrdineFornitoreEvasoEventHandler>();

        return services;
    }
}