﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebUi.AuthentificatorService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserIdentification", Namespace="http://schemas.datacontract.org/2004/07/AuthentificatorService")]
    [System.SerializableAttribute()]
    public partial class UserIdentification : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DisplayNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DisplayName {
            get {
                return this.DisplayNameField;
            }
            set {
                if ((object.ReferenceEquals(this.DisplayNameField, value) != true)) {
                    this.DisplayNameField = value;
                    this.RaisePropertyChanged("DisplayName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AuthentificatorService.IAuthentificatorService")]
    public interface IAuthentificatorService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthentificatorService/SetUserAndGetCookie", ReplyAction="http://tempuri.org/IAuthentificatorService/SetUserAndGetCookieResponse")]
        System.Guid SetUserAndGetCookie(WebUi.AuthentificatorService.UserIdentification userIdentification);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthentificatorService/GetUserByCookie", ReplyAction="http://tempuri.org/IAuthentificatorService/GetUserByCookieResponse")]
        WebUi.AuthentificatorService.UserIdentification GetUserByCookie(System.Guid cookie);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAuthentificatorServiceChannel : WebUi.AuthentificatorService.IAuthentificatorService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AuthentificatorServiceClient : System.ServiceModel.ClientBase<WebUi.AuthentificatorService.IAuthentificatorService>, WebUi.AuthentificatorService.IAuthentificatorService {
        
        public AuthentificatorServiceClient() {
        }
        
        public AuthentificatorServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AuthentificatorServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuthentificatorServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuthentificatorServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Guid SetUserAndGetCookie(WebUi.AuthentificatorService.UserIdentification userIdentification) {
            return base.Channel.SetUserAndGetCookie(userIdentification);
        }
        
        public WebUi.AuthentificatorService.UserIdentification GetUserByCookie(System.Guid cookie) {
            return base.Channel.GetUserByCookie(cookie);
        }
    }
}