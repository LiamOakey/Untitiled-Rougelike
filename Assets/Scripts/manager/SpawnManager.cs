using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    float spawnsDelay = 3f; // delay in seconds
    public GameObject enemy;
    // Start is called before the first frame update
    private void Start() {
        InvokeRepeating("spawn",0f,spawnsDelay);
    }

    void spawn(){
        float y = Random.Range(-4f,4f);
        Instantiate(enemy, new Vector3(10, y, 0), Quaternion.identity);
    }
}
