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
        public int PatentesReconocidas { get; set; }
        [Column(TypeName = "bigint"), Required]
        public int PatentesNoReconocidas { get; set; }

        [Column(TypeName = "datetime"), Required]
        public DateTime Fecha { get; set; }
    }
}
