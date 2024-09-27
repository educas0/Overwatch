using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overwatch.Models.Interface
{
    public interface IDBAccess
    {

        #region ...Interface RECOGIDA de Datos...

        //interfaz recogida lista de heroes
        public List<Heroe> RecuperarListaHeroes();




        #endregion

        #region ...Interface BORRADO de Datos...

        #endregion

        #region ...Interface INSERCION de Datos...
        //interfaz registro heroe
        public bool RegistrarHeroe(Heroe NuevoHeroe);
        Heroe RecuperarListaHeroes(int idHeroe);


        #endregion

        #region ...Interface ACTUALIZACIÓN de Datos...

        #endregion

    }

}
