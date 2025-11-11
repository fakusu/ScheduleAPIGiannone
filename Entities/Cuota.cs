using Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Cuota : IEntidad, IClassMethods
    {
        public Cuota()
        {
            CuotaPorSocios = new HashSet<CuotaPorSocio>();
        }
        #region Properties
        public int Id { get; set; }
        public decimal Valor { get; private set; }
        public DateTime CaducaEn { get; private set; }
        [ForeignKey(nameof(Mes))]
        public int IdMes { get; set; }
        [ForeignKey(nameof(Anio))]
        public int IdAnio { get; set; }
        #endregion
        #region Virtual
        public virtual Mes Mes { get; set; }
        public virtual Anio Anio { get; set; }
        public virtual ICollection<CuotaPorSocio> CuotaPorSocios { get; set; }
        #endregion
        #region setters y getters
        public void SetValor(decimal valor)
        {
            if (valor < 0)
                throw new ArgumentException("El valor debe ser mayor a 0.");
            Valor = valor;
        }

        public void SetCaducidad(DateTime caducaEn)
        {
            if (caducaEn < DateTime.Now)
                throw new ArgumentException("La caducidad debe ser mayor a la fecha actual.");
            CaducaEn = caducaEn;
        }
        public string GetClassName()
        {
            return string.Join(": ", Anio.GetClassName(), Mes.GetClassName(), this.GetType().BaseType.Name, Valor.ToString());
        }
        #endregion
    }
}
