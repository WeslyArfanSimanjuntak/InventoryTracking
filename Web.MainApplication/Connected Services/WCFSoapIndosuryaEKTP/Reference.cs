﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.MainApplication.WCFSoapIndosuryaEKTP {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Content", Namespace="http://schemas.datacontract.org/2004/07/EKTPWebApplication")]
    [System.SerializableAttribute()]
    public partial class Content : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ALAMATField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string JENIS_KLMINField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string JENIS_PKRJNField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string KAB_NAMEField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string KEC_NAMEField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string KEL_NAMEField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NAMA_LGKPField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NAMA_LGKP_IBUField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long NIKField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NO_KABField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NO_KECField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NO_KELField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long NO_KKField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NO_PROPField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NO_RTField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private object NO_RWField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PROP_NAMEField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string STATUS_KAWINField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TGL_LHRField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TMPT_LHRField;
        
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
        public string ALAMAT {
            get {
                return this.ALAMATField;
            }
            set {
                if ((object.ReferenceEquals(this.ALAMATField, value) != true)) {
                    this.ALAMATField = value;
                    this.RaisePropertyChanged("ALAMAT");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string JENIS_KLMIN {
            get {
                return this.JENIS_KLMINField;
            }
            set {
                if ((object.ReferenceEquals(this.JENIS_KLMINField, value) != true)) {
                    this.JENIS_KLMINField = value;
                    this.RaisePropertyChanged("JENIS_KLMIN");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string JENIS_PKRJN {
            get {
                return this.JENIS_PKRJNField;
            }
            set {
                if ((object.ReferenceEquals(this.JENIS_PKRJNField, value) != true)) {
                    this.JENIS_PKRJNField = value;
                    this.RaisePropertyChanged("JENIS_PKRJN");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string KAB_NAME {
            get {
                return this.KAB_NAMEField;
            }
            set {
                if ((object.ReferenceEquals(this.KAB_NAMEField, value) != true)) {
                    this.KAB_NAMEField = value;
                    this.RaisePropertyChanged("KAB_NAME");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string KEC_NAME {
            get {
                return this.KEC_NAMEField;
            }
            set {
                if ((object.ReferenceEquals(this.KEC_NAMEField, value) != true)) {
                    this.KEC_NAMEField = value;
                    this.RaisePropertyChanged("KEC_NAME");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string KEL_NAME {
            get {
                return this.KEL_NAMEField;
            }
            set {
                if ((object.ReferenceEquals(this.KEL_NAMEField, value) != true)) {
                    this.KEL_NAMEField = value;
                    this.RaisePropertyChanged("KEL_NAME");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NAMA_LGKP {
            get {
                return this.NAMA_LGKPField;
            }
            set {
                if ((object.ReferenceEquals(this.NAMA_LGKPField, value) != true)) {
                    this.NAMA_LGKPField = value;
                    this.RaisePropertyChanged("NAMA_LGKP");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NAMA_LGKP_IBU {
            get {
                return this.NAMA_LGKP_IBUField;
            }
            set {
                if ((object.ReferenceEquals(this.NAMA_LGKP_IBUField, value) != true)) {
                    this.NAMA_LGKP_IBUField = value;
                    this.RaisePropertyChanged("NAMA_LGKP_IBU");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long NIK {
            get {
                return this.NIKField;
            }
            set {
                if ((this.NIKField.Equals(value) != true)) {
                    this.NIKField = value;
                    this.RaisePropertyChanged("NIK");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NO_KAB {
            get {
                return this.NO_KABField;
            }
            set {
                if ((this.NO_KABField.Equals(value) != true)) {
                    this.NO_KABField = value;
                    this.RaisePropertyChanged("NO_KAB");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NO_KEC {
            get {
                return this.NO_KECField;
            }
            set {
                if ((this.NO_KECField.Equals(value) != true)) {
                    this.NO_KECField = value;
                    this.RaisePropertyChanged("NO_KEC");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NO_KEL {
            get {
                return this.NO_KELField;
            }
            set {
                if ((this.NO_KELField.Equals(value) != true)) {
                    this.NO_KELField = value;
                    this.RaisePropertyChanged("NO_KEL");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long NO_KK {
            get {
                return this.NO_KKField;
            }
            set {
                if ((this.NO_KKField.Equals(value) != true)) {
                    this.NO_KKField = value;
                    this.RaisePropertyChanged("NO_KK");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NO_PROP {
            get {
                return this.NO_PROPField;
            }
            set {
                if ((this.NO_PROPField.Equals(value) != true)) {
                    this.NO_PROPField = value;
                    this.RaisePropertyChanged("NO_PROP");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NO_RT {
            get {
                return this.NO_RTField;
            }
            set {
                if ((this.NO_RTField.Equals(value) != true)) {
                    this.NO_RTField = value;
                    this.RaisePropertyChanged("NO_RT");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public object NO_RW {
            get {
                return this.NO_RWField;
            }
            set {
                if ((object.ReferenceEquals(this.NO_RWField, value) != true)) {
                    this.NO_RWField = value;
                    this.RaisePropertyChanged("NO_RW");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PROP_NAME {
            get {
                return this.PROP_NAMEField;
            }
            set {
                if ((object.ReferenceEquals(this.PROP_NAMEField, value) != true)) {
                    this.PROP_NAMEField = value;
                    this.RaisePropertyChanged("PROP_NAME");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string STATUS_KAWIN {
            get {
                return this.STATUS_KAWINField;
            }
            set {
                if ((object.ReferenceEquals(this.STATUS_KAWINField, value) != true)) {
                    this.STATUS_KAWINField = value;
                    this.RaisePropertyChanged("STATUS_KAWIN");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TGL_LHR {
            get {
                return this.TGL_LHRField;
            }
            set {
                if ((object.ReferenceEquals(this.TGL_LHRField, value) != true)) {
                    this.TGL_LHRField = value;
                    this.RaisePropertyChanged("TGL_LHR");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TMPT_LHR {
            get {
                return this.TMPT_LHRField;
            }
            set {
                if ((object.ReferenceEquals(this.TMPT_LHRField, value) != true)) {
                    this.TMPT_LHRField = value;
                    this.RaisePropertyChanged("TMPT_LHR");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WCFSoapIndosuryaEKTP.IEKTPService")]
    public interface IEKTPService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEKTPService/DoWork", ReplyAction="http://tempuri.org/IEKTPService/DoWorkResponse")]
        void DoWork();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEKTPService/DoWork", ReplyAction="http://tempuri.org/IEKTPService/DoWorkResponse")]
        System.Threading.Tasks.Task DoWorkAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEKTPService/CekNIK", ReplyAction="http://tempuri.org/IEKTPService/CekNIKResponse")]
        Web.MainApplication.WCFSoapIndosuryaEKTP.Content CekNIK(string nik);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEKTPService/CekNIK", ReplyAction="http://tempuri.org/IEKTPService/CekNIKResponse")]
        System.Threading.Tasks.Task<Web.MainApplication.WCFSoapIndosuryaEKTP.Content> CekNIKAsync(string nik);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEKTPService/StringCekNIK", ReplyAction="http://tempuri.org/IEKTPService/StringCekNIKResponse")]
        string StringCekNIK(string nik);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEKTPService/StringCekNIK", ReplyAction="http://tempuri.org/IEKTPService/StringCekNIKResponse")]
        System.Threading.Tasks.Task<string> StringCekNIKAsync(string nik);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IEKTPServiceChannel : Web.MainApplication.WCFSoapIndosuryaEKTP.IEKTPService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class EKTPServiceClient : System.ServiceModel.ClientBase<Web.MainApplication.WCFSoapIndosuryaEKTP.IEKTPService>, Web.MainApplication.WCFSoapIndosuryaEKTP.IEKTPService {
        
        public EKTPServiceClient() {
        }
        
        public EKTPServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public EKTPServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EKTPServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EKTPServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void DoWork() {
            base.Channel.DoWork();
        }
        
        public System.Threading.Tasks.Task DoWorkAsync() {
            return base.Channel.DoWorkAsync();
        }
        
        public Web.MainApplication.WCFSoapIndosuryaEKTP.Content CekNIK(string nik) {
            return base.Channel.CekNIK(nik);
        }
        
        public System.Threading.Tasks.Task<Web.MainApplication.WCFSoapIndosuryaEKTP.Content> CekNIKAsync(string nik) {
            return base.Channel.CekNIKAsync(nik);
        }
        
        public string StringCekNIK(string nik) {
            return base.Channel.StringCekNIK(nik);
        }
        
        public System.Threading.Tasks.Task<string> StringCekNIKAsync(string nik) {
            return base.Channel.StringCekNIKAsync(nik);
        }
    }
}
