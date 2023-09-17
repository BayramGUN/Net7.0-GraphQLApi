using GraphQLDemo.Api.Schema.Mutations;
using GraphQLDemo.Api.Schema.Queries;
using GraphQLDemo.Api.Schema.Subscriptions;
var MyAllowSpecificOrigins  = "_mySpecs";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
        {
        options.AddPolicy(MyAllowSpecificOrigins,
                              policy =>
                              {
                                  policy.AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                              });
        });
// added graphql server query type and other graphql 
builder.Services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddInMemorySubscriptions();


var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
app.MapGet("/", () => "Hello World!");
app.UseWebSockets();
app.MapGraphQL();
app.Run();
