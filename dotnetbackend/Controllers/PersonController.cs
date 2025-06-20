
using dotnetbackend.Dtos.Person;
using dotnetbackend.IRepository;
using dotnetbackend.IServices;
using Microsoft.AspNetCore.Mvc;


namespace dotnetbackend.Controllers
{

  [Route("api/person")]
  [ApiController]
  public class PersonController : ControllerBase
  {
    private readonly IPersonService _personService;

    public PersonController( IPersonService personService)
    {
      _personService = personService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var persons = await _personService.GetAllAsync();
      if (persons == null || persons.Count == 0)
      {
        return NotFound("No persons found");
      }
      return Ok(persons);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var person = await _personService.GetByIdAsync(id);
      if (person is null)
      {
        return NotFound(new{
          Message = $"Person with id {id} not found"
        });
      }
      return Ok(person);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonRequest personDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      if (personDto == null)
      {
        return BadRequest("Person data is required");
      }
      var person = await _personService.CreateAsync(personDto);
  
      return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePersonRequest personDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      if (personDto is null)
      {
        return BadRequest(new
        {
          Message = "Person data is required"
        });
      }
      var updatedPerson = await _personService.UpdateAsync(id, personDto);

      if (updatedPerson is null)
      {
        return NotFound(new
        {
          Message = $"Person with id {id} not found"
        });
      }
      return Ok(updatedPerson);
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var wasDeleted = await _personService.DeleteAsync(id);
      if (!wasDeleted)
      {
        return NotFound(new
        {
          Message = $"Person with id {id} not found"
        });
      }
      return NoContent();
    }
  }
}