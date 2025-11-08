using GraphQL.Types;

namespace Autodealer.GraphQL.GraphQLTypes;

public sealed class CarInputType : InputObjectGraphType
{
    public CarInputType()
    {
        Name = "CarInput";
        Field<NonNullGraphType<StringGraphType>>("brand");
        Field<NonNullGraphType<StringGraphType>>("model");
        Field<NonNullGraphType<StringGraphType>>("generation");
        Field<EngineInputType>("engine");
    }
}