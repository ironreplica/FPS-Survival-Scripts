using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData
{
    public Vector3 playerPos;
    public int[] inventory;
    public int[] hotBar;
    public float playerHealth;
    public Dictionary<string, bool> worldHealthObjs;
    public Dictionary<string, SafeData> containers;
    public Dictionary<string, FoundationData> foundations; // This includes foundations and attached structures. Mapping the entire build.
    public Dictionary<string, SaplingData> saplings;
    public GameData()
    {
        // This will be the starting values of the New Game, utilizing the class constructor.
        playerPos = Vector3.zero;
        playerHealth = 100;
        worldHealthObjs = new Dictionary<string, bool>();
        containers = new Dictionary<string, SafeData>();
        inventory = new int[18];
        hotBar = new int[3];
        foundations = new Dictionary<string, FoundationData>();
        saplings = new Dictionary<string, SaplingData>();
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = -1;
        }
        for (int i = 0; i < hotBar.Length; i++)
        {
            hotBar[i] = -1;
        }
    }
    public class SafeData
    {
        public Vector3 Position { get; set; }
        public Dictionary<int, int> Slots { get; set; } // <SlotNumber, ItemID>

        public SafeData(Vector3 position)
        {
            Position = position;
            Slots = new Dictionary<int, int>();
        }
    }
    public class FurnaceData
    {
        public Vector3 Position { get; set; }
        public Dictionary<int, int> Slots { get; set; } // <SlotNumber, ItemID>
        // Need a fuel slot, input slot, and product/biproduct slots 
        public FurnaceData(Vector3 position)
        {
            Position = position;
            Slots = new Dictionary<int, int>();
        }
    }
    public class FoundationData
    {
        public Vector3 Position { get; set; }
        public int BuildID { get; set; } // Item id
        public Dictionary<int, int> WallPositions { get; set; } // Child number, and which child has what id to represent if its built.

        public FoundationData(Vector3 position)
        {
            Position = position;
            WallPositions = new Dictionary<int, int>();
        }
    }
    public class SaplingData
    {
        public Vector3 Position;
        public float CurrentTime;
        public SaplingData(Vector3 position, float currentTime)
        {
            Position = position;
            CurrentTime = currentTime;
        }
    }
}
