﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClientApp.ServerConnectionService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServerConnectionService.IServerConnectionService")]
    public interface IServerConnectionService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServerConnectionService/AddUser", ReplyAction="http://tempuri.org/IServerConnectionService/AddUserResponse")]
        void AddUser(string firstName, string lastName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServerConnectionService/AddUser", ReplyAction="http://tempuri.org/IServerConnectionService/AddUserResponse")]
        System.Threading.Tasks.Task AddUserAsync(string firstName, string lastName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServerConnectionServiceChannel : ClientApp.ServerConnectionService.IServerConnectionService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServerConnectionServiceClient : System.ServiceModel.ClientBase<ClientApp.ServerConnectionService.IServerConnectionService>, ClientApp.ServerConnectionService.IServerConnectionService {
        
        public ServerConnectionServiceClient() {
        }
        
        public ServerConnectionServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServerConnectionServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServerConnectionServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServerConnectionServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void AddUser(string firstName, string lastName) {
            base.Channel.AddUser(firstName, lastName);
        }
        
        public System.Threading.Tasks.Task AddUserAsync(string firstName, string lastName) {
            return base.Channel.AddUserAsync(firstName, lastName);
        }
    }
}