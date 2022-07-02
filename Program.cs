using Avaliacao3BimLp3.Models;
using Avaliacao3BimLp3.DataBase;
using Avaliacao3BimLp3.Repositories;

var databaseConfig = new DatabaseConfig();

var databaseSetup = new DatabaseSetup(databaseConfig);

var productRepository = new ProductRepository (databaseConfig);

var modelName = args[0];
var modelAction = args[1];

if (modelName == "Product")
{
        if (modelAction == "List")
        {
           if (productRepository.GetAll().Any())
            {
                foreach (var product in productRepository.GetAll())
                {
                    Console.WriteLine(
                            "{0}, {1}, {2}, {3}", product.Id, product.Name, product.Price, product.Active
                            );
                }
            }
            else
            {
                Console.WriteLine("Nenhum produto cadastrado");
            }  
        }

        if (modelAction == "New") 
        {
            var id = Convert.ToInt32(args [2]);
            var name = args [3];
            var price = Convert.ToDouble(args [4]);
            var active = Convert.ToBoolean(args [5]);

            var product = new Product(id, name, price, active);

            if (productRepository.ExistsById(id))
            {
                Console.WriteLine("Produto {0} já existe", product.Name);
            }
            else
            {            
                productRepository.Save(product);

                Console.WriteLine("Produto {0} cadastrado com sucesso", product.Name);
            }
        }
            
        
        if (modelAction == "Delete") 
        {
                var id = Convert.ToInt32(args [2]);
                
                if (productRepository.ExistsById(id))
                {
                    productRepository.Delete(id);

                    Console.WriteLine("Produto {0} removido com sucesso", id);
                }
                else
                {           
                    Console.WriteLine("Produto {0} não encontrado", id);
                }
        }

        if (modelAction == "Enable") 
        {
            var id = Convert.ToInt32(args [2]);

            if (productRepository.ExistsById(id))
            {
                productRepository.Enable(id);

                Console.WriteLine("Produto {0} habilitado com sucesso", id);
            }
            else
            {           
                Console.WriteLine("Produto {0} não encontrado", id);
            }     
        }

        if (modelAction == "Disable") 
        {
            var id = Convert.ToInt32(args [2]);

            if (productRepository.ExistsById(id))
            {  
                productRepository.Disable(id);

                Console.WriteLine("Produto {0} desabilitado com sucesso", id);
            }
            else
            {           
                Console.WriteLine("Produto {0} não encontrado", id);
            }           
        }

        if (modelAction == "PriceBetween") 
        {
            var initialPrice = Convert.ToDouble(args [2]);
            var endPrice = Convert.ToDouble(args [3]);

            if (productRepository.GetAllWithPriceBetween(initialPrice, endPrice).Any())
            {
                foreach (var product in productRepository.GetAllWithPriceBetween(initialPrice, endPrice))
                {
                    Console.WriteLine(
                            "{0}, {1}, {2}, {3}", product.Id, product.Name, product.Price, product.Active
                            );
                }
            } 
            else
            {
                Console.WriteLine("Nenhum produto encontrado dentro do intervalo de preço R$ {0} e R$ {1}.", initialPrice, endPrice);
            }      
        }

        if (modelAction == "PriceHigherThan") 
        {
            var price = Convert.ToDouble(args [2]);

            if (productRepository.GetAllWithPriceHigherThan(price).Any())
            {
                foreach (var product in productRepository.GetAllWithPriceHigherThan(price))
                {
                    Console.WriteLine(
                            "{0}, {1}, {2}, {3}", product.Id, product.Name, product.Price, product.Active
                            );
                }
            } 
            else
            {
                Console.WriteLine("Nenhum produto encontrado com preço maior que R$ {0}.", price);
            }      
        }

        if (modelAction == "PriceLowerThan") 
        {
            var price = Convert.ToDouble(args [2]);

            if (productRepository.GetAllWithPriceLowerThan(price).Any())
            {
                foreach (var product in productRepository.GetAllWithPriceLowerThan(price))
                {
                    Console.WriteLine(
                            "{0}, {1}, {2}, {3}", product.Id, product.Name, product.Price, product.Active
                            );
                }
            } 
            else
            {
                Console.WriteLine("Nenhum produto encontrado com preço menor que R$ {0}.", price);
            }      
        }

        if (modelAction == "AveragePrice") 
        {
            if (productRepository.GetAll().Any())
            {
                Console.WriteLine("A média dos preços é R$ {0}", productRepository.GetAveragePrice());
            } 
            else
            {
                Console.WriteLine("Nenhum produto cadastrado.");
            }      
        }
}