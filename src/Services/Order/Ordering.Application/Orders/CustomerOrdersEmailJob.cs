using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;

namespace Ordering.Application.Orders;

public class CustomerOrdersEmailJob 
{
    private IOrderDbContext orderDbContext;
    public CustomerOrdersEmailJob(IOrderDbContext context)
    {
        orderDbContext = context;
    }
    public async Task RunJob() 
    {
        var customers = await orderDbContext.Customers
                     .ToListAsync();

        var toDate = DateTime.Now;
        var fromDate = toDate.AddMonths(-1);

        foreach (var customer in customers)
        {
            var orders = await orderDbContext.Orders
                .AsNoTracking()
                .Where(o => o.CustomerId == customer.Id)
                .Where(o => o.CreatedAt >= fromDate && o.CreatedAt <= toDate)
                .OrderBy(o => o.OrderName)
                .ToListAsync();

            //Simulating email
            Console.WriteLine($"Dear Customer {customer.FirstName}, Your have ordered {orders.Count} in the previous month!");

        }
    }
}
