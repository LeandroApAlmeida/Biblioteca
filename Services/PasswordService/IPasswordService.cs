namespace Library.Services.PasswordService {


    public interface IPasswordService {
    
        public byte[] GeneratePasswordHash(string password);

        public bool IsItTheSamePassword(string password, byte[] hash);
    
    }


}
