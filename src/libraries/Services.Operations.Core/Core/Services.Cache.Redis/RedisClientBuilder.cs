using Services.Cache.Contracts;
using StackExchange.Redis;

namespace Services.Cache.Redis
{
    sealed class RedisClientBuilder
    {
        IRedisCacheProviderConfiguration _config;

        internal RedisClientBuilder (IRedisCacheProviderConfiguration config)
        {
            _config = config;
        }

        internal IConnectionMultiplexer Build ()
        {
            return ConnectionMultiplexer.Connect(ConnectionString);
        }

        string ConnectionString
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_config.ConnectionString))
                {
                    return _config.ConnectionString;
                }

                return $"{_config.Host}:{Port};{Password}{IsSsl}abortConnect=False";
            }
        }

        int Port
        {
            get
            {
                if (_config.Port > 0)
                {
                    return _config.Port;
                }

                if (!string.IsNullOrWhiteSpace(_config.ConnectionString) || _config.Host.Equals("localhost"))
                {
                    return 6379;
                }
                else
                {
                    return 6380;
                }
            }
        }

        string IsSsl
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_config.ConnectionString) || _config.Host.Equals("localhost"))
                {
                    return "ssl=False;";
                }
                else
                {
                    return "ssl=True;";
                }
            }
        }

        string Password
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_config.ConnectionString) || _config.Host.Equals("localhost"))
                {
                    return string.Empty;
                }

                return $"password={_config.Password};";
            }
        }
    }
}
