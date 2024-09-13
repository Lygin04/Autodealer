using Microsoft.AspNetCore.Mvc;
using Autodealer.Dto;
using Autodealer.Model;
using Autodealer.Services;

namespace Autodealer.Controllers;

[Route("[controller]")]
[ApiController]
public class AutoController(IAutoService autoService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(autoService.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var auto = autoService.GetById(id);
            return Ok(auto);
        }
        catch
        {
            return NotFound();
        }
    }

    [HttpPost("create")]
    public IActionResult Create(AutoDto auto)
    {
        Auto createdAuto = autoService.Create(auto);
        return CreatedAtAction(nameof(GetById), new { id = createdAuto.Id }, createdAuto);
    }

    [HttpPut("update")]
    public IActionResult Update(Auto auto)
    {
        try
        {
            autoService.Update(auto);
        }
        catch
        {
            return NotFound();
        }
        return Ok();
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            autoService.Delete(id);
        }
        catch
        {
            return NotFound();
        }
        return Ok();
    }
}