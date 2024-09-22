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
        damage = GameObject.FindWithTag("Player").GetComponent<PlayerShooting>().gun.damage;
        pierce = GameObject.FindWithTag("Player").GetComponent<PlayerShooting>().gun.pierce;
        knockback = GameObject.FindWithTag("Player").GetComponent<PlayerShooting>().gun.knockback;
        Invoke("destroy", 2f);
    }

    void destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyBehavoir>().takeDamage(damage, knockback);
            if (pierce == 1)
            {
                destroy();
            }
            pierce--;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Dont destroy the bullet if it hits the player's hitbox
        if (!collision.gameObject.CompareTag("Player"))
        {
            destroy();
        }
        
    }
}
