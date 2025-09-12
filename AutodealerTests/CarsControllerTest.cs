using Autodealer.Controllers;
using Autodealer.Dto;
using Autodealer.Entities;
using Autodealer.Services;
using Autodealer.Services.Caching;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AutodealerTests;

public class CarsControllerTest
{
    private readonly Mock<ICarService> _carServiceMock;
    private readonly CarsController _controller;
    
    public CarsControllerTest()
    {
        _carServiceMock = new Mock<ICarService>();
        var cacheServiceMock = new Mock<IRedisCacheService>();
        var producerServiceMock = new Mock<ProducerService>();
        _controller = new CarsController(_carServiceMock.Object, cacheServiceMock.Object, producerServiceMock.Object);
    }

    [Fact]
    public async Task Create100Cars()
    {
        var cars = new List<CarDto>();
        const int countCars = 100;

        for (var i = 0; i < countCars; i++)
        {
            cars.Add(new CarDto {Brand = $"Lada{i}", Model = "Granta", Generation = i.ToString(), Engine = "1.4"});
        }

        _carServiceMock.Setup(service => service.Create(It.IsAny<CarDto>()))
            .ReturnsAsync((CarDto car) =>
                new Car { Id = "someId", Brand = car.Brand, Model = car.Model,
                    Generation = car.Generation, Engine = car.Engine });

        foreach (var car in cars)
        {
            var result = await _controller.Create(car);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        _carServiceMock.Verify(service => service.Create(It.IsAny<CarDto>()), Times.Exactly(countCars));
    }

    [Fact]
    public async Task Create100000Cars()
    {
        var cars = new List<CarDto>();
        const int countCars = 100000;

        for (var i = 0; i < countCars; i++)
        {
            cars.Add(new CarDto {Brand = $"Lada{i}", Model = "Granta", Generation = i.ToString(), Engine = "1.4"});
        }

        _carServiceMock.Setup(service => service.Create(It.IsAny<CarDto>()))
            .ReturnsAsync((CarDto car) =>
                new Car { Id = "someId", Brand = car.Brand, Model = car.Model,
                    Generation = car.Generation, Engine = car.Engine });

        foreach (var car in cars)
        {
            var result = await _controller.Create(car);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        _carServiceMock.Verify(service => service.Create(It.IsAny<CarDto>()), Times.Exactly(countCars));
    }

    [Fact]
    public async Task DeleteAllCars()
    {
        var cars = (await _controller.GetAll()).Value as List<Car>;

        foreach (var car in cars!)
        {
            var result = await _controller.Delete(car.Id);
            Assert.IsType<OkResult>(result);
        }

        _carServiceMock.Verify(service => service.Delete(It.IsAny<string>()), Times.Exactly(cars.Count));
    }
}