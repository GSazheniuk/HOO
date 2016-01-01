using System.ServiceModel;

namespace HOO.ComLib
{
    public class BackServiceHelper
    {
        public static IHOOService ConnectToBackService()
        {
            string serviceAddress = "net.tcp://localhost:8000/HOOService";

            NetTcpBinding z = new NetTcpBinding(SecurityMode.None);
            z.MaxReceivedMessageSize = int.MaxValue;
            z.MaxBufferSize = int.MaxValue;
            z.MaxBufferPoolSize = int.MaxValue;
            z.MaxConnections = 200;
            CallbackHandler clientCallback = new CallbackHandler();

            ChannelFactory<IHOOService> factory = new ChannelFactory<IHOOService>(
                z,
                new EndpointAddress(serviceAddress));

            return factory.CreateChannel();
        }
    }
}
