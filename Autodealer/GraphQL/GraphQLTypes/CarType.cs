using Autodealer.Entities;
using Autodealer.Repositories.Interfaces;
using GraphQL.Types;

namespace Autodealer.GraphQL.GraphQLTypes;

public sealed class CarType : ObjectGraphType<Car>
{
    public CarType(IEngineRepository engineRepository)
    {
        Name = "Car";
        Description = "A car entity";
        
        Field(x => x.Id, type: typeof(IdGraphType))
            .Description("The unique identifier of the car");
        
        Field(x => x.Brand)
            .Description("The brand of the car");
        
        Field(x => x.Model)
            .Description("The model of the car");
        
        Field(x => x.Generation, nullable: true)
            .Description("The generation of the car");
        
        /*Field(x => x.EngineId, nullable: true)
            .Description("The engine of the car");*/
        
        Field<EngineType>("engine")
            .Resolve(context => engineRepository.GetById(context.Source.EngineId));
    }
}