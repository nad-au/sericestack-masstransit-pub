using System;
using Funq;
using ServiceStack;
using Example1.ServiceInterface;
using MassTransit;

namespace Example1
{
    //VS.NET Template Info: https://servicestack.net/vs-templates/EmptyAspNet
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Base constructor requires a Name and Assembly where web service implementation is located
        /// </summary>
        public AppHost()
            : base("Example1", typeof(MyServices).Assembly) { }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        public override void Configure(Container container)
        {
            container.Register(c => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            }));
            
            container.Register<IPublishEndpoint>(c => c.Resolve<IBusControl>());
            
            var busControl = container.Resolve<IBusControl>();
            busControl.Start(TimeSpan.FromSeconds(5));
        }
    }
}
