using Task9.Data;
using Task9.Dto;

namespace Task9.Controller;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionController : ControllerBase
{
    private readonly PrescriptionContext _context;

    public PrescriptionController(PrescriptionContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] PrescriptionDto dto)
    {
        if (dto.Medicaments.Count > 10)
            return BadRequest("A prescription can include a maximum of 10 medications.");

        if (dto.DueDate < dto.Date)
            return BadRequest("DueDate must be greater than or equal to Date.");

        var patient = await _context.Patients.FindAsync(dto.Patient.IdPatient);
        if (patient == null)
        {
            patient = new Patient
            {
                IdPatient = dto.Patient.IdPatient,
                FirstName = dto.Patient.FirstName,
                LastName = dto.Patient.LastName,
                Birthdate = dto.Patient.Birthdate
            };
            _context.Patients.Add(patient);
        }

        foreach (var medicament in dto.Medicaments)
        {
            var med = await _context.Medicaments.FindAsync(medicament.IdMedicament);
            if (med == null)
                return BadRequest($"Medicament with Id {medicament.IdMedicament} does not exist.");
        }

        var prescription = new Prescription
        {
            Date = dto.Date,
            DueDate = dto.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = dto.Doctor.IdDoctor
        };

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        foreach (var medicament in dto.Medicaments)
        {
            _context.PrescriptionMedicaments.Add(new PrescriptionMedicament
            {
                IdPrescription = prescription.IdPrescription,
                IdMedicament = medicament.IdMedicament,
                Dose = medicament.Dose
            });
        }

        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("{idPatient}")]
    public async Task<IActionResult> GetPatientData(int idPatient)
    {
        var patient = await _context.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.Doctor)
            .FirstOrDefaultAsync(p => p.IdPatient == idPatient);

        if (patient == null)
            return NotFound();

        return Ok(new
        {
            patient,
            prescriptions = patient.Prescriptions.OrderBy(p => p.DueDate)
        });
    }
}
