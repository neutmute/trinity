namespace Trinity
{
    public interface IAppSettingRepository
    {
        TResult Get<TResult>(string key);
        long Increment(string key);
        bool Exists(string key);
        void Set(string key, string value);
        void Set(string key, int value);
    }
}