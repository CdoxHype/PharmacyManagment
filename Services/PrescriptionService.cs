using Microsoft.EntityFrameworkCore;
using Pharmacy.Data;
using Pharmacy.Models;

namespace Pharmacy.Services;

//Notes: we are using async methods to be able to process database operations
// while not stoping the app helps in efficiency 


public class PrescriptionService
{
    private readonly PrescriptionContext _context; //We make this can only be assigned in the constructor

    //Constructor
    public PrescriptionService(PrescriptionContext context)
    {
        _context = context;
    }

    //async = it does not block execution time while waiting for the database operation to complete
    //Task is a type that allows to run a task (in the background) that will be returning something in a certain time without stoping the program
    public async Task<string> ValidateAndAddPrescription(Prescription prescription)
    {
        // Rule 1: Dosage must be between 1 and 1000 mg
        if(prescription.DosageInMg < 1 || prescription.DosageInMg > 1000)
        {
            return "Error: Dosage must be between 1 and 1000 mg.";
        }
        // Rule 2: Prescription date can't be in the future
        if(prescription.PrescriptionDate > DateTime.Now)
        {
            return "Error: Prescription date cannot be in the future.";
        }
        //Rule 3: Patient name and medication name cannot be empty
        if(prescription.PatientName == string.Empty || prescription.MedicationName == string.Empty)
        {
            return "Error: Patient name and medication name are required.";
        }

        _context.Prescriptions.Add(prescription); //Adding prescription to database not saving yet
        await _context.SaveChangesAsync(); // await allows the program to continue while waiting for this save operation to complete
        return "Prescription added successfully.";

    }

    public async Task<List<Prescription>> GetAllPrescriptions()
    {
        return await _context.Prescriptions.ToListAsync();
    }

    public async Task<Prescription> GetPrescriptionById(int id)
    {
        return await _context.Prescriptions.FindAsync(id) ?? throw new Exception("Prescription not found.");
    }

    public async Task<string> UpdatePrescription(int id, Prescription updatedPrescription)
    {
        var prescription = await _context.Prescriptions.FindAsync(id);
        if (prescription == null)
        {
            return "Error: Prescription not found";
        }
        prescription.PatientName = updatedPrescription.PatientName;
        prescription.MedicationName = updatedPrescription.MedicationName;
        prescription.DosageInMg = updatedPrescription.DosageInMg;
        prescription.IsRefillable = updatedPrescription.IsRefillable;

        //Revalidating that everything is okay
        var validationResult = await ValidateAndAddPrescription(prescription);
        if(!validationResult.Contains("successfully")){
            return validationResult;
        }

        await _context.SaveChangesAsync();
        return "Prescription Updated successfully.";
    }

    public async Task<string> DeletePrescription(int id)
    {
        var prescription = await _context.Prescriptions.FindAsync(id);
        if(prescription == null){
            return "Error: Prescription not found.";
        }
        _context.Prescriptions.Remove(prescription); //This create a SQL DELETE statement automatically
        await _context.SaveChangesAsync();
        return "Prescription deleted successfully.";

    }

}