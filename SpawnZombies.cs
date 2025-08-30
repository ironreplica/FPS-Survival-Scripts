using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombies : MonoBehaviour
{
    public GameObject zombiePrefab;
    public GameObject player;
    public int TotalZombies;
    private bool isSpawning;
    void Start()
    {
        isSpawning = true;
        StartCoroutine(spawnTick());

    }

    private IEnumerator spawnTick()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(Random.Range(45, 300));
            spawnZombies(Random.Range(3, 7));
        }
    }
    private void spawnZombies(int n)
    {
        // Get a random position in a large vicinty of the player, not inside a building, and instantiate a zombie ever x amount of seconds
        // Increased spawn rate at night
        // max amount of zombies allowed in the game

        // Add functionality to make the zombies roam around the map a little
        // randomized positions for the zombies to walk to on the navmesh
        for(var i = 0; i <= n; i++)
        {
            Vector3 newZombiePosition = new Vector3(player.transform.position.x + 10 + Random.Range(0,100), player.transform.position.z, player.transform.position.z + 10 + +Random.Range(0, 100));
            TotalZombies++;
            GameObject newZombie = Instantiate(zombiePrefab);
            newZombie.transform.parent = null;
            newZombie.transform.position = newZombiePosition;
            /*newZombie.transform.position = new Vector3(newZombie.transform.position.y + Random.Range(0, 25), newZombie.transform.position.z + Random.Range(0, 25));*/
        }
    }
}
