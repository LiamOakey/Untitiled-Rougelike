using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float damage;
    float pierce;
    float knockback;
    // Start is called before the first frame update
    void Start()
    {
        damage = GameObject.FindWithTag("Player").GetComponent<PlayerShooting>().damage;
        pierce = GameObject.FindWithTag("Player").GetComponent<PlayerShooting>().pierce;
        knockback = GameObject.FindWithTag("Player").GetComponent<PlayerShooting>().knockback;
        Invoke("destroy", 2f);
    }

    void destroy(){
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D other) {

        if(other.gameObject.tag == "Enemy"){
            other.GetComponent<EnemyBehavoir>().takeDamage(damage, knockback);
            if(pierce == 1){
                destroy();
            }
            pierce--;
        }
    }

    
}
