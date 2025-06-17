namespace Library.Services.Authentication {


    public interface IPasswordService {
    
        public byte[] GeneratePasswordHash(string password);

        public bool IsTheSamePassword(string password, byte[] hash);
    
    }


}
