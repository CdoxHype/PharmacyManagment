using Microsoft.AspNetCore.Mvc;
using Pharmacy.Models;
using Pharmacy.Services;

namespace Pharmacy.Controllers;

//This define the base route for all actions, the controller is replaced with by the name of the controller
// in this case PrescriptionController sou the route will be api/prescriptions
[Route("api/[controller]")]
//This attribute is used to mark the class as an API controller which give some perks like
// Consistent responses for HTTP status code, simplifies binding parameters to request data etc
[ApiController]

//This class is responsible to handle all HTTP request related to prescriptions
// ControllerBase is a base class for API controllers in ASP.NET
// Provides core functionalities such returning responses, validation and model binding
public class PrescriptionController : ControllerBase
{
    //Holds the prescription instance injected via the constructor
    private readonly PrescriptionService _prescriptionService;
    //Constructor
    public PrescriptionController(PrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    // GET: api/prescriptions
    // This tell that this method should handle HTTP GET requests.
    //This method fetch all prescriptions from the service.
    //ActionResult will return a list of Prescriptions objects wrapped in HTTP response.
    [HttpGet]
    public async Task<ActionResult<List<Prescription>>> GetPrescriptions()
    {
        // Calls GetAllPrescriptions in the Prescription Service which is already Async
        var prescriptions = await _prescriptionService.GetAllPrescriptions();
        return Ok(prescriptions); //Return a 200 status code with the list of descriptions
    }

    // GETl: api/prescriptions/5 Example
    [HttpGet("{id}")]
    public async Task<ActionResult<Prescription>> GetPrescription(int id)
    {
        try
        {
            var prescription = await _prescriptionService.GetPrescriptionById(id);
            return Ok(prescription);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message); //This throws a 400 status code with error message
        }

    }
    // POST: api/prescriptions
    // [FromBody] indicates that Prescription object will be passed to the HTTP request body
    // and ASP.Net CORE will bind the incoming JSON data to the prescription object
    [HttpPost]
    public async Task<ActionResult<string>> AddPrescription([FromBody] Prescription prescription)
    {
        var result = await _prescriptionService.ValidateAndAddPrescription(prescription);
        if (result.Contains("Error"))
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    // PUT: api/prescriptions/5 Example
    //This specifies that the method should handle HTTP PUT to update a existing prescription by id.
    [HttpPut("{id}")]
    public async Task<ActionResult<string>> UpdatePrescription(int id, [FromBody] Prescription prescription)
    {
        var result = await _prescriptionService.UpdatePrescription(id, prescription);
        if (result.Contains("Error"))
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    // DELETE: api/prescriptions/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> DeletePrescription(int id)
    {
        var result = await _prescriptionService.DeletePrescription(id);
        if (result.Contains("Error"))
        {
            return NotFound(result);
        }
        return Ok(result);
    }

}