using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] toSpawn;
    public float spawnMax = 1, spawnRate= 4, startDelay = 0f, difficultyRate = 15f;
    private int size, difficulty = 0;

    private bool isSpawning;
	// Use this for initialization
	void Start () {
		//InvokeRepeating("Spawn", 0, 2);
	    size = toSpawn.Length;
        InvokeRepeating("IncreaseDifficulty", difficultyRate, difficultyRate);
        Invoke("StartSpawn", startDelay);
	}

    void StartSpawn()
    {
        isSpawning = true;
    }
	// Update is called once per frame
	void Update ()
	{
	    if (isSpawning)
	    {
	        StartCoroutine(spawnEnumerator());
	        isSpawning = false;
	    }
    }

    private IEnumerator spawnEnumerator()
    {
        while (true)
        {
            int spawnIdx = Mathf.FloorToInt(Random.Range(0, difficulty));
            Instantiate(toSpawn[spawnIdx], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
    }
    private void IncreaseDifficulty()
    {
        if (spawnRate > spawnMax)
        {
            spawnRate--;
        }
        if (difficulty == size) return;

        difficulty++;
    }
   // private void Spawn()
   // {
   //     Instantiate(toSpawnGameObject, transform.position, Quaternion.identity);    
   // }
}
