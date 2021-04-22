using ServiceStack;
using Example1.ServiceModel;
using MassTransit;

namespace Example1.ServiceInterface
{
    public class MyServices : Service
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MyServices(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        
        public object Any(Hello request)
        {
            return new HelloResponse { Result = $"Hello, {request.Name}!" };
        }
    }
}
