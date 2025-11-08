using Autodealer.Dto;
using Autodealer.Entities;
using Autodealer.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Autodealer.Controllers;

[Route("[controller]")]
[ApiController]
public class EnginesController(IEngineRepository engineRepository) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(EngineDto engineDto)
    {
        var engine = await engineRepository.Create(engineDto);
        return Ok(engine);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var engines = await engineRepository.GetAll();
        return Ok(engines);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var engine = engineRepository.GetById(id);
        return Ok(engine);
    }

    [HttpPut]
    public async Task<IActionResult> Update(Engine engine)
    {
        await engineRepository.Update(engine);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await engineRepository.Delete(id);
        return Ok();
    }
}