using System.ServiceModel;

namespace ServerApp
{
    public interface ICommunicationServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void Disconnect();

        [OperationContract]
        void Connect();
    }
}
