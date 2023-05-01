using Microsoft.AspNetCore.Mvc;
using UPB.PracticeThree.Models;

namespace UPB.PracticeThree.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    public PatientController()
    {       
    }

    [HttpGet]
    public List<Patient> Get()
    {
        return new List<Patient>();
    }
    [HttpPut]
    public Patient Put()
    {
        return new Patient();
    }
    [HttpPost]
    public Patient Post()
    {
        return new Patient();
    }
    [HttpDelete]
    public Patient Delete()
    {
        return new Patient();
    }
}
