using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using fnbr.authSessions;
using System.Diagnostics;
using System.Threading;
using System.Web;
using System.IO;
using System.IO.Compression;
using fnbr.Misc;

namespace fnbr
{
    public class Client
    {
        private static Exception HandleError(IRestResponse rest) //Todo: Switch to void and throw here
        {

            var error = JsonConvert.DeserializeObject<Json.error>(rest.Content);
            if (error.errorCode == null || error.errorMessage == null)
            {
                return new Exception(rest.ErrorMessage);
            }
            Exception exception = new Exception($"\nGot HTTP {(int)rest.StatusCode} {rest.StatusDescription}\n" + error.errorMessage + "\n\n" + error.errorCode);
            exception.Source = error.originatingService;
            return exception;
        }


        internal static Classes.client_credential GetCredentialcodetoken(string BasicToken)
        {
            var client = new RestClient("https://account-public-service-prod03.ol.epicgames.com/account/api/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Basic {BasicToken}");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.Timeout = 4000;
            request.AddParameter("grant_type", "client_credentials");
            var response = client.Execute(request);

            HandleError(response);

            return JsonConvert.DeserializeObject<Classes.client_credential>(response.Content);
        }

        internal static void setupRequest(IRestClient client)
        {
            client.UserAgent = "fnbr-Csharp/0.0.1";
            client.Timeout = 3000;
        }

        /// <summary>
        /// New Client to interact with Epic Api
        /// </summary>
        public Client()
        {

        }

        /// <summary>
        /// The auth session
        /// </summary>
        public Classes.Token AuthSession;

        /// <summary>
        /// Generate a device code to save in a file.
        /// </summary>
        /// <returns>Compressed Response</returns>
        public byte[] GetDeviceAuth()
        {
            if (this.AuthSession == null) throw new Exception("Login first");

            var client = new RestClient("https://account-public-service-prod.ol.epicgames.com/account/api/public/account/" + AuthSession.account_id + "/deviceAuth");
            var request = new RestRequest(Method.POST);

            request.AddHeader("Authorization", "Bearer " + AuthSession.access_token);
            setupRequest(client);

            var response = client.Execute(request);

            if (!response.IsSuccessful) throw HandleError(response);
            var DeviceAuth = JsonConvert.DeserializeObject<Classes.DeviceAuthJ>(response.Content);

            return misc.Compress(JsonConvert.SerializeObject(new
            {
                accountId = DeviceAuth.accountId,
                deviceId = DeviceAuth.deviceId,
                secret = DeviceAuth.secret,
                clientId = AuthSession.client_id
            }));
        }

        public void OpenWeb()
        {
            if (this.AuthSession == null) throw new Exception("Login first");

            Process.Start("https://www.epicgames.com/id/exchange?exchangeCode=" + GenerateExchange());
        }

        public string GenerateExchange()
        {
            if (this.AuthSession == null) throw new Exception("Login first");


            var client = new RestClient(Endpoints.Oauth.Exchange);
            setupRequest(client);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + AuthSession.access_token);

            var response = client.Execute(request);
            if (!response.IsSuccessful) throw HandleError(response);
            dynamic Json = JsonConvert.DeserializeObject(response.Content);

            return (string)Json.code;
        }

        /// <summary>
        /// Login using an Authorization Code
        /// </summary>
        /// <param name="authCode">The Authorization Code</param>
        /// <param name="authClient">The client used to generate the code</param>
        public void CodeLogin(string authCode, AuthClients authClient = AuthClients.Windows)
        {
            var basic = "Basic " + Tokens.ClientList.Find(x => x.AuthClient == authClient).BasicToken;

            var client = new RestClient(Endpoints.Oauth.CreateToken);
            setupRequest(client);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", basic);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("code", authCode);

            var response = client.Execute(request);

            if (!response.IsSuccessful)
                throw HandleError(response);

            AuthSession = JsonConvert.DeserializeObject<Classes.Token>(response.Content);
        }

