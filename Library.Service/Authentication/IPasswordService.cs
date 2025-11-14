namespace Library.Services.Authentication {


    /// <summary>
    /// Interface que define um serviço de senha. 
    /// </summary>
    public interface IPasswordService {
    

        /// <summary>
        /// Gerar o hash da senha passada como parâmetro.
        /// </summary>
        /// <param name="password">Senha a se obter o hash.</param>
        /// <returns>Hash da senha gerado.</returns>
        public byte[] GeneratePasswordHash(string password);


        /// <summary>
        /// Verificar se a senha passada é a mesma a que foi gerado o hash gravado no banco
        /// de dados.
        /// </summary>
        /// <param name="password">Senha a ser comparada.</param>
        /// <param name="passwordHash">Hash da senha gravado no banco de dados.</param>
        /// <returns>True, caso a senha seja a mesma que gerou o hash. False, caso o contrário.</returns>
        public bool IsTheSamePassword(string password, byte[] hash);
    

    }


}
