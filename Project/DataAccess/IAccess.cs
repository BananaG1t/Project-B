interface IAccess<T>
{
    int Write(T model);
    void Update(T model);
    void Delete(int id, string tableName);
    T? GetById(int id, string tableName);
    List<T> GetAll(string tableName);
}

interface IHasID
{
    int ID { get; set; }
}