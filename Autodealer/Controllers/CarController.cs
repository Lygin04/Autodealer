using Autodealer.Dto;
using Autodealer.Model;
using Autodealer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Autodealer.Controllers;

[Route("[controller]")]
[ApiController]
public class CarController(ICarService carService) : ControllerBase
{
    [HttpGet("/Cars")]
    public async Task<IEnumerable<Car>> GetAll()
    {
        return await carService.GetAll();
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        try
        {
            var auto = carService.GetById(id);
            return Ok(auto);
        }
        catch
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(CarDto car)
    {
        var createdAuto = await carService.Create(car);
        return CreatedAtAction(nameof(GetById), new { id = createdAuto.Id }, createdAuto);
    }

    [HttpPut]
    public async Task<IActionResult> Update(Car car)
    {
        try
        {
            await carService.Update(car);
        }
        catch
        {
            return NotFound();
        }
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            await carService.Delete(id);
        }
        catch
        {
            return NotFound();
        }
        return Ok();
    }
}