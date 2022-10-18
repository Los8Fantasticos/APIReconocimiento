using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalAPI_Reconocimiento.Models.ApplicationModel
{
    public class PatenteModel
    {
        [Key, Required]
        public int IdPatente { get; set; }
        [MaxLength(10), Column(TypeName = "nvarchar"), Required]
        public string Patente { get; set; }
        [Column(TypeName = "bit"),Required]
        public bool Active { get; set; }
        [Column(TypeName = "datetime"), Required]
        public DateTime FechaAlta { get; set; }
    }
}