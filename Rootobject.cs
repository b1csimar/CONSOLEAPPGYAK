// See https://aka.ms/new-console-template for more information
public class Rootobject
{
    public DateTime export_date { get; set; }
    public string shop_name { get; set; }
    public Customer[] customers { get; set; }
    public Part[] parts { get; set; }
    public Order[] orders { get; set; }
    public Summary summary { get; set; }
}