        /// <summary>
        /// DeviceCode login but using event args. So its will not freeze the whole process
        /// </summary>
        /// <param name="OnCompleted">The Auth Session</param>
        public void DeviceCodeLogin(EventHandler<Client> OnCompleted, EventHandler<Exception> OnDecline, EventHandler<Exception> OnError = null)
        {
            var client = new RestClient(Endpoints.Oauth.DeviceCode);
            var request = new RestRequest(Method.POST);
            setupRequest(client);
            request.AddHeader("Authorization", $"Bearer {AuthSessions.client_Credential(AuthClients.Switch).access_token}");
            setupRequest(client);

            var response = client.Execute(request);
            HandleError(response);

            Classes.Devicecode Devicecode = JsonConvert.DeserializeObject<Classes.Devicecode>(response.Content);
            Process.Start(Devicecode.verification_uri_complete);

            Task.Run(() =>
            {
                while (true)
                {
                    var client1 = new RestClient(Endpoints.Oauth.CreateToken);
                    setupRequest(client1);
                    var request1 = new RestRequest(Method.POST);
                    request1.AddHeader("Authorization", $"Basic {Tokens.ClientList.Find(x => x.AuthClient == AuthClients.Switch).BasicToken}");
                    request1.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                    request1.AddParameter("grant_type", "device_code");
                    request1.AddParameter("device_code", Devicecode.device_code);
                    var response1 = client1.Execute(request1);

                    if (response1.IsSuccessful)
                    {
                        AuthSession = JsonConvert.DeserializeObject<Classes.Token>(response1.Content);
                        OnCompleted(null, this);
                        break;
                    }
                    else if (response1.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        var error = JsonConvert.DeserializeObject<Json.error>(response1.Content);
                        if (error.errorCode == "errors.com.epicgames.not_found")
                        {
                            OnDecline(null, new Exception("User Declined Login"));
                        }
                        else if (error.errorCode != "errors.com.epicgames.account.oauth.authorization_pending")
                        {
                            throw HandleError(response1);
                        }
                    }
                    else
                    {
                        throw HandleError(response1);
                    }

                    Thread.Sleep(5000); //5 sec
                    }
            });
        }


