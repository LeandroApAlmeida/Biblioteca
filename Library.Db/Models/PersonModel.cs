using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Db.Models {


    /// <summary>
    /// 
    /// Entidade que representa uma pessoa física ou pessoa jurídica. O cadastro de pessoa será utilizado
    /// em duas situações:
    /// 
    /// <br/><br/>
    /// 
    /// <list type="number">
    /// 
    /// <item> Doação de um livro: Quando um livro é doado, o beneficiário da doação será uma pessoa física,
    /// ou, eventualmente, uma biblioteca, ambos representados pela entidade. </item>
    /// 
    /// <item> Empréstimo de um livro: O tomador de empréstimo também é representado pela entidade. </item>
    /// 
    /// </list>
    /// 
    /// </summary>
    [Table("Person")]
    public class PersonModel {

        /// <summary> Identificador chave primária da pessoa. </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        /// <summary> Nome da pessoa. </summary>
        [Required(ErrorMessage = "Digite o nome da pessoa")]
        public required string Name { get; set; }

        /// <summary> Rua do endereço da pessoa. </summary>
        public string? Street { get; set; }

        /// <summary> Número do endereço da pessoa. </summary>
        public string? Number { get; set; }

        /// <summary> Bairro do endereço da pessoa. </summary>
        public string? District {  get; set; }

        /// <summary> CEP do endereço da pessoa. </summary>
        public string? PostalCode { get; set; }

        /// <summary> Complemento do endereço da pessoa. </summary>
        public string? Complement { get; set; }

        /// <summary> Cidade do endereço da pessoa. </summary>
        public string? City { get; set; }

        /// <summary> Estado do endereço da pessoa. </summary>
        public string? FederalState { get; set; }

        /// <summary> País do endereço da pessoa. </summary>
        public string? Country { get; set; }

        /// <summary> Descrição da pessoa. </summary>
        public string? Description { get; set; }

        /// <summary> Data de cadastro da pessoa. </summary>
        [DataType(DataType.DateTime)]
        public required DateTime RegistrationDate { get; set; } = DateTime.Now;

        /// <summary> Data da última atualização de cadastro da pessoa. </summary>
        [DataType(DataType.DateTime)]
        public required DateTime LastUpdateDate { get; set; } = DateTime.Now;

        /// <summary> Status de pessoa excluída. </summary>
        public required bool IsDeleted { get; set; } = false;

    }


}