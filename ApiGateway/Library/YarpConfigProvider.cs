using Microsoft.Extensions.Primitives;
using Yarp.ReverseProxy.Configuration;

namespace ApiGateway.Library
{
    public class YarpConfigProvider : IProxyConfigProvider
    {
        private readonly InMemoryConfig _yarpConfig;

        public YarpConfigProvider()
        {
            _yarpConfig = new InMemoryConfig();
        }

        public IProxyConfig GetConfig()
        {
            return _yarpConfig;
        }

        
    }

    public class InMemoryConfig : IProxyConfig
    {
        public IReadOnlyList<RouteConfig> Routes => throw new NotImplementedException();

        public IReadOnlyList<ClusterConfig> Clusters => throw new NotImplementedException();

        public IChangeToken ChangeToken => throw new NotImplementedException();
    }
}
