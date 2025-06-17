using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Db.Models {


    [Table("Settings")]
    [PrimaryKey(nameof(Id), nameof(UserId))]
    public class SettingsModel {

        public required string Id { get; set; }

        public virtual Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public required UserModel User { get; set; }

        public string? StringValue { get; set; }

        public int? IntValue { get; set; }

        public long? LongValue { get; set; }

        public float? FloatValue { get; set; }

        public double? DoubleValue { get; set; }

        public bool? BoolValue { get; set; }

    }


}