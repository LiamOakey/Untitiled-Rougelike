using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerHealth : MonoBehaviour
{
    float health = 100;
    float maxHealth = 100;
    bool invincible = false;

    public TextMeshProUGUI healthText;
    BoxCollider2D collider;

    private void Awake() {
        collider = GetComponent<BoxCollider2D>();
    }

    void takeDamage(float damage){

        Debug.Log(damage);

        invincible = true;
        Invoke("resetInvincible", 0.2f);
        health -= damage;

        //health text update
        healthText.text = health.ToString();

        //death check
        if(health < 1){
            die();
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if(!invincible){

            if(other.gameObject.tag == "Enemy"){
                float damage = other.gameObject.GetComponent<EnemyBehavoir>().damage;
                takeDamage(damage);
            }
        }
    }
    
    void resetInvincible(){
        invincible = false;
    }

    void die(){
        Debug.Log("YOU DIED");
    }
}
