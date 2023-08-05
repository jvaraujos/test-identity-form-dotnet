namespace IdentityForm.Application.Interfaces.Infrastructures
{
    public interface IMemoryCacheRepository
    {
        bool TryGetValue<T>(string key, out T value);
        void SetValue<T>(string key, T value);
        void Remove(string key);
    }
}
