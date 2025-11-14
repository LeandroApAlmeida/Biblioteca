namespace Library.Services.User {
    
    
    /// <summary>
    /// Interface que define um gerenciador de configurações do usuário.
    /// </summary>
    public interface ISettingsService {


        /// <summary>
        /// Obter o valor String de uma configuração.
        /// </summary>
        /// <param name="key">Chave da configuração.</param>
        /// <param name="defaultValue">Valor default da configuração.</param>
        /// <returns>Valor String da configuração.</returns>
        public string? GetString(string key, string? defaultValue);


        /// <summary>
        /// Obter o valor Int de uma configuração.
        /// </summary>
        /// <param name="key">Chave da configuração.</param>
        /// <param name="defaultValue">Valor default da configuração.</param>
        /// <returns>Valor Int da configuração.</returns>
        public int? GetInt(string key, int? defaultValue);


        /// <summary>
        /// Obter o valor Long de uma configuração.
        /// </summary>
        /// <param name="key">Chave da configuração.</param>
        /// <param name="defaultValue">Valor default da configuração.</param>
        /// <returns>Valor Long da configuração.</returns>
        public long? GetLong(string key, long? defaultValue);


        /// <summary>
        /// Obter o valor Float de uma configuração.
        /// </summary>
        /// <param name="key">Chave da configuração.</param>
        /// <param name="defaultValue">Valor default da configuração.</param>
        /// <returns>Valor Float da configuração.</returns>
        public float? GetFloat(string key, float? defaultValue);


        /// <summary>
        /// Obter o valor Double de uma configuração.
        /// </summary>
        /// <param name="key">Chave da configuração.</param>
        /// <param name="defaultValue">Valor default da configuração.</param>
        /// <returns>Valor Double da configuração.</returns>
        public double? GetDouble(string key, double? defaultValue);


        /// <summary>
        /// Obter o valor Boolean de uma configuração.
        /// </summary>
        /// <param name="key">Chave da configuração.</param>
        /// <param name="defaultValue">Valor default da configuração.</param>
        /// <returns>Valor Boolean da configuração.</returns>
        public bool? GetBoolean(string key, bool? defaultValue);


        /// <summary>
        /// Definir o valor String de uma configuração.
        /// </summary>
        /// <param name="key">Chave da configuração.</param>
        /// <param name="value">Valor String da configuração, ou null</param>
        public void SetString(string key, string? value);


        /// <summary>
        /// Definir o valor Int de uma configuração.
        /// </summary>
        /// <param name="key">Chave da configuração.</param>
        /// <param name="value">Valor Int da configuração, ou null</param>
        public void SetInt(string key, int? value);


        /// <summary>
        /// Definir o valor Long de uma configuração.
        /// </summary>
        /// <param name="key">Chave da configuração.</param>
        /// <param name="value">Valor Long da configuração, ou null</param>
        public void SetLong(string key, long? value);


        /// <summary>
        /// Definir o valor Float de uma configuração.
        /// </summary>
        /// <param name="key">Chave da configuração.</param>
        /// <param name="value">Valor Float da configuração, ou null</param>
        public void SetFloat(string key, float? value);


        /// <summary>
        /// Definir o valor Double de uma configuração.
        /// </summary>
        /// <param name="key">Chave da configuração.</param>
        /// <param name="value">Valor Double da configuração, ou null</param>
        public void SetDouble(string key, double? value);


        /// <summary>
        /// Definir o valor Boolean de uma configuração.
        /// </summary>
        /// <param name="key">Chave da configuração.</param>
        /// <param name="value">Valor Boolean da configuração, ou null</param>
        public void SetBoolean(string key, bool? value);


        /// <summary>
        /// Redefine todas as configurações para os valores default.
        /// </summary>
        public void Reset();


    }


}