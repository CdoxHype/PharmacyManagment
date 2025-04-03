using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models
{
    public class Prescription
    {
        public int Id {get; set;}
        public string PatientName {get;set;} = string.Empty;
        public string MedicationName {get;set;} = string.Empty;
        public int DosageInMg {get;set;}
        public DateTime PrescriptionDate {get;set;}
        public bool IsRefillable {get;set;}
    }
}