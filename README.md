# training
Training courses

White Lotus

Radzen UI
Awesome Blazor - Github repository

Angular Tour of heroes
https://angular.io/tutorial

GraphQL
https://graphql.org/

gRPC
https://grpc.io/


#Entity Framework Setup

Add EntityFrameworkCore, EntityFrameworkCore.SqlServer and EntityFrameworkCore.Design to OdeToFood.Data project.
Add EntityFrameworkCore.Design to OdeToFood project (base ef classes aren't installed by default in .net 5

In dev command line, install ef tools as not installed by default in .net 5

`dotnet tool install --global dotnet-ef`

Create OdeToFoodDbContext in data project

`public class OdeToFoodDbContext : DbContext
{
	public OdeToFoodDbContext(DbContextOptions<OdeToFoodDbContext> options)
		: base(options)
	{

	}

	public DbSet<Restaurant> Restaurants { get; set; }

}`

Switch to data project to find context
dotnet ef dbcontext list

Should now find context

dotnet ef dbcontext info will still error because all startup code is in OdeToFood project, not data project.

Add connection string to appsettings.json
  `"ConnectionStrings": {
    "OdeToFoodDb": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OdeToFood;Integrated Security=True;"
  }`

Add services.AddDbContextPool to Startup.cs
`services.AddDbContextPool<OdeToFoodDbContext>(options =>
{
	options.UseSqlServer(Configuration.GetConnectionString("OdeToFoodDb"));
});`

-s means startup project. This will find the startup services, etc
`dotnet ef dbcontext info -s ..\OdeToFood\OdeToFood.csproj`

Mine still errored because I needed to install entityframeworkcore.design into startup project
Had to rebuild project before command line detected package was installed

Create an initial migration
`dotnet ef migrations add initialcreate -s ..\OdeToFood\OdeToFood.csproj`

Use the initial migration to create the database
`dotnet ef database update -s ..\OdeToFood\OdeToFood.csproj`

