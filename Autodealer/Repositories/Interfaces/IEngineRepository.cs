using Autodealer.Dto;
using Autodealer.Entities;

namespace Autodealer.Repositories.Interfaces;

public interface IEngineRepository
{
    Task<IEnumerable<Engine>?> GetAll();
    Engine GetById(string id);
    Task<Engine> Create(EngineDto engineDto);
    Task Update(Engine newEngine);
    Task Delete(string id);
}