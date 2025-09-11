using UnityEngine;

public class PickUpHammer : MonoBehaviour
{
    private bool canPickUp;
    private MissionManager missionManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canPickUp = false;
        missionManager = FindFirstObjectByType<MissionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canPickUp && Input.GetKey(KeyCode.E))
        {
            // Destroy the hammer
            missionManager.setHammer(true);
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