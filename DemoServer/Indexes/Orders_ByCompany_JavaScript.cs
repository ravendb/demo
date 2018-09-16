using Raven.Client.Documents.Indexes;

namespace DemoServer.Indexes
{
    public class Orders_ByCompany_JavaScript : AbstractJavaScriptIndexCreationTask
    {
        public Orders_ByCompany_JavaScript()
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
                        Company : order.Company,
                        Count : 1,
                        Total : total
                    }
                })"
            );

            Reduce =
                @"groupBy(x => ({ Company : x.Company }))
                    .aggregate(g => { 
                        return {
                            Company : g.key.Company,
                            Count : g.values.reduce((count, val) => val.Count + count, 0),
                            Total : g.values.reduce((total, val) => val.Total + total, 0)
                        };
                    })";
        }
    }
}