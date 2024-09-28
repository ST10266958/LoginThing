using Azure;
using Azure.Data.Tables;

namespace AzLogin.Repository
{
    public class UserRepository : IUserRepository

    {
        private readonly TableClient _tableClient;

        public UserRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("AzureTableStorage")["ConnectionString"];
            var tableName = configuration.GetSection("AzureTableStorage")["TableName"];
            _tableClient = new TableClient(connectionString, tableName);
            _tableClient.CreateIfNotExists();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                //Assume RowKey is the email
                var response = await _tableClient.GetEntityAsync<User>("User", email);
                return response.Value;
            }
            catch (RequestFailedException ex) when (ex.Status == 404)
            {
                return null;
            }
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            try
            {
                await _tableClient.AddEntityAsync(user);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
        
    

