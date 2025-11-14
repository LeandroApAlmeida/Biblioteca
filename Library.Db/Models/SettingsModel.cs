using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Db.Models {


    /// <summary>
    /// Entidade que representa as configurações do usuário em sessão.
    /// </summary>
    [Table("Settings")]
    [PrimaryKey(nameof(Id), nameof(UserId))]
    public class SettingsModel {

        /// <summary> Identificador (chave) da configuração. </summary>
        public required string Id { get; set; }

        /// <summary> Identificador chave primária do usuário em sessão. </summary>
        public virtual Guid UserId { get; set; }

        /// <summary> Instância do usuário em sessão. </summary>
        [ForeignKey("UserId")]
        public required UserModel User { get; set; }

        /// <summary> Valor no formato string da configuração. </summary>
        public string? StringValue { get; set; }

        /// <summary> Valor no formato integer da configuração. </summary>
        public int? IntValue { get; set; }

        /// <summary> Valor no formato long da configuração. </summary>
        public long? LongValue { get; set; }

        /// <summary> Valor no formato float da configuração. </summary>
        public float? FloatValue { get; set; }

        /// <summary> Valor no formato double da configuração. </summary>
        public double? DoubleValue { get; set; }

        /// <summary> Valor no formato boolean da configuração. </summary>
        public bool? BoolValue { get; set; }

    }


}