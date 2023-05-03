using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float damage;
    float pierce;
    // Start is called before the first frame update
    void Start()
    {
        damage = GameObject.FindWithTag("Player").GetComponent<PlayerShooting>().damage;
        pierce = GameObject.FindWithTag("Player").GetComponent<PlayerShooting>().pierce;
        Invoke("destroy", 2f);
    }

    void destroy(){
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy"){
            other.GetComponent<EnemyBehavoir>().takeDamage(10);
            if(pierce == 1){
                destroy();
            }
        }
    }

    
}
