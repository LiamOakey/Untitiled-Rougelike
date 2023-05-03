using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // scriptable objects
    public GunData gun; //Gun that the character is using
    float fireRate; //how many times the character will shoot per second
    float bulletSpeed; // how fast bullet moves
    float damage;
    int pierce;
    int mag;
    int currentAmmo;
    float reloadSpeed;

    public bool canFire = true;
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

    }

    void Update()
    {
        Debug.Log(canFire);

        if (Input.GetMouseButton(0) && canFire) // Check if the fire button (left-click) is being pressed/held
        {
            if(currentAmmo>0){
                Shoot(); // Call the Shoot method to spawn a projectile
                canFire = false;
                Invoke("shotReset", fireRate); 
            }else{
                canFire = false;
                Invoke("reload",reloadSpeed);
            }
            
        }
    }

    void Shoot()
    {
        currentAmmo--;
        Vector3 mousePosition = Input.mousePosition; // Get the mouse position in screen coordinates
        mousePosition.z = -mainCamera.transform.position.z; // Set the z-coordinate of the mouse position to be the same as the camera's z-coordinate

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition); // Convert the screen coordinates to world coordinates

        Vector2 direction = (worldPosition - firePoint.position).normalized; // Calculate the direction from the fire point to the mouse position

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity); // Spawn a new projectile at the fire point
        projectile.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed; // Set the velocity of the projectile to be in the direction of the mouse position
    }

    void shotReset(){
        canFire = true;
    }

    void reload(){
        currentAmmo = mag;
        canFire = true;
    }


}
