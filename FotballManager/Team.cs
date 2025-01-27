public class Team
{
    public string name { get; set; } = "";
    public List<Player> players = new List<Player>();

    public Team() {}


//Add new player
public string AddPlayer (Player player)
{
    var existingPlayer = players.FirstOrDefault(p => p.Id == player.Id);

    if (existingPlayer != null)
    {
        return $"Player with ID {player.Id} already exists!";
    }

    players.Add(player);
    return $"Player with name {player.name} (ID: {player.Id}) has been added";
}

//Remove a player
public string RemovePlayer (int playerId)
{
    var existingPlayer = players.FirstOrDefault(p => p.Id == playerId);

    if (existingPlayer == null)
    {
        return $"Player with ID {playerId} not found.";
    }
    players.Remove(existingPlayer);
    return $"Player with ID {playerId} removed.";
}

//Find player by ID
public string FindPlayersByName (string name)
{
    var matchedPlayers = players.Where(p => p.name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();

    if (matchedPlayers.Count == 0)
    {
        return $"No players can be found with name: {name}";
    }

    return $"Found {matchedPlayers.Count} player(s) with the name {name}: " +
    string.Join(", ", matchedPlayers.Select(p => $"ID: {p.Id}, Name: {p.name}"));
}

//Update player details
public string UpdatePlayer (int playerId, string newName)
{
    var player = players.FirstOrDefault(p => p.Id == playerId);

    if (player == null)
    {
        return $"Player with ID {playerId} not found.";
    }

    player.name = newName;
    return $"Player with ID {playerId} updated. New name: {newName}";
}

//

}