using GraphQL.Types;

namespace Autodealer.GraphQL.GraphQLTypes;

public sealed class EngineInputType : InputObjectGraphType
{
    public EngineInputType()
    {
        Name = "EngineInput";
        Field<NonNullGraphType<StringGraphType>>("brand");
        Field<NonNullGraphType<StringGraphType>>("model");
        Field<NonNullGraphType<StringGraphType>>("capacity");
        Field<NonNullGraphType<StringGraphType>>("countBlock");
    }
}