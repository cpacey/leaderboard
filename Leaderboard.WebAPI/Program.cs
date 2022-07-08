var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<Leaderboard.ILeaderboardManager, Leaderboard.LeaderboardManager>();
builder.Services.AddScoped<Leaderboard.IConfiguration, Leaderboard.Configuration>();
builder.Services.AddScoped<Amazon.DynamoDBv2.IAmazonDynamoDB>( serviceProvider => {
	Leaderboard.IConfiguration config = serviceProvider.GetRequiredService<Leaderboard.IConfiguration>();
	Amazon.DynamoDBv2.AmazonDynamoDBConfig clientConfig = new() {
		ServiceURL = config.DynamoEndpoint
	};
	return new Amazon.DynamoDBv2.AmazonDynamoDBClient( clientConfig );
} );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
