﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.1
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MTestConsole.MServiceD {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="MCodeSilverlightServer", ConfigurationName="MServiceD.MDuplexService", CallbackContract=typeof(MTestConsole.MServiceD.MDuplexServiceCallback))]
    public interface MDuplexService {
        
        [System.ServiceModel.OperationContractAttribute(Action="MCodeSilverlightServer/MDuplexService/srv_select_DataFromLists", ReplyAction="MCodeSilverlightServer/MDuplexService/srv_select_DataFromListsResponse")]
        void srv_select_DataFromLists(string tbl);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="MCodeSilverlightServer/MDuplexService/srv_work_DataFromLists")]
        void srv_work_DataFromLists(string tbl, string action, MClassLib.cListItem item);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface MDuplexServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="MCodeSilverlightServer/MDuplexService/packageConsignmentList")]
        void packageConsignmentList(MClassLib.cConsignmentItem[] list);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="MCodeSilverlightServer/MDuplexService/packageItemsList")]
        void packageItemsList(MClassLib.cListItem[] list);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="MCodeSilverlightServer/MDuplexService/packageError")]
        void packageError(System.Exception err);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface MDuplexServiceChannel : MTestConsole.MServiceD.MDuplexService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MDuplexServiceClient : System.ServiceModel.DuplexClientBase<MTestConsole.MServiceD.MDuplexService>, MTestConsole.MServiceD.MDuplexService {
        
        public MDuplexServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public MDuplexServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public MDuplexServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public MDuplexServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public MDuplexServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void srv_select_DataFromLists(string tbl) {
            base.Channel.srv_select_DataFromLists(tbl);
        }
        
        public void srv_work_DataFromLists(string tbl, string action, MClassLib.cListItem item) {
            base.Channel.srv_work_DataFromLists(tbl, action, item);
        }
    }
}
