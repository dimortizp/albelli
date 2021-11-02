
using Microsoft.Extensions.Configuration;

namespace Api
{
    public class ApiSettings
    {
        public string ConnectionString { get; set; }

        public ApiSettings(IConfiguration configuration)
        {
            configuration.Bind(this);
        }
    }
}
