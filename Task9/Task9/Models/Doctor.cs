namespace Task9.Data;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Doctor
{
    [Key]
    public int IdDoctor { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    public ICollection<Prescription> Prescriptions { get; set; }
}

