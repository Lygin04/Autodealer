using Autodealer.Dto;
using Autodealer.Model;

namespace Autodealer.Services;

public class AutoService : IAutoService
{
    public List<Auto?> GetAll()
    {
        return DataContext.Autos;
    }

    public Auto GetById(int id)
    {
        return DataContext.Autos.Find(c => c != null && c.Id == id) ?? throw new InvalidOperationException();
    }

    public Auto Create(AutoDto auto)
    {
        Auto newAuto = new Auto();
        newAuto.Id = ++DataContext.Id;
        newAuto.Brand = auto.Brand;
        newAuto.Model = auto.Model;
        newAuto.Generation = auto.Generation;
        newAuto.Engine = auto.Engine;
        DataContext.Autos.Add(newAuto);
        return newAuto;
    }

    public void Update(Auto newAuto)
    {
        Auto auto = DataContext.Autos.Find(c => c != null && c.Id == newAuto.Id)
                    ?? throw new InvalidOperationException();
        auto.Brand = newAuto.Brand;
        auto.Model = newAuto.Model;
        auto.Generation = newAuto.Generation;
        auto.Engine = newAuto.Engine;
    }

    public void Delete(int id)
    {
        Auto auto = DataContext.Autos.Find(c => c != null && c.Id == id)
                    ?? throw new InvalidOperationException();
        DataContext.Autos.Remove(auto);
    }
}