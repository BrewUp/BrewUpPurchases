using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Commands;
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.CustomTypes;

namespace BrewUpPurchases.Modules.BrewUpPurchases.Tests
{
    public class CommandValidation
    {
        [Fact]
        public void Can_Validate_CreateOrder()
        {
            var orderId = Guid.NewGuid();
            var creaOrdine = new CreaOrdineFornitore(new OrderId(orderId), Guid.NewGuid(), new OrderNumber("123"),
                new Fornitore(new FornitoreId("456"), new DenominazioneFornitore("Pippo")),
                new DataInserimento(DateTime.Today), new DataPrevistaConsegna(DateTime.Today.AddDays(30)),
                Enumerable.Empty<OrderRow>());

            Assert.Equal(orderId, creaOrdine.OrderId.Value);
        }
    }
}