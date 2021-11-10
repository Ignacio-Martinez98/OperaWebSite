using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;//agregar--> se agrega solo con [Required]
using System.ComponentModel.DataAnnotations.Schema;//agregar--> se agrega solo con [Table("...")]
using OperaWebSite.Validations;//agregar para la validacion del año

namespace OperaWebSite.Models
{
    [Table("Opera")]
    public class Opera
    {
        public int OperaID { get; set; }

        [Required(ErrorMessage ="Is required")]//ErrorMessage=--> entre " " el mensaje de error que queremos mostrar
        [StringLength(150)]
        public string Title { get; set; }

        [Required]
        public string Composer { get; set; }

        [CheckValidYear]//validacion perzonalizada para la propiedad Year
        public int Year { get; set; }
    }
}