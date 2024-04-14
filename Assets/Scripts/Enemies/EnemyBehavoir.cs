using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBehavoir : MonoBehaviour
{
    new public ParticleSystem particleSystem;
    [SerializeField] private GameObject floatingDamageNumber;
    public Enemy enemyType;
    public float health;
    public float damage;
    public float knockbackResistance; //number between 0-1, that indicates the percentage of knockback taken
    // knockbackResistance of 0.2 means the enemy only takes 20% knockback
    Rigidbody2D rb;

    // Assign values from enemy type
    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gameObject.tag = "Enemy";
        health = enemyType.health;
        damage = enemyType.damage;
        knockbackResistance = enemyType.knockbackResistance;
    }

    public void takeDamage(float incomingDamage, float knockback){
        particleSystem.Play();
        health-=incomingDamage;
        ShowDamage(incomingDamage.ToString());
         takeKnockback(knockback);
         particleSystem.Play();
        if(health <=0 ){
            die();
        }
    }

    void die(){
        Destroy(gameObject);
    }

    void ShowDamage(string damageText){
        if(floatingDamageNumber){
            Vector3 variation = new Vector3(Random.Range(-0.5f,0.5f),-0.3f,0);
            GameObject prefab = Instantiate(floatingDamageNumber, transform.position+variation, Quaternion.identity);
            prefab.GetComponentInChildren<TextMeshPro>().text = damageText;
        }
    }


    void takeKnockback(float knockback){
        GameObject player = GameObject.FindWithTag("Player");
        knockback = knockback * knockbackResistance;


        if(player){
            Vector2 direction = (transform.position - player.transform.position).normalized;
            rb.AddForce(direction * knockback, ForceMode2D.Impulse);
            Invoke("ResetVelocity", 0.2f);
        }
    }


    void ResetVelocity(){
        rb.velocity = new Vector2(0,0);
    }
}
