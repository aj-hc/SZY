﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.17929
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.17929 版自动生成。
// 
#pragma warning disable 1591

namespace RuRo.BLL.ClinicalData {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="PacsLisReportServicesSoap", Namespace="http://tempuri.org/")]
    public partial class PacsLisReportServices : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback NetTestPacsOperationCompleted;
        
        private System.Threading.SendOrPostCallback NetTestLisOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetPacsItemsOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetNormalLisItemsOperationCompleted;
        
        private System.Threading.SendOrPostCallback PacsReportOperationCompleted;
        
        private System.Threading.SendOrPostCallback NormalLisReportOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetPatientDiagnoseOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public PacsLisReportServices() {
            this.Url = global::RuRo.BLL.Properties.Settings.Default.BLL_ClinicalData_PacsLisReportServices;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event NetTestPacsCompletedEventHandler NetTestPacsCompleted;
        
        /// <remarks/>
        public event NetTestLisCompletedEventHandler NetTestLisCompleted;
        
        /// <remarks/>
        public event GetPacsItemsCompletedEventHandler GetPacsItemsCompleted;
        
        /// <remarks/>
        public event GetNormalLisItemsCompletedEventHandler GetNormalLisItemsCompleted;
        
        /// <remarks/>
        public event PacsReportCompletedEventHandler PacsReportCompleted;
        
        /// <remarks/>
        public event NormalLisReportCompletedEventHandler NormalLisReportCompleted;
        
        /// <remarks/>
        public event GetPatientDiagnoseCompletedEventHandler GetPatientDiagnoseCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/NetTestPacs", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string NetTestPacs() {
            object[] results = this.Invoke("NetTestPacs", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void NetTestPacsAsync() {
            this.NetTestPacsAsync(null);
        }
        
        /// <remarks/>
        public void NetTestPacsAsync(object userState) {
            if ((this.NetTestPacsOperationCompleted == null)) {
                this.NetTestPacsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnNetTestPacsOperationCompleted);
            }
            this.InvokeAsync("NetTestPacs", new object[0], this.NetTestPacsOperationCompleted, userState);
        }
        
        private void OnNetTestPacsOperationCompleted(object arg) {
            if ((this.NetTestPacsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.NetTestPacsCompleted(this, new NetTestPacsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/NetTestLis", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string NetTestLis() {
            object[] results = this.Invoke("NetTestLis", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void NetTestLisAsync() {
            this.NetTestLisAsync(null);
        }
        
        /// <remarks/>
        public void NetTestLisAsync(object userState) {
            if ((this.NetTestLisOperationCompleted == null)) {
                this.NetTestLisOperationCompleted = new System.Threading.SendOrPostCallback(this.OnNetTestLisOperationCompleted);
            }
            this.InvokeAsync("NetTestLis", new object[0], this.NetTestLisOperationCompleted, userState);
        }
        
        private void OnNetTestLisOperationCompleted(object arg) {
            if ((this.NetTestLisCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.NetTestLisCompleted(this, new NetTestLisCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetPacsItems", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetPacsItems(string Request) {
            object[] results = this.Invoke("GetPacsItems", new object[] {
                        Request});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetPacsItemsAsync(string Request) {
            this.GetPacsItemsAsync(Request, null);
        }
        
        /// <remarks/>
        public void GetPacsItemsAsync(string Request, object userState) {
            if ((this.GetPacsItemsOperationCompleted == null)) {
                this.GetPacsItemsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetPacsItemsOperationCompleted);
            }
            this.InvokeAsync("GetPacsItems", new object[] {
                        Request}, this.GetPacsItemsOperationCompleted, userState);
        }
        
        private void OnGetPacsItemsOperationCompleted(object arg) {
            if ((this.GetPacsItemsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetPacsItemsCompleted(this, new GetPacsItemsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetNormalLisItems", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetNormalLisItems(string Request) {
            object[] results = this.Invoke("GetNormalLisItems", new object[] {
                        Request});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetNormalLisItemsAsync(string Request) {
            this.GetNormalLisItemsAsync(Request, null);
        }
        
        /// <remarks/>
        public void GetNormalLisItemsAsync(string Request, object userState) {
            if ((this.GetNormalLisItemsOperationCompleted == null)) {
                this.GetNormalLisItemsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetNormalLisItemsOperationCompleted);
            }
            this.InvokeAsync("GetNormalLisItems", new object[] {
                        Request}, this.GetNormalLisItemsOperationCompleted, userState);
        }
        
        private void OnGetNormalLisItemsOperationCompleted(object arg) {
            if ((this.GetNormalLisItemsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetNormalLisItemsCompleted(this, new GetNormalLisItemsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/PacsReport", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string PacsReport(string Request) {
            object[] results = this.Invoke("PacsReport", new object[] {
                        Request});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void PacsReportAsync(string Request) {
            this.PacsReportAsync(Request, null);
        }
        
        /// <remarks/>
        public void PacsReportAsync(string Request, object userState) {
            if ((this.PacsReportOperationCompleted == null)) {
                this.PacsReportOperationCompleted = new System.Threading.SendOrPostCallback(this.OnPacsReportOperationCompleted);
            }
            this.InvokeAsync("PacsReport", new object[] {
                        Request}, this.PacsReportOperationCompleted, userState);
        }
        
        private void OnPacsReportOperationCompleted(object arg) {
            if ((this.PacsReportCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.PacsReportCompleted(this, new PacsReportCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/NormalLisReport", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string NormalLisReport(string Request) {
            object[] results = this.Invoke("NormalLisReport", new object[] {
                        Request});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void NormalLisReportAsync(string Request) {
            this.NormalLisReportAsync(Request, null);
        }
        
        /// <remarks/>
        public void NormalLisReportAsync(string Request, object userState) {
            if ((this.NormalLisReportOperationCompleted == null)) {
                this.NormalLisReportOperationCompleted = new System.Threading.SendOrPostCallback(this.OnNormalLisReportOperationCompleted);
            }
            this.InvokeAsync("NormalLisReport", new object[] {
                        Request}, this.NormalLisReportOperationCompleted, userState);
        }
        
        private void OnNormalLisReportOperationCompleted(object arg) {
            if ((this.NormalLisReportCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.NormalLisReportCompleted(this, new NormalLisReportCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetPatientDiagnose", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetPatientDiagnose(string Request) {
            object[] results = this.Invoke("GetPatientDiagnose", new object[] {
                        Request});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetPatientDiagnoseAsync(string Request) {
            this.GetPatientDiagnoseAsync(Request, null);
        }
        
        /// <remarks/>
        public void GetPatientDiagnoseAsync(string Request, object userState) {
            if ((this.GetPatientDiagnoseOperationCompleted == null)) {
                this.GetPatientDiagnoseOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetPatientDiagnoseOperationCompleted);
            }
            this.InvokeAsync("GetPatientDiagnose", new object[] {
                        Request}, this.GetPatientDiagnoseOperationCompleted, userState);
        }
        
        private void OnGetPatientDiagnoseOperationCompleted(object arg) {
            if ((this.GetPatientDiagnoseCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetPatientDiagnoseCompleted(this, new GetPatientDiagnoseCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void NetTestPacsCompletedEventHandler(object sender, NetTestPacsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class NetTestPacsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal NetTestPacsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void NetTestLisCompletedEventHandler(object sender, NetTestLisCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class NetTestLisCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal NetTestLisCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetPacsItemsCompletedEventHandler(object sender, GetPacsItemsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetPacsItemsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetPacsItemsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetNormalLisItemsCompletedEventHandler(object sender, GetNormalLisItemsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetNormalLisItemsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetNormalLisItemsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void PacsReportCompletedEventHandler(object sender, PacsReportCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class PacsReportCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal PacsReportCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void NormalLisReportCompletedEventHandler(object sender, NormalLisReportCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class NormalLisReportCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal NormalLisReportCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetPatientDiagnoseCompletedEventHandler(object sender, GetPatientDiagnoseCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetPatientDiagnoseCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetPatientDiagnoseCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591