public static class EventIds
{
    public const string ScoreChanged = "game.score.changed";
    public const string ScoreUpdated = "game.score.updated";

    public const string ItemCollected = "game.item.collected";     
    public const string InventoryUpdated = "game.inventory.updated";

    public const string DoorOpen = "world.door.open";

    public const string HealRequested = "game.player.heal";   
    public const string DamageRequested = "game.player.damage"; 
    public const string HealthUpdated = "game.player.health.updated";

    public const string SpeedBuffRequested = "game.player.speedbuff";

    public const string PlaySfx = "audio.play";
    public const string GameCompleted = "game.completed";
}
