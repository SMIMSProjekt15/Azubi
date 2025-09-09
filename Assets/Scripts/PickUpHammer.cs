using UnityEngine;

public class PickUpHammer : MonoBehaviour
{
    private bool canPickUp;
    public bool hammer = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canPickUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canPickUp && Input.GetKey(KeyCode.E))
        {
            // Destroy the brick
            Destroy(gameObject);
            hammer = true;
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