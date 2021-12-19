namespace MCDStorageChest.Save.Json.Mapping
{
    public interface INamingPolicy<T>
    {
        public T ConvertName(string name);
        public string ConvertValue(T value);
    }
}
