namespace Library.Services.User {
    
    
    public interface ISettingsService {

        public string? GetString(string key, string? defaultValue);

        public int? GetInt(string key, int? defaultValue);

        public long? GetLong(string key, long? defaultValue);

        public float? GetFloat(string key, float? defaultValue);

        public double? GetDouble(string key, double? defaultValue);

        public bool? GetBoolean(string key, bool? defaultValue);

        public void SetString(string key, string? value);

        public void SetInt(string key, int? value);

        public void SetLong(string key, long? value);

        public void SetFloat(string key, float? value);

        public void SetDouble(string key, double? value);

        public void SetBoolean(string key, bool? value);

        public void Reset();

    }


}