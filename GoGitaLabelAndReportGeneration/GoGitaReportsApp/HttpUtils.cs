using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace GoGitaReportsApp
{
    public static class HttpUtils
    {
        #region Http Get Utils
        public static async Task<T> GetDataAsync<T>(HttpClient client, string endpointAddress)
        {
            HttpResponseMessage response = await client.GetAsync(endpointAddress);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<T>();
        }

        public static async Task<T> GetDataAsync<T>(HttpClient client, string endpointAddress, params Tuple<string, string>[] httpGetParameters)
            => await GetDataAsync<T>(client, AppendParametersToHttpUrl(endpointAddress, httpGetParameters));
        #endregion

        #region Http Post Utils
        public static async Task<T_Out> PostDataAsync<T_In, T_Out>(HttpClient client, string endpointAddress, T_In data)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(endpointAddress, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<T_Out>();
        }

        public static async Task PostDataAsync<T_In>(HttpClient client, string endpointAddress, T_In data)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(endpointAddress, data);
            response.EnsureSuccessStatusCode();
            return;
        }

        public static async Task PostDataAsync(HttpClient client, string endpointAddress, params Tuple<string, string>[] httpParameters)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync<string>(AppendParametersToHttpUrl(endpointAddress, httpParameters), String.Empty);
            response.EnsureSuccessStatusCode();
            return;
        }
        #endregion

        #region private helpers
        public class HttpRequestParams
        {
            public string url;
            public string method;
            public string parameters;
            public string request;
            public string mediaType;
            public string authScheme;
            public string authToken;
            public int timeOut;
        }

        public static string AppendParametersToHttpUrl(string httpUrl, Tuple<string, string>[] httpParameters)
        {
            string trimmedHttpUrl = httpUrl.Trim();
            string httpParametersForUrl = String.Join("&", httpParameters.Select(x => x.Item1 + "=" + x.Item2));
            return String.IsNullOrEmpty(httpParametersForUrl)
                ? trimmedHttpUrl
                : trimmedHttpUrl.Contains("?") == false
                    ? trimmedHttpUrl + "?" + httpParametersForUrl
                    : trimmedHttpUrl.EndsWith("?")
                        ? trimmedHttpUrl + httpParametersForUrl
                        : trimmedHttpUrl + "&" + httpParametersForUrl;
        }

        private static string SanitiseUrlEnd(string url)
        {
            if (url[url.Length - 1] == '/') url = url.Substring(0, url.Length - 1);
            if (url[url.Length - 1] != '?') url = url + "?";

            return url;
        }

        public static HttpResponseMessage GetHttpResponseMessage(HttpRequestParams requestParam, Dictionary<string, string> headerKeyValues)
        {
            using (HttpClient client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true }))
            {
                requestParam.url = SanitiseUrlEnd(requestParam.url);
                requestParam.url = requestParam.url + requestParam.parameters;

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(requestParam.mediaType));
                client.Timeout = TimeSpan.FromSeconds(requestParam.timeOut);
                if (requestParam.authToken != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(requestParam.authScheme, requestParam.authToken);

                using (HttpRequestMessage reqMsg = new HttpRequestMessage())
                {
                    reqMsg.RequestUri = new Uri(requestParam.url);

                    foreach (var kvp in headerKeyValues)
                        reqMsg.Headers.Add(kvp.Key, kvp.Value);

                    if ("GET" == requestParam.method.ToUpper())
                    {
                        reqMsg.Method = HttpMethod.Get;
                    }
                    else if ("POST" == requestParam.method.ToUpper())
                    {
                        reqMsg.Method = HttpMethod.Post;
                        reqMsg.Content = new StringContent(requestParam.request, Encoding.UTF8, "application/json");
                    }
                    else if ("PUT" == requestParam.method.ToUpper())
                    {
                        reqMsg.Method = HttpMethod.Put;
                        reqMsg.Content = new StringContent(requestParam.request, Encoding.UTF8, "application/json");
                    }
                    else if ("DELETE" == requestParam.method.ToUpper())
                    {
                        reqMsg.Method = HttpMethod.Delete;
                        reqMsg.Content = new StringContent(requestParam.request, Encoding.UTF8, "application/json");
                    }
                    return client.SendAsync(reqMsg).Result;
                }
            }
        }
        #endregion
    }
}