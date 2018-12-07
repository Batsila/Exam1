using System.ServiceModel;

namespace ServerApp
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    public class CommunicationService : ICommunicationService 
    {
        private readonly UserService _userService;
        public CommunicationService()
        {
            _userService = new UserService();

        }

        public void Connect(UserData user)
        {
            var connection = OperationContext.Current.GetCallbackChannel<ICommunicationServiceCallBack>();
            _userService.AddNewItem(user);
            connection.Connect();
        }

        public void DeleteUser(UserData user)
        {
            _userService.RemoveItem(user);
            var connection = OperationContext.Current.GetCallbackChannel<ICommunicationServiceCallBack>();
            connection.Disconnect();
        }

        public void Disconnect(UserData user)
        {
            _userService.Disconnect(user);
            var connection = OperationContext.Current.GetCallbackChannel<ICommunicationServiceCallBack>();
            connection.Disconnect();
        }

    }
}
