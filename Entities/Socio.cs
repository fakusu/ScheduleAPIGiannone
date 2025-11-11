using Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Socio : IEntidad
    {
        public Socio()
        {
            CoutasPorSocios = new HashSet<CuotaPorSocio>();
        }
        public Socio(string nombre, string apellido, string mail)
        {
            SetNombre(nombre);
            SetApellido(apellido);
            SetEmail(mail);
        }
        #region Properties
        public int Id { get; set; }
        [StringLength(30)]
        public string Nombre { get; private set; }
        [StringLength(30)]
        public string Apellido { get; private set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; private set; }
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string TelefonoMovil { get; set; }
        public string? TelefonoFijo { get; set; }
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

        public void SetApellido(string apellido)
        {
            if (string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El apellido del autor no puede estar vacío.");
            Apellido = apellido;
        }
        public void SetEmail(string mail)
        {
            if (string.IsNullOrWhiteSpace(mail) || (!mail.Contains("@") && !mail.Contains(".com")))
                throw new ArgumentException("El email del autor no puede estar vacío o contener un @.");
            Email = mail;
        }

        public string GetCompleteName()
        {
            return string.Join(", ", Apellido, Nombre);
        }
        #endregion
    }
}
