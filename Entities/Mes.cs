using Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Mes : IEntidad, IClassMethods
    {
        public Mes()
        {
            CoutasPorSocios = new HashSet<CuotaPorSocio>();
        }
        public Mes(string nombre)
        {
            SetNombre(nombre);
        }
        #region Properties
        public int Id { get; set; }
        public string Nombre { get; private set; }
        #endregion
        #region Virtual
        public virtual ICollection<CuotaPorSocio> CoutasPorSocios { get; set; }
        #endregion
        #region setters y getters
        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del autor no puede estar vacío.");
            Nombre = nombre;
        }
        public string GetName()
        {
            return string.Join(": ", "Mes", Nombre);
        }

        public string GetClassName()
        {
            return string.Join(": ", this.GetType().BaseType.Name, Nombre);
        }
        #endregion
    }
}
