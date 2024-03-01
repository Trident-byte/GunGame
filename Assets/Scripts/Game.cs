using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] Transform playerPosition;
    [SerializeField] float spawnTimer;
    [SerializeField] GameObject enemy;
    private float timeSinceLastSpawn;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn > spawnTimer)
        {
            GameObject newEnemy = Instantiate(enemy as GameObject);
            Enemy enemyScript = newEnemy.GetComponent<Enemy>();
            enemyScript.playerPos = playerPosition;
            enemyScript.speed = 10;
            newEnemy.transform.position = spawnPoints[0].GetComponent<Transform>().position;

            timeSinceLastSpawn = 0;
        }
    }
}
