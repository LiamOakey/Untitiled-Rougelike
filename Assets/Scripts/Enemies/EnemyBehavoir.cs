using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBehavoir : MonoBehaviour
{
    [SerializeField] private GameObject floatingDamageNumber;
    public Enemy enemy;
    public float health;
    public float damage;

    private void Awake() {
        gameObject.tag = "Enemy";
        health = enemy.health;
        damage = enemy.damage;
    }

    public void takeDamage(float incomingDamage){
        health-=incomingDamage;
        ShowDamage(incomingDamage.ToString());
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
}
