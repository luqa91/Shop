﻿namespace Shop.Infrastructure
{
    public interface ISessionManager
    {
    
    T Get<T>(string key);
    void Set<T>(string name, T value);
    void Abandon();
    T TryGet<T>(string key);

}
}