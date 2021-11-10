using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;//agregar
using OperaWebSite.Models;//agregar 

namespace OperaWebSite.Data
{
    //Esta clase elimina y crea la base de datos para realizar pruebas de inicializacion
    public class OperasInitializer : DropCreateDatabaseAlways<OperaDbContext>
    {
        protected override void Seed(OperaDbContext context)
        {
            var operas = new List<Opera>  //creamos una lista
            {
               new Opera {                //agregamos un elemento
                  Title = "Cosi Fan Tutte",
                  Year = 1790,
                  Composer = "Mozart"
               }
            };
            operas.ForEach(s => context.Operas.Add(s));
            context.SaveChanges();


        }
    }

}