        /// <summary>
        /// Login using generated Device Auth.
        /// </summary>
        /// <param name="AuthCode">The File byte</param>
        public void DeviceAuthLogin(byte[] AuthCode)
        {
            var Decompressed = misc.Decompress(AuthCode);
            dynamic Json = JsonConvert.DeserializeObject(Decompressed); //todo: make a class

            var client = new RestClient(Endpoints.Oauth.CreateToken);
            var request = new RestRequest(Method.POST);
            Console.WriteLine(Json.clientId);
            var UsedClient = Tokens.ClientList.Find(x => x.clientId == (string)Json.clientId);

            request.AddHeader("Authorization", $"Basic {UsedClient.BasicToken}");
            setupRequest(client);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "device_auth");
            request.AddParameter("account_id", Json.accountId);
            request.AddParameter("device_id", Json.deviceId);
            request.AddParameter("secret", Json.secret);
            //request.AddParameter("token_type", "eg1");
            var response = client.Execute(request);
            AuthSession = JsonConvert.DeserializeObject<Classes.Token>(response.Content);
        }
        public void SidLogin(string sid)
        {
            var SetSid = new RestClient("https://www.epicgames.com/id/api/set-sid?sid=" + sid);
            var SetXSRF = new RestClient("https://www.epicgames.com/id/api/csrf");
            var Exchange = new RestClient("https://www.epicgames.com/id/api/exchange/generate");
            setupRequest(SetSid);
            setupRequest(SetXSRF);
            setupRequest(Exchange);

            var GetReq = new RestRequest();

            GetReq.AddHeader("X-Epic-Event-Action", "login");
            GetReq.AddHeader("X-Epic-Event-Category", "login");

            var SetSid_res = SetSid.Execute(GetReq);
            if (!SetSid_res.IsSuccessful)
                throw HandleError(SetSid_res);

            var Cookies = SetSid_res.Cookies.ToList();


            var SetXSRF_res = SetXSRF.Execute(GetReq);

            if (!SetXSRF_res.IsSuccessful)
                throw HandleError(SetXSRF_res);

            var Cookies2 = SetXSRF_res.Cookies.ToList();
            var XSRF_Token = Cookies2.Find(x => x.Name == "XSRF-TOKEN").Value;

            var PostReq = new RestRequest();
            PostReq.Method = Method.POST;

            PostReq.AddHeader("X-Epic-Event-Action", null);
            PostReq.AddHeader("X-Epic-Event-Category", null);
            PostReq.AddHeader("X-XSRF-TOKEN", XSRF_Token);

            Cookies.ForEach(cookie =>
            {
                PostReq.AddCookie(cookie.Name, cookie.Value);
            });

            Cookies2.ForEach(cookie =>
            {
                PostReq.AddCookie(cookie.Name, cookie.Value);
            });

            var Response = Exchange.Execute(PostReq);
            if (!Response.IsSuccessful)
                throw HandleError(Response);
            dynamic Code = JsonConvert.DeserializeObject(Response.Content);

            string ExchangeCode = Code.code;
            if (ExchangeCode == null)
            {
                throw new NullReferenceException("Exchange Code was null");
            }

            ExchangeLogin(ExchangeCode);
        }

        public void ExchangeLogin(string ExchangeCode, EventHandler<Exception> OnError = null, AuthClients authClient = AuthClients.Windows)
        {
            var client = new RestClient("https://account-public-service-prod03.ol.epicgames.com/account/api/oauth/token");
            setupRequest(client);
            var request = new RestRequest(Method.POST);

            request.AddHeader("Authorization", "Basic " + Tokens.ClientList.Find(x => x.AuthClient == authClient).BasicToken);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            request.AddParameter("grant_type", "exchange_code");
            request.AddParameter("exchange_code", ExchangeCode);

            var response = client.Execute(request);

            if (!response.IsSuccessful)
                throw HandleError(response);

            AuthSession = JsonConvert.DeserializeObject<Classes.Token>(response.Content);


        }

        public string GetAuthCode(AuthClients authClient = AuthClients.Windows)
        {
            if (this.AuthSession == null) throw new Exception("Login first");

            var client = new RestClient("https://www.epicgames.com/id/api/redirect?responseType=code&clientId=" + Tokens.ClientList.Find(x => x.AuthClient == authClient).clientId);
            setupRequest(client);
            var request = new RestRequest(Method.GET);
            request.AddCookie("EPIC_BEARER_TOKEN", AuthSession.access_token);
            var response = client.Execute(request);
            if (!response.IsSuccessful) throw HandleError(response);

            string redirectUri = ((dynamic)JsonConvert.DeserializeObject(response.Content)).redirectUrl;
            string queryString = new Uri(redirectUri).Query;
            var queryDictionary = HttpUtility.ParseQueryString(queryString);
            return queryDictionary["code"];
        }


        public void DeviceCodeLogin()
        {
            var client = new RestClient("https://account-public-service-prod.ol.epicgames.com/account/api/public/account/" + AuthSessions.Token.account_id + "/deviceAuth");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {AuthSessions.Token.access_token}");
            setupRequest(client);


            var response = client.Execute(request);

            if (!response.IsSuccessful)
                throw HandleError(response);

            Classes.Devicecode Devicecode = JsonConvert.DeserializeObject<Classes.Devicecode>(response.Content);

            while (true)
            {
                var client1 = new RestClient("https://account-public-service-prod03.ol.epicgames.com/account/api/oauth/token");

                var request1 = new RestRequest(Method.POST);
                request1.AddHeader("Authorization", $"Basic {Tokens.ClientList.Find(x => x.AuthClient == AuthClients.Switch).BasicToken}");
                request1.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                request1.AddParameter("grant_type", "device_code");
                request1.AddParameter("device_code", Devicecode.device_code);
                var response1 = client.Execute(request);

                if (response1.IsSuccessful)
                {
                    AuthSession = JsonConvert.DeserializeObject<Classes.Token>(response1.Content);
                    break;
                }
                else if (response1.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var error = JsonConvert.DeserializeObject<Json.error>(response1.Content);
                    if (error.errorCode == "errors.com.epicgames.not_found")
                    {
                        throw new Exception("User Declined Login");
                    }
                    else if (error.errorCode != "errors.com.epicgames.account.oauth.authorization_pending")
                    {
                        throw HandleError(response1);
                    }
                }
                else
                {
                    throw HandleError(response1);
                }

                Thread.Sleep(5000); //5 sec
            }

        }


    }


}

