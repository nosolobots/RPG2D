using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnOnceSO", menuName = "Scriptable Objects/SpawnOnceSO")]
public class SpawnOnceSO : ScriptableObject
{
    [Header("Spawn Once Item Properties")]
    public string itemID; // Unique identifier for the item
    public int sceneID; // ID of the scene where the item should be spawned
    public SpawnOnceType itemType; // Type of the item (Weapon, Health, Ammo, etc.)
    public SpawnOnceState state; // Current state of the item (NotSpawned, Spawned, Collected)
    public Vector3 spawnPosition; // Position where the item should be spawned
    public GameObject itemPrefab; // Prefab of the item to spawn
    [TextArea(3, 10)]
    public string message; // Message to display when the item is collected

    [Serializable]
    public enum SpawnOnceState
    {
        NotSpawned,
        ToSpawn,
        Collected
    }

    [Serializable]
    public enum SpawnOnceType
    {
        Weapon,
        Health,
        Ammo,
        Key,
        Other // Add more types as needed
    }

}

