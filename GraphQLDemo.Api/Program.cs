using GraphQLDemo.Api.DataLoaders;
using GraphQLDemo.Api.Schema.Mutations;
using GraphQLDemo.Api.Schema.Queries;
using GraphQLDemo.Api.Schema.Subscriptions;
using GraphQLDemo.Api.Services;
using GraphQLDemo.Api.Services.Courses;
using GraphQLDemo.Api.Services.Instructors;
using GraphQLDemo.Api.Services.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

builder.Services.AddPooledDbContextFactory<SchoolDbContext>(
        o => o.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("SQLServer"))
);

builder.Services.AddScoped<ICoursesRepository, CoursesRepository>();
builder.Services.AddScoped<IInstructorsRepository, InstructorsRepository>();
builder.Services.AddScoped<IStudentsRepository, StudentsRepository>();
builder.Services.AddScoped<InstructorDataLoader>();

var app = builder.Build();

// database context operations
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<IDbContextFactory<SchoolDbContext>>();
await context.CreateDbContext().Database.MigrateAsync();


app.UseCors(MyAllowSpecificOrigins);
app.MapGet("/", () => "Hello World!");
app.UseWebSockets();
app.MapGraphQL();
app.Run();
