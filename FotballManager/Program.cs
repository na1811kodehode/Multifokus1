using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//Add services for swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


//Enable Swagger only in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


Team? team = null;

app.MapGet("/", () => "Hello World!")
.WithName("hello")
.WithOpenApi();

//Create team if it does not exist
app.MapPost("/createteam", ([FromBody] Team newTeam) =>
{
    //Check if team is created or not
    if (team != null)
    {
        return Results.Ok(new { Message = $"Team with name {team.name} is already created!"});
    }

    //Create a new team
    team = newTeam;

    return Results.Ok(new { Message = $"Team '{newTeam.name} created"});
});


app.MapGet("/teams", () =>
{
    if (team == null)
    {
        return Results.BadRequest(new { Message = "You have not created a team yet!"});
    }
    return Results.Ok(team.players);
});

//Add player
app.MapPost("/addplayer", ([FromBody] Player player) =>
{
    //Check if team exists
    if (team == null)
    {
        return Results.BadRequest(new { Message = "You must create a team first"});
    }

    var existingPlayer = team.players.FirstOrDefault (p => p.Id == player.Id);
    if (existingPlayer != null)
    {
        return Results.BadRequest(new { Message = $"Player with ID: {player.Id} already exists"});
    }

    //Check name is empty or not
    if (string.IsNullOrWhiteSpace(player.name))
    {
        return Results.BadRequest(new { Message = $"Player with name is required"});
    }

    //Let us check the latest ID number and add 1
    int newId = team.players.Any() ? team.players.Max (p => p.Id) + 1 : 1;

    //Create a player or update
    player.Id = newId;

    //Add to class
    team.AddPlayer(player);
    return Results.Ok(new { Message = $"Player with ID {player.Id} added"});
});


//Update by ID
app.MapPut("/updateplayer/{id:int}", ([FromRoute] int id, [FromBody] Player updatedPlayer) =>
{
    if (team == null)
    {
        return Results.BadRequest(new { Message = "You must create a team first"});
    }
    var existingPlayer = team.players.FirstOrDefault(p => p.Id == id);
    if (existingPlayer == null)
    {
        return Results.NotFound(new { Message = $"Player with ID {id} not found"});
    }

    //Check against empty entry
    if (!string.IsNullOrWhiteSpace(updatedPlayer.name))
    {
        existingPlayer.name = updatedPlayer.name;
    }

    //SJEKK LINJE 103 I CHEATSHEET

    return Results.Ok(new { Message = $"Player with ID {id} updated"});
});


//Find player by ID
app.MapGet("findplayer/{id:int}", ([FromRoute] int id) =>
{
    if (team == null)
    {
        return Results.BadRequest(new { Message = "You must create a team first"});
    }

    var player = team.players.FirstOrDefault(p => p.Id == id);
    if (player == null)
    {
        return Results.NotFound(new { Message = $"Player with ID {id} not found"});
    }

    return Results.Ok(player);
});


//Delete a player
app.MapDelete("/deleteplayer", ([FromBody] int id) =>
{
    //Ensure team exists
    if (team == null)
    {
        return Results.BadRequest(new { Message = "You must create a team first"});
    }

    //Find player by ID
    var existingPlayer = team.players.FirstOrDefault(p => p.Id == id);

    if (existingPlayer == null)
    {
        return Results.NotFound(new { Message = $"Player with ID: {id} not found"});
    }

    //Remove the matched player
    team.players.Remove(existingPlayer);

    return Results.Ok(new { Message = $"Player with ID: {id} deleted"});
});


//Find all players by certain rank
app.MapGet("/findplayerbyrank/{rank:int}", ([FromRoute] int rank) =>
{
    //Ensure that team exists
    if (team == null)
    {
        return Results.BadRequest(new { Message = "You must create a team first"});
    }

    var playerByRank = team.players.Where(p => p.ranking == rank).ToList();

    if (playerByRank == null)
    {
        return Results.NotFound(new { Message = $"There are currently no players with '{rank}' ranking."});
    }

    return Results.Ok(playerByRank);
});


//General team statistics
app.MapGet("/statistics", () =>
{
    //Ensure that team exists
    if (team == null)
    {
        return Results.BadRequest(new { Message = "You must create a team first"});
    }

    //# of players on the team
    var countPlayers = team.players.Count();

    //Find the lowest age
    var lowestAge = team.players.Min(p => p.age);
    //Find youngest player
    var youngestPlayer = team.players.Where(p => p.age == lowestAge).ToList();

    //Find the highest age
    var highestAge = team.players.Max(p => p.age);
    //Find the oldest player
    var oldestPlayer = team.players.Where(p => p.age == highestAge).ToList();

    //** Extra statistics just for fun ** 

    //Average age of players in the team
    var avgAge = team.players.Average(p => p.age);
    //List all players above average age
    var aboveAvgAgePlayers = team.players.Where(p => p.age > avgAge).ToList();
    //List all players below the average age
    var belowAvgAgePlayers = team.players.Where(p => p.age < avgAge).ToList();

    //Count the # of players above/below average age.
    //# of players below average age
    var countBelowAvgAge = belowAvgAgePlayers.Count();
    //# of players above average age
    var countAboveAvgAge = aboveAvgAgePlayers.Count();


    //Average ranking of players in the team
    var avgRank = team.players.Average(p => p.ranking);
    //List all players above average in ranking
    var  aboveAvgRankPlayers = team.players.Where(p => p.ranking > avgRank).ToList();
    //List all players below average age in ranking
    var belowAvgRankPlayers = team.players.Where(p => p.ranking < avgRank).ToList();

    //Count the # of players above/below average rank
    //# of players above average rank
    var countAboveAvgRank = aboveAvgRankPlayers.Count();
    //# of players below average rank
    var countBelowAvgRank = belowAvgRankPlayers.Count();

    return Results.Ok(new {
        TotalPlayers = countPlayers,
        LowestAge = lowestAge,
        YoungestPlayers = youngestPlayer,
        HighestAge = highestAge,
        OldestPlayers = oldestPlayer,
        AverageAge = avgAge,
        AboveAverageAgePlayers = aboveAvgAgePlayers,
        BelowAverageAgePlayers = belowAvgAgePlayers,
        CountBelowAverageAge = countBelowAvgAge,
        CountAboveAverageAge = countAboveAvgAge,
        AverageRanking = avgRank,
        AboveAverageRankingPlayers = aboveAvgRankPlayers,
        BelowAverageRankingPlayers = belowAvgRankPlayers,
        CountAboveAverageRank = countAboveAvgRank,
        CountBelowAverageRank = countBelowAvgRank
    });
});


app.Run();
