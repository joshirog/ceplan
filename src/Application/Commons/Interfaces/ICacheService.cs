namespace Application.Commons.Interfaces;

public interface ICacheService
{
    T Get<T>(string value);

    T GetOrAddDaily<T>(string value, T item);

    void Remove(string value);

    string Template(string key);
}