using UnityEngine;

public static class SpawnManager
{
    public static string nextSpawnID = "Default";
    
    public static void SetNextSpawn(string spawnID)
    {
        nextSpawnID = spawnID;
    }
}
