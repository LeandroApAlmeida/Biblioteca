namespace Library.Services {


    public class Response<T> {

        public T? Data {
            get; set;
        }

        public string Message { get; set; } = string.Empty;

        public bool Successful { get; set; } = true;

    }


}
