using Autodealer;
using Autodealer.Controllers;
using Autodealer.Dto;
using Autodealer.Model;
using Autodealer.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AutodealerTests;

public class CarControllerTest
{
    private readonly Mock<ICarService> _carServiceMock;
    private readonly CarController _controller;

    public CarControllerTest()
    {
        _carServiceMock = new Mock<ICarService>();
        _controller = new CarController(_carServiceMock.Object);
    }

    [Fact]
    public async Task Create100Cars()
    {
        var cars = new List<CarDto>();

        for (int i = 0; i < 100; i++)
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

        _carServiceMock.Verify(service => service.Create(It.IsAny<CarDto>()), Times.Exactly(100));
    }

    [Fact]
    public async Task Create100000Cars()
    {
        var cars = new List<CarDto>();

        for (int i = 0; i < 100000; i++)
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

        _carServiceMock.Verify(service => service.Create(It.IsAny<CarDto>()), Times.Exactly(100000));
    }

    [Fact]
    public async Task DeleteAllCars()
    {
        var cars = (await _controller.GetAll()).ToList();

        foreach (var car in cars)
        {
            var result = await _controller.Delete(car.Id);
            Assert.IsType<OkResult>(result);
        }

        _carServiceMock.Verify(service => service.Delete(It.IsAny<string>()), Times.Exactly(cars.Count));
    }
}