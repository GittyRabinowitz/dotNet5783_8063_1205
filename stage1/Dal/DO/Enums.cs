

namespace Dal.DO;

    public enum eCategory
    {

    }

public enum eEntityOptions { exit, product, order, orderItem};

public enum eCrudOptions { add=1, viewById, viewAll, update, delete };

public enum eOrderItemOptions { add=1, viewById, viewAll, update, delete , viewByorderIdAndProductId, viewByOrderId};
