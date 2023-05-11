using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    bool canMove = true;
    public float speed = 1f;
    float maxSpeed; // set equal to speed



    private void Awake() {
        maxSpeed = speed;
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update() {
        if(canMove){
            
           

            //speed lowers over time, before reaching zero and reseting
            if(speed > 0.01){
                speed-= 0.005f;

                //Sets animation
                animator.SetFloat("Speed", 1f);
            } else{
                Invoke("resetSpeed", 0.3f);
                animator.SetFloat("Speed", -1f);
            }

            Transform enemyPosition = gameObject.GetComponent<Transform>();
            Transform playerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>();

            //Check if playerPosition exists
            if(playerPosition){
                Vector2 direction = (playerPosition.position - transform.position).normalized;
                enemyPosition.Translate(direction* speed *  Time.deltaTime);
            }

            
        }
    }

    void resetSpeed(){
        speed = maxSpeed;
    }

}
