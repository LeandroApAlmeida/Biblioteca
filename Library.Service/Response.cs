namespace Library.Services {


    /// <summary>
    /// Objeto de resposta de uma consulta.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T> {

        /// <summary> Objeto retornado na consulta. </summary>
        public T? Data { get; set; }

        /// <summary> Mensagem da resposta. </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary> Status de sucesso da consulta. </summary>
        public bool Successful { get; set; } = true;

    }


}
