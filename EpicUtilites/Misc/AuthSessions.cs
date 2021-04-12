using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fnbr.Classes;
using fnbr;

namespace fnbr.authSessions
{
    public enum AuthClients
    {
        iOS,
        Windows,
        Switch
    }
    class AuthSessions
    {
        public static Token Token;

        internal static List<client_credential> TokenList = new List<client_credential>();
        public static client_credential client_Credential(AuthClients authClient)
        {
            var clientToken = Tokens.ClientList.Find(x => x.AuthClient == authClient);
            var finded = TokenList.Find(x => x.client_id == clientToken.clientId);

            if (finded != null && finded.expires_at.Ticks >= DateTime.Now.Ticks)
            {
                return finded;
            }
            else if (finded != null && finded.expires_at.Ticks <= DateTime.Now.Ticks)
            {
                TokenList.Remove(finded); //token is expired
            }

            var cred = Client.GetCredentialcodetoken(Tokens.ClientList.Find(x => x.AuthClient == authClient).BasicToken);
            TokenList.Add(cred);
            return cred;
        }



    }
}
