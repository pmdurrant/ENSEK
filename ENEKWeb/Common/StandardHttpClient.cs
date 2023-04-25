// ***********************************************************************
// Assembly         : ENSEKWeb
// Author           : pdurr
// Created          : 04-22-2023
//
// Last Modified By : pdurr
// Last Modified On : 04-22-2023
// ***********************************************************************
// <copyright file="StandardHttpClient.cs" company="ENSEKWeb">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using ENSEK.Contracts;
using ENSEK.Entities.Models;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace ENSEKWeb.Common
{

    /// <summary>
    /// Class StandardHttpClient.
    /// Implements the <see cref="IStandardHttpClient" />
    /// </summary>
    /// <seealso cref="IStandardHttpClient" />
    public class StandardHttpClient : IStandardHttpClient
    {
        /// <summary>
        /// The HTTP context accessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// The client
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        /// The logger
        /// </summary>
        private ILogger<StandardHttpClient> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="StandardHttpClient" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public StandardHttpClient(ILogger<StandardHttpClient> logger, IHttpContextAccessor httpContextAccessor)
        {
            _client = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(60)
            };
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// HTTP GET a string
        /// </summary>
        /// <param name="uri">URI</param>
        /// <param name="authorizationToken">Auth Token</param>
        /// <param name="authorizationMethod">Auth Method, Default Bearer</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public async Task<string> GetStringAsync(string uri, string? authorizationToken = null,
            string authorizationMethod = "Bearer")
        {
            return await GetStringAsync(uri, new Dictionary<string, string>(), authorizationToken, authorizationMethod);
        }

        /// <summary>
        /// HTTP GET a string
        /// </summary>
        /// <param name="uri">URI</param>
        /// <param name="headers">Disctionary of Headers</param>
        /// <param name="authorizationToken">Auth Token</param>
        /// <param name="authorizationMethod">Auth Method, Default Bearer</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public async Task<string> GetStringAsync(string uri, Dictionary<string, string> headers,
            string? authorizationToken = null, string authorizationMethod = "Bearer")
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            foreach (var header in headers) requestMessage.Headers.Add(header.Key, header.Value);

            SetAuthorizationHeader(requestMessage);

            if (authorizationToken != null)
                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue(authorizationMethod, authorizationToken);

            var response = await _client.SendAsync(requestMessage);

            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="authorizationMethod">The authorization method.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        public Task<HttpResponseMessage> GetAsync(string uri, Dictionary<string, string>? headers = null,
            string? authorizationToken = null, string authorizationMethod = "Bearer")
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            if (headers != null)
                foreach (var header in headers)
                    requestMessage.Headers.Add(header.Key, header.Value);

            SetAuthorizationHeader(requestMessage);

            if (authorizationToken != null)
                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue(authorizationMethod, authorizationToken);

            return _client.SendAsync(requestMessage);
        }

        /// <summary>
        /// post as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="item">The item.</param>
        /// <param name="headeritems">The headeritems.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="authorizationMethod">The authorization method.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T item, List<HeaderItem>? headeritems, string? authorizationToken = null,
            string? requestId = null, string authorizationMethod = "Bearer")
        {
            return await DoPostPutAsync(HttpMethod.Post, uri, item, headeritems, authorizationToken, requestId, authorizationMethod);
        }

        /// <summary>
        /// post form as an asynchronous operation.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="form">The form.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="authorizationMethod">The authorization method.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        public async Task<HttpResponseMessage> PostFormAsync(string uri, MultipartFormDataContent form, List<HeaderItem>? headers,
            string? authorizationToken = null, string? requestId = null, string authorizationMethod = "Bearer")
        {
            return await DoPostPutFormAsync(HttpMethod.Post, uri, form, authorizationToken, requestId,
                authorizationMethod);
        }

        /// <summary>
        /// post form as an asynchronous operation.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="form">The form.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="authorizationMethod">The authorization method.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        public async Task<HttpResponseMessage> PostFormAsync(string uri, FormUrlEncodedContent form, List<HeaderItem>? headers,
            string? authorizationToken = null, string? requestId = null, string authorizationMethod = "Bearer")
        {
            return await DoPostPutFormAsync(HttpMethod.Post, uri, form, authorizationToken, requestId,
                authorizationMethod);
        }

        /// <summary>
        /// put form as an asynchronous operation.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="form">The form.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="authorizationMethod">The authorization method.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        public async Task<HttpResponseMessage> PutFormAsync(string uri, MultipartFormDataContent form, List<HeaderItem>? headers,
            string? authorizationToken = null, string? requestId = null, string authorizationMethod = "Bearer")
        {
            return await DoPostPutFormAsync(HttpMethod.Put, uri, form, authorizationToken, requestId,
                authorizationMethod);
        }

        /// <summary>
        /// put as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="item">The item.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="authorizationMethod">The authorization method.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        public async Task<HttpResponseMessage> PutAsync<T>(string uri, T item, List<HeaderItem>? headers, string? authorizationToken = null,
            string? requestId = null, string authorizationMethod = "Bearer")
        {
            return await DoPostPutAsync(HttpMethod.Put, uri, item, headers, authorizationToken, requestId, authorizationMethod);
        }

        /// <summary>
        /// patch as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="item">The item.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="authorizationMethod">The authorization method.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        public async Task<HttpResponseMessage> PatchAsync<T>(string uri, T item, string? authorizationToken = null,
            string? requestId = null, string authorizationMethod = "Bearer")
        {
            return await DoPatchAsync(new HttpMethod("PATCH"), uri, item, authorizationToken, requestId,
                authorizationMethod);
        }

        /// <summary>
        /// delete as an asynchronous operation.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="authorizationMethod">The authorization method.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        public async Task<HttpResponseMessage> DeleteAsync(string uri, string? authorizationToken = null,
            string? requestId = null, string authorizationMethod = "Bearer")
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            SetAuthorizationHeader(requestMessage);

            if (authorizationToken != null)
                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue(authorizationMethod, authorizationToken);

            if (requestId != null) requestMessage.Headers.Add("x-requestid", requestId);

            return await _client.SendAsync(requestMessage);
        }

        /// <summary>
        /// do post put as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method">The method.</param>
        /// <param name="uri">The URI.</param>
        /// <param name="item">The item.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="authorizationMethod">The authorization method.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        private async Task<HttpResponseMessage> DoPostPutAsync<T>(HttpMethod method, string uri, T item, List<HeaderItem>? headers,
            string? authorizationToken = null, string? requestId = null, string authorizationMethod = "Bearer")
        {

            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");


            var response = await DoPostPutFormAsync(method, uri, content, authorizationToken, requestId,
                authorizationMethod);

            return response;
        }

        /// <summary>
        /// do post put form as an asynchronous operation.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="uri">The URI.</param>
        /// <param name="content">The content.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="authorizationMethod">The authorization method.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        /// <exception cref="System.ArgumentException">Value must be either post or put. - method</exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        private async Task<HttpResponseMessage> DoPostPutFormAsync(HttpMethod method, string uri, HttpContent content,
            string? authorizationToken = null, string? requestId = null, string authorizationMethod = "Bearer")
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
                throw new ArgumentException("Value must be either post or put.", nameof(method));

            // a new StringContent must be created for each retry
            // as it is disposed after each call

            var requestMessage = new HttpRequestMessage(method, uri)
            {

                //    SetAuthorizationHeader(requestMessage);

                Content = content
            };

            //todo determine if token is needed for the provider


            //if (authorizationToken != null)
            requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue(authorizationMethod, authorizationToken);

            if (requestId != null) requestMessage.Headers.Add("x-requestid", requestId);

            var response = await _client.SendAsync(requestMessage);

            // raise exception if HttpResponseCode 500
            // needed for circuit breaker to track fails

            if (response.StatusCode == HttpStatusCode.InternalServerError) throw new HttpRequestException();

            return response;
        }

        /// <summary>
        /// do patch as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method">The method.</param>
        /// <param name="uri">The URI.</param>
        /// <param name="item">The item.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="authorizationMethod">The authorization method.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        private async Task<HttpResponseMessage> DoPatchAsync<T>(HttpMethod method, string uri, T item,
            string? authorizationToken = null, string? requestId = null, string authorizationMethod = "Bearer")
        {
            var requestMessage = new HttpRequestMessage(method, uri);

            SetAuthorizationHeader(requestMessage);

            requestMessage.Content =
                new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

            if (authorizationToken != null)
                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue(authorizationMethod, authorizationToken);

            if (requestId != null) requestMessage.Headers.Add("x-requestid", requestId);

            var response = await _client.SendAsync(requestMessage);

            // raise exception if HttpResponseCode 500
            // needed for circuit breaker to track fails

            if (response.StatusCode == HttpStatusCode.InternalServerError) throw new HttpRequestException();

            return response;
        }

        /// <summary>
        /// Sets the authorization header.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        private void SetAuthorizationHeader(HttpRequestMessage requestMessage)
        {
#pragma warning disable CS8602
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
#pragma warning restore CS8602
            if (!string.IsNullOrEmpty(authorizationHeader))
#pragma warning disable CS8604
                requestMessage.Headers.Add("Authorization", new List<string> { authorizationHeader });
#pragma warning restore CS8604
        }

        /// <summary>
        /// Puts the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="item">The item.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="authorizationMethod">The authorization method.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<HttpResponseMessage> PutAsync<T>(string uri, T item, string? authorizationToken = null, string? requestId = null, string authorizationMethod = "Bearer")
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Posts the form asynchronous.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="form">The form.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="authorizationMethod">The authorization method.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<HttpResponseMessage> PostFormAsync(string uri, MultipartFormDataContent form, string? authorizationToken = null, string? requestId = null, string authorizationMethod = "Bearer")
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Puts the form asynchronous.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="form">The form.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="authorizationMethod">The authorization method.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<HttpResponseMessage> PutFormAsync(string uri, MultipartFormDataContent form, string? authorizationToken = null, string? requestId = null, string authorizationMethod = "Bearer")
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Posts the form asynchronous.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="form">The form.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="authorizationMethod">The authorization method.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<HttpResponseMessage> PostFormAsync(string uri, FormUrlEncodedContent form, string? authorizationToken = null, string? requestId = null, string authorizationMethod = "Bearer")
        {
            throw new NotImplementedException();
        }
    }
}
