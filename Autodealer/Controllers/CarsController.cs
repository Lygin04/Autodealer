using Autodealer.Dto;
using Autodealer.Model;
using Autodealer.Services;
using Autodealer.Services.Caching;
using Microsoft.AspNetCore.Mvc;

namespace Autodealer.Controllers;

[Route("[controller]")]
[ApiController]
public class CarsController(ICarService carService, IRedisCacheService cache) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<Car>?> GetAll()
    {
        try
        {
            var userId = Request.Headers["UserId"];

            var cachingKey = $"cars_{userId}";
            var cars = cache.GetData<IEnumerable<Car>>(cachingKey);
            if (cars is null)
            {
                cars = await carService.GetAll();
                cache.SetData(cachingKey, cars);
            }

            return cars;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        try
        {
            const string key = "carsById";
            var car = cache.GetData<Car>(key);
            if (car is null)
            {
                car = carService.GetById(id);
                cache.SetData(key, car);
            }

            return Ok(car);
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