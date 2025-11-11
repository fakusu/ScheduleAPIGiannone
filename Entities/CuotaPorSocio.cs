using Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CuotaPorSocio : IEntidad
    {
        #region Properties
        public CuotaPorSocio() { }
        public CuotaPorSocio(Cuota cuota)
        {
            SetValor(cuota);
            SetRecargo(cuota);
            SetTotal();
        }
        public int Id { get; set; }
        [ForeignKey(nameof(Socio))]
        public int IdSocio { get; set; }
        [ForeignKey(nameof(Cuota))]
        public int IdCouta { get; set; }
        public decimal Valor { get; private set; }
        public decimal Recargo { get; private set; }
        public decimal Total { get; private set; }
        public DateTime? FechaPago { get; set; }
        #endregion

        #region Virtual
        public virtual Socio Socio { get; set; }
        public virtual Cuota Cuota { get; set; }
        #endregion

        #region setters y getters
        public void SetValor(Cuota cuota)
        {
            Valor = cuota.Valor;
        }

        public void SetRecargo(Cuota cuota)
        {
            var anioActual = DateTime.Today.Year;
            var mesActual = DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture);
            if (anioActual == cuota.Anio.Numero && mesActual == cuota.Mes.Nombre)
            {
                Recargo = 1;
            }
            else
            {
                var diferenciaMes = DateTime.Today.Month - cuota.Mes.Id;
                Recargo = anioActual - cuota.Anio.Numero < 0 ? (diferenciaMes + 12) * 3 : diferenciaMes > 0 ? 3 * diferenciaMes : 0;
            }
        }
        public void SetTotal()
        {
            if (this.Valor == 0 || this.Recargo == 0)
                throw new ArgumentException("Para realizar el cálculo es necesario cargar el valor y el recargo de la cuota.");
            Total = this.Valor + this.Recargo;
        }
        #endregion
    }
}
