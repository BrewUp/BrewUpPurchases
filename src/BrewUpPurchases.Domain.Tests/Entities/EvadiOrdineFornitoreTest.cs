using BrewUpPurchases.Domain.CommandHandlers;
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Commands;
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.CustomTypes;
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Events;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;

namespace BrewUpPurchases.Domain.Tests.Entities;

public sealed class EvadiOrdineFornitoreTest : CommandSpecification<EvadiOrdineFornitore>
{
    private readonly OrderId _orderId = new(Guid.NewGuid());
    private readonly OrderNumber _orderNumber = new("20221028-01");

    private readonly Guid _correlationId = Guid.NewGuid();

    private readonly Fornitore _fornitore = new Fornitore(new FornitoreId(Guid.NewGuid().ToString()), new DenominazioneFornitore("Fornitore"));

    private readonly DataInserimento _dataInserimento = new(DateTime.UtcNow);
    private readonly DataPrevistaConsegna _dataPrevistaConsegna = new(DateTime.UtcNow.AddDays(30));
    private readonly DataEffettivaConsegna _dataEffettivaConsegna = new(DateTime.UtcNow.AddDays(50));

    private readonly IEnumerable<OrderRow> _rows = Enumerable.Empty<OrderRow>();

    public EvadiOrdineFornitoreTest()
    {
        _rows = _rows.Concat(new List<OrderRow>
        {
            new (new RowId(Guid.NewGuid().ToString()), new Ingredient(new IngredientId(Guid.NewGuid().ToString()), new IngredientName("Malt")), new Quantity(10))
        });
    }

    protected override IEnumerable<DomainEvent> Given()
    {
        yield return new OrdineFornitoreInserito(_orderId, _correlationId, _orderNumber, _fornitore, _dataInserimento,
        _dataPrevistaConsegna, _rows);
    }

    protected override EvadiOrdineFornitore When()
    {
        return new EvadiOrdineFornitore(_orderId, _dataEffettivaConsegna, _rows);
    }

    protected override ICommandHandlerAsync<EvadiOrdineFornitore> OnHandler()
    {
        return new EvadiOrdineFornitoreCommandHandler(Repository, new NullLoggerFactory());
    }

    protected override IEnumerable<DomainEvent> Expect()
    {
        yield return new OrdineFornitoreEvaso(_orderId, _dataEffettivaConsegna, _rows);
    }
}