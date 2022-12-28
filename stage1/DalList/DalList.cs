
using DalApi;
using Dal.UseObjects;

namespace Dal;


/// <summary>
/// this class is singlethon in order to instnce the class only once 
/// the class is also threadsafe using "lock" and also Lazy Initialization
/// </summary>


internal sealed class DalList : IDal
{
    private static Lazy<IDal>? instance;
    public static IDal Instance { get { return GetInstence(); } }

    public static IDal GetInstence()
    {
        lock (instance??=new Lazy<IDal>(() => new DalList())) // thread safe
        {
            return instance.Value;
        }
    }


    private DalList() { }
    public IProduct Product => new DalProduct() { };
    public IOrder Order => new DalOrder() { };
    public IOrderItem OrderItem => new DalOrderItem() { };
}

