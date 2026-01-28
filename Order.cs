// See https://aka.ms/new-console-template for more information
public class Order
{
    public int order_id { get; set; }
    public int customer_id { get; set; }
    public DateTime order_date { get; set; }
    public string status { get; set; }
    public string shipping_method { get; set; }
    public string payment_method { get; set; }
    public string notes { get; set; }
    public Item[] items { get; set; }
    public int subtotal { get; set; }
    public int shipping_cost { get; set; }
    public int total { get; set; }
}

