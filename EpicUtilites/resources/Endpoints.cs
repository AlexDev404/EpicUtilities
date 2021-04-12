using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fnbr
{
    internal class Endpoints
    {
        internal class Oauth
        {
            internal const string CreateToken = "https://account-public-service-prod.ol.epicgames.com/account/api/oauth/token";
            internal const string TokenVerify = "https://account-public-service-prod.ol.epicgames.com/account/api/oauth/verify";
            internal const string Exchange = "https://account-public-service-prod.ol.epicgames.com/account/api/oauth/exchange";
            internal const string DeviceCode = "https://account-public-service-prod.ol.epicgames.com/account/api/oauth/deviceAuthorization";
            internal const string Sid = "https://www.epicgames.com/id/api/set-sid";
            internal const string CSRF = "https://www.epicgames.com/id/api/csrf";
            internal const string WebExchange = "https://www.epicgames.com/id/api/exchange/generate";
        }

        internal class Xmpp
        {
            internal const string Server = "wss://xmpp-service-prod.ol.epicgames.com";
        }

        internal class Account
        {
            internal const string Multiple = "https://account-public-service-prod.ol.epicgames.com/account/api/public/account";
            internal const string Mcp = "https://fortnite-public-service-prod11.ol.epicgames.com/fortnite/api/game/v2/profile";
            internal const string DisplayName = "https://account-public-service-prod.ol.epicgames.com/account/api/public/account/displayName";
            internal const string AccountId = "https://account-public-service-prod.ol.epicgames.com/account/api/public/account";
            internal const string Email = "https://account-public-service-prod03.ol.epicgames.com/account/api/public/account/email";
        }

        internal const string Party = "https://party-service-prod.ol.epicgames.com/party/api/v1/Fortnite";
    }
}
