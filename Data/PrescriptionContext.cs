using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Models;

namespace Pharmacy.Data;

//This class inherits from DbContext which is a built-in EF Core Class
//Db context allows the class to interact with a database
public class PrescriptionContext : DbContext
{
    //Constructor (options) = database connection settings pased in main
    public PrescriptionContext(DbContextOptions<PrescriptionContext> options) : base(options){}
    //Methods 
    public DbSet<Prescription> Prescriptions {get;set;}
}