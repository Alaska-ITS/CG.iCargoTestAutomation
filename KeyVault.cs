using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCargoUIAutomation
{
    public class KeyVault
    {
        public static string? keyVaultUri = ConfigurationManager.AppSettings["Key_Vault_Uri"];        
        public static string? CCC_Username = ConfigurationManager.AppSettings["CCC_Secret_Username"];
        public static string? CCC_Password = ConfigurationManager.AppSettings["CCC_Secret_Password"];
        public static string? CGODG_Username = ConfigurationManager.AppSettings["CGODG_Secret_Username"];
        public static string? CGODG_Password = ConfigurationManager.AppSettings["CGODG_Secret_Password"];
        public static Dictionary<string, string> GetSecret()
        {
            //var client = new SecretClient(new Uri(keyVaultUri), new ClientSecretCredential(tenantId, clientId, clientSecret));
            var client = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());

            var secrets = new Dictionary<string, string>();

            KeyVaultSecret cccUsername = client.GetSecret(CCC_Username);
            string ccc_UserName = cccUsername.Value;
            secrets.Add("CCC_Username", ccc_UserName);

            KeyVaultSecret cccPassword = client.GetSecret(CCC_Password);
            string ccc_Password = cccPassword.Value;
            secrets.Add("CCC_Password", ccc_Password);

            KeyVaultSecret cgodgUsername = client.GetSecret(CGODG_Username);
            string cgodg_UserName = cgodgUsername.Value;
            secrets.Add("CGODG_Username", cgodg_UserName);

            KeyVaultSecret cgodgPassword = client.GetSecret(CGODG_Password);
            string cgodg_Password = cgodgPassword.Value;
            secrets.Add("CGODG_Password", cgodg_Password);

            return secrets;
        }
    }
}
