using Autodealer.Entities;
using GraphQL.Types;

namespace Autodealer.GraphQL.GraphQLTypes;

public sealed class EngineType : ObjectGraphType<Engine>
{
    public EngineType()
    {
        Name = "Engine";
        Description = "A engine entity";

        Field(x => x.Id, type: typeof(IdGraphType))
            .Description("The unique identifier of the engine");
        
        Field(x => x.Brand)
            .Description("The brand of the engine");
        
        Field(x => x.Model)
            .Description("The model of the engine");
        
        Field(x => x.Capacity)
            .Description("The capacity of the engine");
        
        Field(x => x.CountBlock)
            .Description("The count block of the engine");
    }
}