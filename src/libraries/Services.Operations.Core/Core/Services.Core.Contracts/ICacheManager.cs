using System.Threading.Tasks;

namespace Services.Core.Contracts
{
    public interface ICacheManager : ICompositionPart
    {
        void Initialize<T> (T configuration);
        Task InsertAsync (string key, object value);
        Task InsertAsync (string key, object value, ICacheItemPolicy policy);
        void Insert (string key, object value);
        void Insert (string key, object value, ICacheItemPolicy policy);
        bool Contains (string key);
        Task<bool> ContainsAsync (string key);
        int Count { get; }
        object Get (string key);
        Task<object> GetAsync (string key);
        T Get<T> (string key);
        Task<T> GetAsync<T> (string key);
        void Remove (string key);
        object this[string key] { get; }        
    }
}
