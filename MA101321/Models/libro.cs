using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace MA101321.Models
{
    public class libro
    {
        public int Id { get; set; }
        public string titulo { get; set; }
        public int isbn { get; set; }
        public int anio_edicion { get; set; }
        public string editorial { get; set; }
        public string descripcion { get; set; }
        [ForeignKey("Autor")]
        public int id_autor { get; set; }
        public virtual autor Autor { get; set; }
    }
}
