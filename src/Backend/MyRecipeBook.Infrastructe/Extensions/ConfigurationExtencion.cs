using Microsoft.Extensions.Configuration;

namespace MyRecipeBook.Infrastructe.Extensions
{
    public static class ConfigurationExtencion
    {

        public static string ConnectionString(this  IConfiguration configuration)
        {
          return configuration.GetConnectionString("Connection")!;   
        }
    }
}
