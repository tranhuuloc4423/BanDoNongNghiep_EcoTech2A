using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Source_Demo.Lib
{
    public interface ICallApi
    {
        Task<ResponseData<T>> GetResponseDataAsync<T>(string url, Dictionary<string, dynamic> dictPars, string accessToken = "");
        Task<ResponseData<T>> GetDictHeaderResponseDataAsync<T>(string url, Dictionary<string, dynamic> dictPars, Dictionary<string, dynamic> dictHeads = default(Dictionary<string, dynamic>));
        Task<ResponseData<T>> PostResponseDataAsync<T>(string url, Dictionary<string, dynamic> dictPars, string accessToken = "");
        Task<ResponseData<T>> PostDictHeaderResponseDataAsync<T>(string url, Dictionary<string, dynamic> dictPars, Dictionary<string, dynamic> dictHeads = default(Dictionary<string, dynamic>));
        Task<ResponseData<T>> PostResponseDataAsync<T>(string url, MultipartFormDataContent formData, string accessToken = "");
        Task<ResponseData<T>> PostResponseDataAsync<T>(string url, FormUrlEncodedContent xwwwFormUrlEndcoded, string accessToken = "");
    }
    public class CallApi : ICallApi
    {
        private readonly HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(1) };
        public async Task<ResponseData<T>> GetResponseDataAsync<T>(string url, Dictionary<string, dynamic> dictPars, string accessToken = "")
        {
            ResponseData<T> res = new ResponseData<T>();
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (!string.IsNullOrEmpty(accessToken))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                int i = 0;
                string _params = "";
                UriBuilder _uribuilder = new UriBuilder(url);
                if (dictPars != null)
                    foreach (KeyValuePair<string, dynamic> item in dictPars)
                    {
                        _params += (i == 0 ? "" : "&") + string.Format("{0}={1}", item.Key, item.Value == null ? "" : item.Value.ToString());
                        i++;
                    }
                _uribuilder.Query = _params;
                var response = client.GetAsync(_uribuilder.Uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonres = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<ResponseData<T>>(jsonres);
                }
                else
                {
                    throw new ApiException
                    {
                        StatusCode = (int)response.StatusCode,
                        Content = await response.Content.ReadAsStringAsync()
                    };
                }
                return res;
            }
            catch (ApiException ex)
            {
                res.result = -1;
                res.error = new error() { code = ex.StatusCode, message = ex.Content };
                return res;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error = new error() { code = -1, message = ex.Message };
                return res;
            }
        }
        public async Task<ResponseData<T>> GetDictHeaderResponseDataAsync<T>(string url, Dictionary<string, dynamic> dictPars, Dictionary<string, dynamic> dictHeads = default(Dictionary<string, dynamic>))
        {
            ResponseData<T> res = new ResponseData<T>();
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (dictHeads != null)
                    foreach (KeyValuePair<string, dynamic> item in dictHeads)
                        client.DefaultRequestHeaders.Add(item.Key, item.Value == null ? "" : item.Value.ToString());
                int i = 0;
                string _params = "";
                UriBuilder _uribuilder = new UriBuilder(url);
                if (dictPars != null)
                    foreach (KeyValuePair<string, dynamic> item in dictPars)
                    {
                        _params += (i == 0 ? "" : "&") + string.Format("{0}={1}", item.Key, item.Value == null ? "" : item.Value.ToString());
                        i++;
                    }
                _uribuilder.Query = _params;
                var response = client.GetAsync(_uribuilder.Uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonres = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<ResponseData<T>>(jsonres);
                }
                else
                {
                    throw new ApiException
                    {
                        StatusCode = (int)response.StatusCode,
                        Content = await response.Content.ReadAsStringAsync()
                    };
                }
                return res;
            }
            catch (ApiException ex)
            {
                res.result = -1;
                res.error = new error() { code = ex.StatusCode, message = ex.Content };
                return res;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error = new error() { code = -1, message = ex.Message };
                return res;
            }
        }
        public async Task<ResponseData<T>> PostResponseDataAsync<T>(string url, Dictionary<string, dynamic> dictPars, string accessToken = "")
        {
            ResponseData<T> res = new ResponseData<T>();
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (!string.IsNullOrEmpty(accessToken))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                MultipartFormDataContent formData = new MultipartFormDataContent();
                if (dictPars != null)
                    foreach (KeyValuePair<string, dynamic> item in dictPars)
                        formData.Add(new StringContent(item.Value == null ? "" : item.Value.ToString()), item.Key);

                HttpResponseMessage response = client.PostAsync(url, formData).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonres = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<ResponseData<T>>(jsonres);
                }
                else
                {
                    throw new ApiException
                    {
                        StatusCode = (int)response.StatusCode,
                        Content = await response.Content.ReadAsStringAsync()
                    };
                }
                return res;
            }
            catch (ApiException ex)
            {
                res.result = -1;
                res.error = new error() { code = ex.StatusCode, message = ex.Content };
                return res;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error = new error() { code = -1, message = ex.Message };
                return res;
            }

        }
        public async Task<ResponseData<T>> PostDictHeaderResponseDataAsync<T>(string url, Dictionary<string, dynamic> dictPars, Dictionary<string, dynamic> dictHeads = default(Dictionary<string, dynamic>))
        {
            ResponseData<T> res = new ResponseData<T>();
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (dictHeads != null)
                    foreach (KeyValuePair<string, dynamic> item in dictHeads)
                        client.DefaultRequestHeaders.Add(item.Key, item.Value == null ? "" : item.Value.ToString());
                MultipartFormDataContent formData = new MultipartFormDataContent();
                if (dictPars != null)
                    foreach (KeyValuePair<string, dynamic> item in dictPars)
                        formData.Add(new StringContent(item.Value == null ? "" : item.Value.ToString()), item.Key);

                HttpResponseMessage response = client.PostAsync(url, formData).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonres = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<ResponseData<T>>(jsonres);
                }
                else
                {
                    throw new ApiException
                    {
                        StatusCode = (int)response.StatusCode,
                        Content = await response.Content.ReadAsStringAsync()
                    };
                }
                return res;
            }
            catch (ApiException ex)
            {
                res.result = -1;
                res.error = new error() { code = ex.StatusCode, message = ex.Content };
                return res;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error = new error() { code = -1, message = ex.Message };
                return res;
            }

        }
        public async Task<ResponseData<T>> PostResponseDataAsync<T>(string url, MultipartFormDataContent formData, string accessToken = "")
        {
            ResponseData<T> res = new ResponseData<T>();
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (!string.IsNullOrEmpty(accessToken))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                HttpResponseMessage response = client.PostAsync(url, formData).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonres = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<ResponseData<T>>(jsonres);
                }
                else
                {
                    throw new ApiException
                    {
                        StatusCode = (int)response.StatusCode,
                        Content = await response.Content.ReadAsStringAsync()
                    };
                }
                return res;
            }
            catch (ApiException ex)
            {
                res.result = -1;
                res.error = new error() { code = ex.StatusCode, message = ex.Content };
                return res;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error = new error() { code = -1, message = ex.Message };
                return res;
            }

        }
        public async Task<ResponseData<T>> PostResponseDataAsync<T>(string url, FormUrlEncodedContent xwwwFormUrlEndcoded, string accessToken = "")
        {
            ResponseData<T> res = new ResponseData<T>();
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (!string.IsNullOrEmpty(accessToken))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                HttpResponseMessage response = client.PostAsync(url, xwwwFormUrlEndcoded).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonres = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<ResponseData<T>>(jsonres);
                }
                else
                {
                    throw new ApiException
                    {
                        StatusCode = (int)response.StatusCode,
                        Content = await response.Content.ReadAsStringAsync()
                    };
                }
                return res;
            }
            catch (ApiException ex)
            {
                res.result = -1;
                res.error = new error() { code = ex.StatusCode, message = ex.Content };
                return res;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error = new error() { code = -1, message = ex.Message };
                return res;
            }

        }
    }
    public class ResponseData<T>
    {
        public ResponseData()
        {
            result = 0;
            time = Utilities.CurrentTimeSeconds();
            isListData = false;
            dataDescription = string.Empty;
            data = default(T);
            data2nd = null;
            error = new error();
        }
        public int result { get; set; } // 0:fail | 1:success
        public long time { get; set; }
        public bool isListData { get; set; }
        public string dataDescription { get; set; }
        public T data { get; set; }
        public dynamic data2nd { get; set; }
        public error error { get; set; }
    }
    public class M_JResult
    {
        public M_JResult()
        {
            this.result = 0;
            this.error = new error();
            this.data = null;
            this.data2nd = null;
        }
        public M_JResult(dynamic response)
        {
            this.result = response.result;
            this.error = response.error;
            this.data = response.data;
            this.data2nd = response.data2nd;
        }
        public M_JResult(dynamic response, dynamic data, dynamic data2nd = default(dynamic))
        {
            this.result = response.result;
            this.error = response.error;
            this.data = data;
            this.data2nd = data2nd;
        }
        public M_JResult(int result, error error, dynamic data, dynamic data2nd = default(dynamic))
        {
            this.result = result;
            this.data = data;
            this.data2nd = data2nd;
            this.error = error;
        }
        public M_JResult MapData(dynamic response)
        {
            return new M_JResult(response);
        }
        public M_JResult MapData(dynamic response, dynamic data, dynamic data2nd = default(dynamic))
        {
            return new M_JResult(response, data, data2nd);
        }
        public int result { get; set; }
        public error error { get; set; }
        public dynamic data { get; set; }
        public dynamic data2nd { get; set; }
    }
    public class error
    {
        public error()
        {
            code = 200;
            message = string.Empty;
        }
        public error(int _code, string _messege)
        {
            code = _code;
            message = _messege;
        }
        public int code { get; set; }
        public string message { get; set; }
    }

    public class ApiException : Exception
    {
        public int StatusCode { get; set; }
        public string Content { get; set; }
    }
}
