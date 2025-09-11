using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class UseNailgun_alt : MonoBehaviour
{
    public float fireCooldown;
    private float currentCooldown;

    public float damage;
    public float range;
    private Transform playerCamera;

    void Start()
    {
        currentCooldown = fireCooldown;
        playerCamera = Camera.main.transform;
    }

    void Update()
    {
        CheckShoot();
    }

    void CheckShoot()
    {
        if (Input.GetMouseButtonDown(0) && currentCooldown <= 0f)
        {
            Debug.Log("Pew");
            Shoot();
            currentCooldown = fireCooldown;
        }
        currentCooldown -= Time.deltaTime;
    }

    void Shoot()
    {
        Ray gunRay = new Ray(playerCamera.position, playerCamera.forward);
        if(Physics.Raycast(gunRay, out RaycastHit hitInfo, range))
        {
            //Debug.Log("Hit " + hit.transform.name);
            // Apply damage to the target if it has a health component
            //Health targetHealth = hit.transform.GetComponent<Health>();
            if(hitInfo.collider.gameObject.TryGetComponent(out Entity enemy))
            {
                enemy.Health -= damage;
            }
        }
    }
}
