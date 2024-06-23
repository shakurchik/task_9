namespace Task9.Data;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
public class Patient
{
    [Key]
    public int IdPatient { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required]
    public DateTime Birthdate { get; set; }

    public ICollection<Prescription> Prescriptions { get; set; }
}


