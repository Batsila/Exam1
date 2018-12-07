using System.ServiceModel;

namespace ServerApp
{
    //[ServiceContract(CallbackContract = typeof(ICommunicationServiceCallBack))]
    public interface ICommunicationService
    {
        [OperationContract]
        void Disconnect(UserData user);
        [OperationContract]
        void Connect(UserData user);
        [OperationContract]
        void DeleteUser(UserData user);
    }
}
