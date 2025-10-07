using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Db.Models {


    /// <summary>
    /// 
    /// Entidade que representa a capa de um livro. A capa será persistida no banco de dados no formato 
    /// string base 64.
    /// 
    /// <br/> <br/>
    /// 
    /// Ao salvar a capa do livro, serão gerados dois arquivos:
    /// 
    /// <br/> <br/>
    /// 
    /// <list type="number">
    /// 
    /// <item>Arquivo da capa, obtido da página de cadastro, que será gravado no campo Data.</item>
    /// 
    /// <item>Arquivo da miniatura da capa, gerado ao cadastrar, que será gravado no campo Thumbnail.</item>
    /// 
    /// </list>
    /// 
    /// O arquivo de miniatura é obtido do arquivo de capa. Ambos os arquivos serão gravados no formato
    /// de String base 64, e são representados internamente no formato JPEG.
    /// 
    /// </summary>
    [Table("Cover")]
    public class CoverModel {

        /// <summary> Identificador chave primária da capa do livro. </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        /// <summary> Arquivo JPEG da capa do livro, no formato de string base 64. </summary>
        [Required]
        public required string Data { get; set; }

        /// <summary> Arquivo JPEG da miniatura da capa do livro, no formato de string base 64. </summary>
        public string? Thumbnail { get; set; } = null;

        /// <summary> Data de cadastro da capa. </summary>
        [DataType(DataType.DateTime)]
        public required DateTime RegistrationDate { get; set; } = DateTime.Now;

        /// <summary> Data da última atualização de cadastro da capa. </summary>
        [DataType(DataType.DateTime)]
        public required DateTime LastUpdateDate { get; set; } = DateTime.Now;

    }


}