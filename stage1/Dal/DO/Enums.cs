
/// <summary>
/// defining a few enums
/// </summary>

namespace Dal.DO;

public enum eCategory { kitchen = 1, washRoom, otherRoom };

public enum eEntityOptions { Exit, Product, Order, OrderItem };

public enum eCrudOptions { Add = 1, ViewById, ViewAll, Update, Delete };

public enum eOrderItemOptions { Add = 1, ViewById, ViewAll, Update, Delete, ViewByorderIdAndProductId, ViewByOrderId };
