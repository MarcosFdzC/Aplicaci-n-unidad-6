using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Disco
    {
        public int Id { get; set; }
        [DisplayName("Título")]
        public string  Titulo { get; set; }
        [DisplayName("Lanzamiento")]
        public  DateTime FechaLanzamiento { get; set; }
        [DisplayName("Número de canciones")]
        public  int CantCanciones { get; set; }
        public string ImgUrl { get; set; }
        public Estilo Estilo { get; set; }
        [DisplayName("Edición")]
        public Edicion Edicion { get; set; }
    }
}
