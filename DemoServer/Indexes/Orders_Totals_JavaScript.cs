using Raven.Client.Documents.Indexes;

namespace DemoServer.Indexes
{
    public class Orders_Totals_JavaScript : AbstractJavaScriptIndexCreationTask
    {
        public Orders_Totals_JavaScript()
        {
            Maps.Add(
                @"map('Orders', (order) => {
                    var total = 0;
                    if (order.Lines) {
                        order.Lines.forEach(function(l) {
                            total += (l.Quantity * l.PricePerUnit) * (1 - l.Discount);
                        });
                    }
            
                    return {
                        Employee : order.Employee,
                        Company : order.Company,
                        Total : total
                    }
                })"
                );
        }
    }
}