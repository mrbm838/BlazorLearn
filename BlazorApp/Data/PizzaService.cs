using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BlazorApp.Data
{
    public class PizzaService
    {
        public async Task<Pizza[]> GetPizzasAsync()
        {
            var pizzas = new List<Pizza>();
            //var connectionString = "Server=(localdb)\\mssqllocaldb;Database=BlazorDB;Integrated Security=True;";
            var connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = E:\Program\Blazor\BlazorApp\BlazorDB.mdf; Integrated Security=True;";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT PizzaId, Name, Description, Price, Vegetarian, Vegan FROM dbo.Pizza";
                using (var command = new SqlCommand(sql, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var pizza = new Pizza
                            {
                                PizzaId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                Price = reader.GetDecimal(3),
                                Vegetarian = reader.GetBoolean(4),
                                Vegan = reader.GetBoolean(5)
                            };
                            pizzas.Add(pizza);
                        }
                    }
                }
            }

            return pizzas.ToArray();
        }
    }
}
