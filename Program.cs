string filepath = "autoparts_orders.json";
string jsonString = File.ReadAllText(filepath);

Rootobject root = System.Text.Json.JsonSerializer.Deserialize<Rootobject>(jsonString)!;

var customers = new List<Customer>();
foreach (var item in root.customers)
{
    customers.Add(item);
}

var order = new List<Order>();
foreach (var item in root.orders)
{
    order.Add(item);
}

var topCustomer = order
    .GroupBy(o => o.customer_id)
    .Select(g => new
    {
        CustomerId = g.Key,
        TotalAmount = g.Sum(o => o.subtotal)
    })
    .OrderByDescending(x => x.TotalAmount)
    .FirstOrDefault();
var customerDetails = customers.FirstOrDefault(c => c.customer_id == topCustomer.CustomerId);
Console.WriteLine($"Top Customer: {customerDetails.name}, Total Spent: {topCustomer.TotalAmount}");


var mostsoldproduct = order
    .SelectMany(o => o.items)
    .GroupBy(i => i.part_id)
    .Select(g => new
    {
        PartId = g.Key,
        TotalQuantity = g.Sum(i => i.quantity)
    })
    .OrderByDescending(x => x.TotalQuantity)
    .FirstOrDefault();
Console.WriteLine($"Most Sold Product ID: {mostsoldproduct.PartId}, Total Quantity Sold: {mostsoldproduct.TotalQuantity}");


var summarizepayments = order
    .GroupBy(o => o.payment_method)
    .Select(g => new
    {
        PaymentMethod = g.Key,
        CustomerCount = g.Select(o => o.customer_id).Distinct().Count(),
        TotalAmount = g.Sum(o => o.subtotal)
    });
foreach (var item in summarizepayments)
    {
    Console.WriteLine($"Payment Method: {item.PaymentMethod}, Customers: {item.CustomerCount}, Total Amount: {item.TotalAmount}");
}

var summarizeorders = order
    .GroupBy(o => o.shipping_method)
    .Select(g => new
    {
        ShippingMethod = g.Key,
        TransactionCount = g.Count(),
        TotalAmount = g.Sum(o => o.subtotal)
    });
foreach (var item in summarizeorders)
    {
    Console.WriteLine($"Shipping Method: {item.ShippingMethod}, Transactions: {item.TransactionCount}, Total Amount: {item.TotalAmount}");
}

public class Summary
{
    public int total_orders { get; set; }
    public int total_customers { get; set; }
    public int total_part_types { get; set; }
    public int total_revenue { get; set; }
}