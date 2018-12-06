using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServerApp
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CommunicationService : ICommunicationService
    {
        private UserService userService;

        public CommunicationService()
        {
            userService = new UserService();
        }

        private ICommunicationServiceCallBack CallBack
        {
            get { return OperationContext.Current.GetCallbackChannel<ICommunicationServiceCallBack>(); }
        }

        public void Connect(UserData user)
        {
            CallBack.Connect(user);
            userService.AddNewItem(user);
        }

        public void DeleteUser(UserData user)
        {
            userService.RemoveItem(user);
        }

        public void Disconnect(UserData user)
        {
            CallBack.Disconnect(user);
            userService.Disconnect(user);
        }

    }
}
