using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Sample
{
    public static class Portal
    {
        public static string Domain { get { return GetDomain(); } }
        public static string ClientId { get { return ConfigurationManager.AppSettings["ClientId"].ToString(); } }
        public static string ClientSecret { get { return ConfigurationManager.AppSettings["ClientSecret"].ToString(); } }
        public static string GrantType { get { return ConfigurationManager.AppSettings["GrantType"].ToString(); } }
        public static string Scope { get { return ConfigurationManager.AppSettings["Scope"].ToString(); } }
        public static string OAuth2URL { get { return "/services/api/oauth2/token"; } }
        public static string ServiceURL { get { return ConfigurationManager.AppSettings["ServiceURL"].ToString(); } }
        
        private static string GetDomain()
        {
            var uri = new Uri(Portal.ServiceURL);
            return uri.Host;
        }
    }


}
