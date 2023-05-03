using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float fireRate = 4f;
    public bool canFire = true;
    public float bulletSpeed = 15f;
    public Transform firePoint; // The position where the projectile will be spawned
    public GameObject projectilePrefab; // The projectile prefab to be spawned

    private Camera mainCamera; // The main camera in the scene

    void Start()
    {
        fireRate = 1/fireRate;
        mainCamera = Camera.main; // Get the main camera in the scene
    }

    void Update()
    {
        

        if (Input.GetMouseButton(0) && canFire) // Check if the fire button (left-click) is being pressed/held
        {
            Shoot(); // Call the Shoot method to spawn a projectile
            canFire = false;
            Invoke("shotReset", fireRate); 
        }
    }

    void Shoot()
    {
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


}
