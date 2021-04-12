using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fnbr.Classes
{
    public class DeviceAuthJ
    {
        public string deviceId { get; set; }
        public string accountId { get; set; }
        public string secret { get; set; }
        public string userAgent { get; set; }
        private object created { get { return null; } }
    }

    public class client_credential
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public DateTime expires_at { get; set; }
        public string token_type { get; set; }
        public string client_id { get; set; }
        public bool internal_client { get; set; }
        public string client_service { get; set; }
    }

    internal class Devicecode
    {
        public string user_code { get; set; }
        public string device_code { get; set; }
        public string verification_uri { get; set; }
        public string verification_uri_complete { get; set; }
        public string prompt { get; set; }
        public int expires_in { get; set; }
        public int interval { get; set; }
        public string client_id { get; set; }

    }

    public class Token
    {
        /// <summary>
        /// The token
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// Expire in secondes
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// Exipre in dateTime
        /// </summary>
        public DateTime expires_at { get; set; }

        /// <summary>
        /// Token_Type (always bearer)
        /// </summary>
        public string token_type
        {
            get
            {
                return "bearer";
            }
        }

        /// <summary>
        /// The refresh token (Can be used to login back again)
        /// </summary>
        public string refresh_token { get; set; }

        /// <summary>
        /// The time when the refresh token expire in secondes
        /// </summary>
        public int refresh_expires { get; set; }

        /// <summary>
        /// The time when the refresh token expire in DateTime
        /// </summary>
        public DateTime refresh_expires_at { get; set; }

        /// <summary>
        /// The accountId of the logged account
        /// </summary>
        public string account_id { get; set; }

        /// <summary>
        /// The ClientId used to login
        /// </summary>
        public string client_id { get; set; }
        public bool internal_client { get; set; }
        public string client_service { get; set; }

        /// <summary>
        /// The name everyone see. (The username)
        /// </summary>
        public string displayName { get; set; }

        /// <summary>
        /// (e.g Fortnite) 
        /// </summary>
        public string app { get; set; }
        public string in_app_id { get; set; }
    }


}
