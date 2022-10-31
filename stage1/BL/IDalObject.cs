
using Dal.DO;
namespace Dal.UseObjects;

    public interface IDalObject
    {

    int Create(IDataObject obj); // כאן אא לרשום תוכן בפונקציה

    IDataObject ReadSingle(int Id);
    IDataObject[] Read();

    void Update(IDataObject obj);

    void Delete(int Id);
}

