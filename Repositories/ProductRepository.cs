using Dapper;
using Microsoft.Data.Sqlite;
using Avaliacao3BimLp3.Models;
using Avaliacao3BimLp3.DataBase;

namespace Avaliacao3BimLp3.Repositories;

class ProductRepository
{
    private readonly DatabaseConfig _databaseConfig;

    public ProductRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    // Insere um produto na tabela
    public Product Save(Product product)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("INSERT INTO Products VALUES (@Id, @Name, @Price, @Active)", product);

        return product;
    }

    // Deleta um produto na tabela
    public void Delete(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Products WHERE id == @Id", new{Id = id});
    }

    // Retorna todos os produtos
    public List<Product> GetAll()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();   

        var products = connection.Query<Product>("SELECT * FROM Products").ToList();

        return products;
    }

    // Habilita um produto
    public void Enable(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Products SET active = true WHERE id = @Id",  new{Id = id});
    }

    //Desabilita um produto
    public void Disable(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Products SET active = false WHERE id = @Id", new{Id = id});

    }

    public bool ExistsById(int id)  //Confere pelo id se um produto existe no banco de dados 
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        
        bool result = connection.ExecuteScalar<bool>("SELECT count(id) FROM Products WHERE id = @Id", new {Id = id});

        return result;
    }

    // Retorna os produtos dentro de um intervalo de preço
    public List<Product> GetAllWithPriceBetween(double initialPrice, double endPrice)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var products = connection.Query<Product> ("SELECT * FROM Products WHERE price BETWEEN @InitialPrice AND @EndPrice", new { InitialPrice = initialPrice, EndPrice = endPrice }).ToList();

        return products;
    }

    // Retorna os produtos com preço acima de um preço especificado
    public List<Product> GetAllWithPriceHigherThan(double price)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var products = connection.Query<Product> ("SELECT * FROM Products WHERE price > @Price", new {Price = price}).ToList();

        return products;
    }

    // Retorna os produtos com preço abaixo de um preço especificado
    public List<Product> GetAllWithPriceLowerThan(double price)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var products = connection.Query<Product>("SELECT * FROM Products WHERE price < @Price", new {Price = price}).ToList();

        return products;
    }

    // Retorna a média dos preços dos produtos
    public double GetAveragePrice()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var average = connection.QuerySingle<double>("SELECT AVG (price) FROM Products");

        return average;
    }
}