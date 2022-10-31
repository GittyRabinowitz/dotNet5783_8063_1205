

namespace Dal.DO;

    public struct OrderItem: IDataObject
{
    public int ID { get; set; }
    public int ProductID { get; set; }
    public int OrderID { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }

    public override string ToString() => $@"
OrderItem ProductID={ProductID}: 
OrderID - {OrderID}
    	Price: {Price}
    	Amount: {Amount}
";
}

