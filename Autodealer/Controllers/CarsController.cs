using Autodealer.Dto;
using Autodealer.Entities;
using Autodealer.Services;
using Autodealer.Services.Caching;
using Microsoft.AspNetCore.Mvc;

namespace Autodealer.Controllers;

[Route("[controller]")]
[ApiController]
public class CarsController(ICarService carService, IRedisCacheService cache, ProducerService producer) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Car>?>> GetAll()
    {
        try
        {
            var cachingKey = "cars_";
            var cars = cache.GetData<IEnumerable<Car>>(cachingKey);
            if (cars is not null) 
                return Ok(cars);
            cars = await carService.GetAll();
            cache.SetData(cachingKey, cars);

            return Ok(cars);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        try
        {
            var key = $"carsById{id}";
            var car = cache.GetData<Car>(key);
            if (car is not null) 
                return Ok(car);
            car = carService.GetById(id);
            cache.SetData(key, car);

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
        cache.Delete<Car>("cars_");
        return CreatedAtAction(nameof(GetById), new { id = createdAuto.Id }, createdAuto);
    }

    [HttpPut]
    public async Task<IActionResult> Update(Car car)
    {
        try
        {
            await carService.Update(car);
            cache.Delete<Car>("cars_");
            cache.SetData($"carsById{car.Id}", car);
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
            cache.Delete<Car>("cars_");
            cache.Delete<Car>($"carsById{id}");
        }
        catch
        {
            return NotFound();
        }
        return Ok();
    }

    [HttpGet("/producer")]
    public async Task<IActionResult> Producer(CancellationToken token)
    {
        await producer.ProduceAsync(token);
        return Ok("Сообщение отправлено");
    }
}