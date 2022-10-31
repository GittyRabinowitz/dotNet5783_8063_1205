

namespace Dal.DO;

    public struct Order: IDataObject
{

    public int ID;
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryDate { get; set; }

    public override string ToString() => $@"
Product ID={ID}: {CustomerName}, 
CustomerEmail - {CustomerEmail}
    	CustomerAdress: {CustomerAdress}
    	OrderDate: {OrderDate}
        ShipDate: {ShipDate}
        DeliveryDate: {DeliveryDate}
";
}

