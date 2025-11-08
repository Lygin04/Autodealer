using Autodealer.GraphQL.GraphQLTypes;
using GraphQL.Types;

namespace Autodealer.GraphQL.GraphQLSchema;

public class AppSchema : Schema
{
    public AppSchema(IServiceProvider provider) : base(provider)
    {
        Query = provider.GetRequiredService<AppQuery>();
        Mutation = provider.GetRequiredService<AppMutation>();
    }
}