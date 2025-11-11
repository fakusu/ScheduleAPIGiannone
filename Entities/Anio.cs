using Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Anio : IEntidad, IClassMethods
    {
        public Anio()
        {
            CoutasPorSocios = new HashSet<CuotaPorSocio>();
        }

        public Anio(int numero)
        {
            this.SetAnio(numero);
        }


        #region Properties
        public int Id { get; set; }
        public int Numero { get; private set; }
        #endregion
        #region Virtual
        public virtual ICollection<CuotaPorSocio> CoutasPorSocios { get; set; }
        #endregion
        #region setters y getters
        public void SetAnio(int anio)
        {
            if (anio < 0)
                throw new ArgumentException("El año dbe ser mayor a 0.");
            Numero = anio;
        }
        public string GetClassName()
        {
            return string.Join(": ", this.GetType().BaseType.Name, Numero.ToString());
        }
        #endregion
    }
}
