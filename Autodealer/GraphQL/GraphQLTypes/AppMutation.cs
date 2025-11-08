using Autodealer.Dto;
using Autodealer.Entities;
using Autodealer.Repositories.Interfaces;
using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Types;

namespace Autodealer.GraphQL.GraphQLTypes;

public sealed class AppMutation : ObjectGraphType
{
    public AppMutation(ICarRepository carRepository, IEngineRepository engineRepository)
    {
        Name = "Mutation";

        AddField(new FieldType
        {
            Name = "createCar",
            Type = typeof(CarType),
            Arguments = new QueryArguments(
                new QueryArgument<NonNullGraphType<CarInputType>> { Name = "car"}),
            Resolver = new FuncFieldResolver<Car>(context =>
            {
                var carInput = context.GetArgument<CarMutationDto>("car");
                var engine = new EngineDto
                {
                    Brand = carInput.Engine.Brand,
                    Model = carInput.Engine.Model,
                    Capacity = carInput.Engine.Capacity,
                    CountBlock = carInput.Engine.CountBlock,
                };

                var engineId = engineRepository.Create(engine).Result.Id;
                
                var carDto = new CarDto
                {
                    Brand = carInput.Brand,
                    Model = carInput.Model,
                    Generation = carInput.Generation,
                    EngineId = engineId
                };

                var car = carRepository.Create(carDto);
                return car.Result;
            })
        });

        AddField(new FieldType
        {
            Name = "updateCar",
            Type = typeof(CarType),
            Arguments = new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" },
                new QueryArgument<StringGraphType> { Name = "brand" },
                new QueryArgument<StringGraphType> { Name = "model" },
                new QueryArgument<StringGraphType> { Name = "generation" },
                new QueryArgument<StringGraphType> { Name = "engineId" }),
            Resolver = new FuncFieldResolver<Car>(context =>
            {
                var id = context.GetArgument<string>("id");

                var car = carRepository.GetById(id);
                if (car == null)
                {
                    context.Errors.Add(new ExecutionError("Car not found"));
                }

                var engineId = context.GetArgument<string>("engineId");

                if (!string.IsNullOrEmpty(engineId))
                {
                    var engine = engineRepository.GetById(engineId);
                    if (engine != null)
                        car.EngineId = engineId;
                }
                
                var brand = context.GetArgument<string>("brand");
                var model = context.GetArgument<string>("model");
                var generation = context.GetArgument<string>("generation");
                
                if(!string.IsNullOrEmpty(brand))
                    car.Brand = brand;
                
                if(!string.IsNullOrEmpty(model))
                    car.Model = model;
                
                if(!string.IsNullOrEmpty(generation))
                    car.Generation = generation;
                
                if(!string.IsNullOrEmpty(engineId))
                    car.EngineId = engineId;

                carRepository.Update(car);

                return car;
            })
        });

        AddField(new FieldType
        {
            Name = "deleteCar",
            Type = typeof(StringGraphType),
            Arguments = new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" }),
            Resolver = new FuncFieldResolver<string>(context =>
            {
                var id = context.GetArgument<string>("id");
                carRepository.Delete(id);
                return $"Car {id} deleted";
            })
        });
    }
}