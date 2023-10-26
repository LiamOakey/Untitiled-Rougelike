using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public GunData minigun;
    // scriptable objects
    public GunData gun; //Gun that the character is using
    static float fireRate; //how many times the character will shoot per second
    float bulletSpeed; // how fast bullet moves
    public float damage; 
    public int pierce; //How many enemies shots will go through
    public float knockback; //amount of knockback delt to enemies
    int mag; //Max Ammo
    int currentAmmo; 
    int projectileCount; // Amount of projectiles fired per shot
    float reloadSpeed; // time in seconds
    float spread; //Random variation on the y axis for shots

    public bool canFire = true;
    bool canReload = true;
    public Transform firePoint; // The position where the projectile will be spawned
    public GameObject projectilePrefab; // The projectile prefab to be spawned

    private Camera mainCamera; // The main camera in the scene

    void Awake()
    { 
        damage = gun.damage;
        pierce = gun.pierce;
        bulletSpeed = gun.bulletSpeed;
        fireRate = gun.fireRate;
        fireRate = 1/fireRate;
        mainCamera = Camera.main; // Get the main camera in the scene
        mag = gun.mag;
        currentAmmo = mag;
        reloadSpeed = gun.reloadSpeed;
        projectileCount = gun.projectileCount;
        spread = gun.spread;
        knockback = gun.knockback;
        ammoText.text = mag.ToString(); //set up ammo counter

    }

    void Update()
    {

        if(Input.GetKeyDown("r") && canReload){
            reload();
        }

        if(Input.GetKeyDown("1")){
            weaponSwap();
        }

    }

    void FixedUpdate(){
        if (Input.GetMouseButton(0) && canFire && canReload) // Check if the fire button (left-click) is being pressed/held
        {

            if(currentAmmo>0){
                Fire(); // Call the Shoot method to spawn a projectile
                canFire = false;
                Invoke("shotReset", fireRate);
            }else{
                if(canReload){
                    reload();
                }
            }
            
        }


    }

    //Fire will be called when mouse is clicked, to manage how many time shoot will be called
    void Fire(){
        currentAmmo--;
        ammoText.text = currentAmmo.ToString();//update ammo counter
        for(int i=0; i<projectileCount;i++){
            Shoot();
        }
    }

    void Shoot()
    {
        
        GameObject projectile; //Bullet

        Vector3 mousePosition = Input.mousePosition; // Get the mouse position in screen coordinates
        


        mousePosition.z = -mainCamera.transform.position.z; // Set the z-coordinate of the mouse position to be the same as the camera's z-coordinate

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition); // Convert the screen coordinates to world coordinates

        Vector2 direction = (worldPosition - firePoint.position).normalized; // Calculate the direction from the fire point to the mouse position
        
        /*Shot spread is based on the weapons "spread" value, spread works as a displacement
         of the mouse on the x and y axis, with the distance being a random number between 
        the positive and negative spread value */

        float shotSpread = Random.Range(-spread,spread);
        direction.x += shotSpread;
        shotSpread = Random.Range(-spread,spread);
        direction.y += shotSpread;
        


        // Spawn a new projectile at the fire point
        projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().rotation = GunRotation.angle;

        projectile.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }

    void shotReset(){
        canFire = true;
    }

    void reload(){
        canReload = false;
        canFire = false;
        Invoke("load",reloadSpeed);
        ammoText.text = "0";
    }

    void load(){
        canReload = true;
        currentAmmo = mag;
        canFire = true;
        ammoText.text = currentAmmo.ToString(); //update ammo counter
    }

    void weaponSwap(){
        gun = minigun;

        damage = gun.damage;
        pierce = gun.pierce;
        bulletSpeed = gun.bulletSpeed;
        fireRate = gun.fireRate;
        fireRate = 1/fireRate;
        mag = gun.mag;
        currentAmmo = mag;
        reloadSpeed = gun.reloadSpeed;
        projectileCount = gun.projectileCount;
        spread = gun.spread;
        knockback = gun.knockback;
        ammoText.text = mag.ToString(); //set up ammo counter

        Debug.Log("swapped");
    }

    public static void setFireRate(float change){
        fireRate *= change;
        Debug.Log(fireRate);
    }


}
