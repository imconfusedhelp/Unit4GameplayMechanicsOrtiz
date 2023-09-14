using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float spawnInterval;
    public bool isBoss = false;
    public int miniEnemySpawnCount;

    private Rigidbody enemyRb;
    private GameObject player;
    private SpawnManager spawnManager;
    private float nextSpawn;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        if (isBoss)
        {
            spawnManager = FindObjectOfType<SpawnManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        enemyRb.AddForce(lookDirection * speed);

        if (isBoss)
        {
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnInterval;
                spawnManager.SpawnMiniEnemy(miniEnemySpawnCount);
            }
        }

        if (transform.position.y < -2)
        {
            Destroy(gameObject);
        }
    }
}
