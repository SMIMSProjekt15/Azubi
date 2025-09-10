using UnityEngine;

public class PickupBrick : MonoBehaviour
{
    private bool canPickUp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canPickUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.E))
        {
            // Destroy the brick
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            canPickUp = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        canPickUp = false;
    }
}