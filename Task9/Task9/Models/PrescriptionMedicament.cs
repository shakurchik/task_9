namespace Task9.Data;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PrescriptionMedicament
{
    [Key, Column(Order = 0)]
    public int IdPrescription { get; set; }
    public Prescription Prescription { get; set; }

    [Key, Column(Order = 1)]
    public int IdMedicament { get; set; }
    public Medicament Medicament { get; set; }

    [Required]
    public int Dose { get; set; }
}
