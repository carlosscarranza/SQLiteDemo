using SQLite;

namespace SQLiteDemo.Data
{
    public interface ISQLiteDB
    {
        SQLiteAsyncConnection GetConnection();
    }
}
