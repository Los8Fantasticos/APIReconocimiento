using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalAPI_Reconocimiento.Models.ApplicationModel
{
    public class TraficoModel
    {
        [Key, Required]
        public int IdTrafico { get; set; }
        //column type bigint
        [Column(TypeName = "bigint"), Required]
        public Int64 PatentesReconocidas { get; set; }
        [Column(TypeName = "bigint"), Required]
        public Int64 PatentesNoReconocidas { get; set; }

        [Column(TypeName = "datetime"), Required]
        public DateTime Fecha { get; set; }
    }
}
