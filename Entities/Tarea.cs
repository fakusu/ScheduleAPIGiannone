using Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Tarea:IEntidad
    {
        public Tarea()
        {
                UsuariosPorTareas = new HashSet<UsuarioPorTarea>();
                Historial = new List<HistorialTarea>();
        }
        #region Properties
        public int Id { get; set; }
        [StringLength(30)]
        public string Titulo { get; set; }
        [StringLength(30)]
        public string  Descripcion { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaVencimiento { get; set; }
        [ForeignKey(nameof(TipoTarea))]
        public int TipoTareaId { get; set; }

        public int Valor { get; set; }

        public bool Completada { get; set; }
        #endregion

        #region Virtual
        public virtual ICollection<UsuarioPorTarea> UsuariosPorTareas { get; set; }
        public virtual List<HistorialTarea> Historial { get; set; }
        public virtual TipoTarea TipoTarea { get; set; }
        #endregion

        #region setters y getters
        public void SetTitulo(string titulo) 
        { 
           if (string.IsNullOrWhiteSpace(titulo))
            {
                throw new ArgumentException("El titulo no puede estar vacío.");
            }
            Titulo = titulo;
        }
        public string GetClassName()
        {
            return string.Join(":", this.GetType().BaseType.Name, Titulo);
        }
        #endregion
    }
}
