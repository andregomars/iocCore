using System;

namespace iocCoreSMS.Models
{
    public interface ISMSConfiguration
    {
        string UrlSendSMS { get; set; }
        string UrlReceiveSMS { get; set; }
        string UrlGetAccessToken { get; set; }
        string AppScope { get; set; }
        string AppKey { get; set; }
        string AppSecret { get; set; }
        bool VerifyMessageDeliveryStatus { get; set; }
        string RefreshToken { get; set; }
        DateTime TokenExpiresDate { get; set; }
        string BaseUrlMessageApi { get; set; } 
    }

    public sealed class SMSConfiguration : ISMSConfiguration
    {
        //lazy loaded singleton configuration object
        // private static readonly Lazy<SMSConfiguration> Lazy =
        //     new Lazy<SMSConfiguration>( () => new SMSConfiguration() );
        // public static SMSConfiguration Instance { get { return Lazy.Value; } }
        // private SMSConfiguration() {}

        public string UrlSendSMS { get; set; }
        public string UrlReceiveSMS { get; set; }
        public string UrlGetAccessToken { get; set; }
        public string AppScope { get; set; }
        public string AppKey { get; set; }
        public string AppSecret { get; set; }
        public bool VerifyMessageDeliveryStatus { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenExpiresDate { get; set; }
        public string BaseUrlMessageApi { get; set; }  
            
    }
}