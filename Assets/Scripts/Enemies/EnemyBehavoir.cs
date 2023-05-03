using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavoir : MonoBehaviour
{
    public Enemy enemy;
    public float health;
    public float damage;

    private void Awake() {
        gameObject.tag = "Enemy";
        health = enemy.health;
        damage = enemy.damage;
    }

    public void takeDamage(float damage){
        health-=damage;
        if(health <=0 ){
            die();
        }
    }

    void die(){
        Destroy(gameObject);
    }
}
