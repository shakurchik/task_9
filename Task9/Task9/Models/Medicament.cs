namespace Task9.Data;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Medicament
{
    [Key]
    public int IdMedicament { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(255)]
    public string Description { get; set; }

    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
}
