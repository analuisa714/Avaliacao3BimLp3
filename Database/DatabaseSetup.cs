using Microsoft.Data.Sqlite;

namespace Avaliacao3BimLp3.DataBase;

class DatabaseSetup
{
    private readonly DatabaseConfig _databaseConfig;

    public DatabaseSetup(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
        CreateProductTable();
    }

    private void CreateProductTable()
    {
        var connection = new SqliteConnection (_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
        CREATE TABLE IF NOT EXISTS Products(
        id int null primary key,
        name varchar(100) not null,
        price double not null,
        active bool not null);
        ";

        command.ExecuteNonQuery();
        connection.Close();
    }
}