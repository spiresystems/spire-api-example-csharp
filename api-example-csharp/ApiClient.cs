using System;
using System.Collections.Generic;
using System.Net;
using RestSharp;


namespace ApiTest
{
    public class ApiException : Exception
    {
        public ApiException() : base() { }
        public ApiException(string message) : base(message) { }
        public ApiException(string message, Exception e) : base(message, e) { }
    }


    public class ApiClient
    {
        const string BaseUrl = "https://127.0.0.1:10880/api/v1";
        readonly public string Company;
        RestClient _client;
        readonly string _username;
        readonly string _password;

        public ApiClient(string company, string username, string password)
        {
            Company = company;
            _username = username;
            _password = password;
            _client = Client();
        }

        RestClient Client()
        {
            // Accept self-signed certificate
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => true;

            var client = new RestClient();
            client.BaseUrl = new Uri(BaseUrl);
            client.Authenticator = new HttpBasicAuthenticator(_username, _password);
            client.FollowRedirects = false;
            return client;
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            var response = _client.Execute<T>(request);

            HttpStatusCode code = response.StatusCode;
            if (code == HttpStatusCode.Unauthorized)
                throw new ApiException("Username or password incorrect");

            if (response.ErrorException != null)
                throw new ApplicationException(response.ErrorMessage,
                    response.ErrorException);

            if (code == HttpStatusCode.OK || code == HttpStatusCode.NoContent)
                return response.Data;

            if (code == HttpStatusCode.Created)
            {
                // Manually follow header redirect
                foreach (var p in response.Headers)
                {
                    if (p.Name == "Location")
                    {
                        var second_request = new RestRequest();
                        var location = p.Value.ToString();
                        second_request.Resource = location.Remove(0,
                            _client.BaseUrl.ToString().Length);
                        return Execute<T>(second_request);
                    }
                }
            }

            throw new ApiException(response.Content);
        }
    }


    public abstract class BaseObjectClient<T> where T : new()
    {
        protected ApiClient Client;

        public BaseObjectClient(ApiClient client)
        {
            Client = client;
        }

        string BaseResource
        {
            get
            {
                return string.Format("companies/{0}/", Client.Company);
            }
        }

        abstract public string Resource { get; }

        public List<T> List()
        {
            var request = new RestRequest();
            request.RequestFormat = DataFormat.Json;
            request.Resource = BaseResource + Resource;
            request.RootElement = "records";
            return Client.Execute<List<T>>(request);
        }

        public T Fetch(int id)
        {
            var request = new RestRequest();
            request.RequestFormat = DataFormat.Json;
            request.Resource = BaseResource + Resource + id;
            return Client.Execute<T>(request);
        }

        public T Create(T obj)
        {
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.Resource = BaseResource + Resource;
            request.AddBody(obj);
            return Client.Execute<T>(request);
        }

        public T Update(int id, T obj)
        {
            var request = new RestRequest(Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.Resource = BaseResource + Resource + id;
            request.AddBody(obj);
            return Client.Execute<T>(request);
        }

        public void Delete(int id)
        {
            var request = new RestRequest(Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.Resource = BaseResource + Resource + id;
            Client.Execute<ErrorResponse>(request);
        }
    }


    public class ErrorResponse
    {
        public string message { get; set; }
        public string error_type { get; set; }
        public string traceback { get; set; }
    }
}