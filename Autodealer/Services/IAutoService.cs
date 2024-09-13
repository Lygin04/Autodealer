using Autodealer.Dto;
using Autodealer.Model;

namespace Autodealer.Services;

public interface IAutoService
{
    List<Auto?> GetAll();
    Auto GetById(int id);
    Auto Create(AutoDto auto);
    void Update(Auto newAuto);
    void Delete(int id);
}