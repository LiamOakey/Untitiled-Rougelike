using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float dashSpeed = 2.75f;
    bool dashing = false;
    Rigidbody2D rb;

    private void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update(){

        if(Input.GetButtonDown("Jump")){ //On pressing space

            if(rb.velocity.x != 0 || rb.velocity.y != 0){ //Must be moving to use dash
                Debug.Log("bruh");
                Dash();
            }
            
        }
    }
   
    void FixedUpdate()
    {
        if(!dashing){
            movementHandler();
        } 
        
    }

    //Check keys and updates player velocity
    void movementHandler(){
        float movementX = Input.GetAxisRaw("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(movementX, movementY);
        movement = movement.normalized * speed;
        rb.velocity = movement;
    }


    void Dash(){
        dashing = true;
        speed = speed*dashSpeed;
        movementHandler();
        Invoke("ResetDash",0.3f);
    }

    void ResetDash(){
        dashing = false;
        speed = speed/dashSpeed;
    }
}
