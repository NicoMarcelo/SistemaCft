using System;
using System.Collections.Generic;

namespace SistemaCft.Models;

public partial class Asignatura
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public DateOnly? FechaActualizacion { get; set; }

    public virtual ICollection<Asignaturaasignada> Asignaturaasignada { get; set; } = new List<Asignaturaasignada>();
}
