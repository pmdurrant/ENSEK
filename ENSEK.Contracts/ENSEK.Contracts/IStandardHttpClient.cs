// ***********************************************************************
// Assembly         : ENSEK.Contracts
// Author           : pauldurrant
// Created          : 07-17-2020
//
// Last Modified By : pauldurrant
// Last Modified On : 07-21-2020
// ***********************************************************************
// <copyright file="IStandardHttpClient.cs" company="PaymentGateway.Contracts">
//     Copyright (c) officeblox. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ENSEK.Entities.Models;

namespace ENSEK.Contracts
{
    /// <summary>
    /// Interface IStandardHttpClient
    /// </summary>
    public interface IStandardHttpClient
    {
        /// <summary>
        /// Gets the string asynchronous.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="authorizationMethod">The authorization method.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        Task<string> GetStringAsync(string uri, string? authorizationToken = null, string authorizationMethod = "Bearer");

        /// <summary>
        /// Gets the string asynchronous.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="authorizationMethod">The authorization method.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        Task<string> GetStringAsync(string uri, Dictionary<string, string> headers, string? authorizationToken = null, string authorizationMethod = "Bearer");

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="authorizationMethod">The authorization method.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        Task<HttpResponseMessage> GetAsync(string uri, Dictionary<string, string>? headers = null, string? authorizationToken = null, string authorizationMethod = "Bearer");

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="item">The item.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="authorizationMethod">The authorization method.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        Task<HttpResponseMessage> PostAsync<T>(string uri, T item, List<HeaderItem>? headers, string? authorizationToken = null, string? requestId = null, string authorizationMethod = "Bearer");

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="authorizationMethod">The authorization method.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        Task<HttpResponseMessage> DeleteAsync(string uri, string? authorizationToken = null, string? requestId = null, string authorizationMethod = "Bearer");

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
        Task<HttpResponseMessage> PutAsync<T>(string uri, T item, string? authorizationToken = null, string? requestId = null, string authorizationMethod = "Bearer");

        /// <summary>
        /// Patches the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="item">The item.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="authorizationMethod">The authorization method.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        Task<HttpResponseMessage> PatchAsync<T>(string uri, T item, string? authorizationToken = null, string? requestId = null, string authorizationMethod = "Bearer");

        /// <summary>
        /// Posts the form asynchronous.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="form">The form.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="authorizationMethod">The authorization method.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        Task<HttpResponseMessage> PostFormAsync(string uri, MultipartFormDataContent form, string? authorizationToken = null, string? requestId = null, string authorizationMethod = "Bearer");

        /// <summary>
        /// Puts the form asynchronous.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="form">The form.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="authorizationMethod">The authorization method.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        Task<HttpResponseMessage> PutFormAsync(string uri, MultipartFormDataContent form, string? authorizationToken = null, string? requestId = null, string authorizationMethod = "Bearer");

        /// <summary>
        /// Posts the form asynchronous.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="form">The form.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="authorizationMethod">The authorization method.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        Task<HttpResponseMessage> PostFormAsync(string uri, FormUrlEncodedContent form, string? authorizationToken = null, string? requestId = null, string authorizationMethod = "Bearer");
    }
}
