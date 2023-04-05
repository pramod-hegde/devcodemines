using System;
using System.Threading.Tasks;

namespace Services.Core.Contracts
{
    public delegate Task<object> AuthenticationCallback();

    public interface ICommunicationClient : ICompositionPart
    {
        string this[string key] { get; }
        Task<T> GetAsync<T>(string resourceUri);
        Task PostAsync(string resourceUri, object content);
        Task<T> PostAsync<T>(string resourceUri, object content);
        void AddHeader(string name, string value);
        void ConfigureClient<TCache>(string baseUri, AuthenticationCallback authCallback = null, TCache cache = default, TimeSpan timeout = default);
    }
}
