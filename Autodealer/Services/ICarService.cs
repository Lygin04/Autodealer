using Autodealer.Dto;
using Autodealer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Autodealer.Services;

public interface ICarService
{
    Task<IEnumerable<Car>?> GetAll();
    Car GetById(string id);
    Task<Car> Create(CarDto car);
    Task Update(Car newCar);
    Task Delete(string id);
}