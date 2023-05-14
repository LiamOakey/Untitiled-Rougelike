using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    float spawnsDelay = 3f; // delay in seconds
    public GameObject enemy;
    int spawnVariation = 3;
    // Start is called before the first frame update
    private void Start() {
        InvokeRepeating("spawn",0f,spawnsDelay);
    }

    void spawn(){
        Vector3 spawnPoint = GameObject.FindWithTag("EnemySpawn").GetComponent<Transform>().position;
        float y = Random.Range(-spawnVariation,spawnVariation);
        spawnPoint.y += y;
        Instantiate(enemy, spawnPoint, Quaternion.identity);
    }
}
