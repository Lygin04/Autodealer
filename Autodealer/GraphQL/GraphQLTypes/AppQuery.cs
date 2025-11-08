using Autodealer.Entities;
using Autodealer.Repositories.Interfaces;
using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Types;

namespace Autodealer.GraphQL.GraphQLTypes;

public sealed class AppQuery : ObjectGraphType
{
    public AppQuery(ICarRepository carRepository)
    {
        Name = "Query";

        AddField(new FieldType
        {
            Name = "cars",
            Arguments = new QueryArguments(
                new QueryArgument<IdGraphType>{Name = "id"},
                new QueryArgument<StringGraphType>{Name = "brand"},
                new QueryArgument<StringGraphType>{Name = "model"}),
            Type = typeof(ListGraphType<CarType>),
            Resolver = new FuncFieldResolver<IEnumerable<Car>>(context =>
            {
                var cars = carRepository.GetAll().GetAwaiter().GetResult();

                var id = context.GetArgument<string?>("id");
                var brand = context.GetArgument<string>("brand");
                var model = context.GetArgument<string?>("model");
                
                if(!string.IsNullOrEmpty(id))
                    cars = cars.Where(c => c.Id ==  id);
                
                if(!string.IsNullOrEmpty(brand))
                    cars = cars.Where(c => c.Brand == brand);
                
                if(!string.IsNullOrEmpty(model))
                    cars = cars.Where(c => c.Model == model);

                return cars;
            })
        });
    }
}