using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupNailgun : MonoBehaviour
{
    [SerializeField]
    private Transform pickUpParent;

    private bool itemInHand;

    private GameObject pickableItem;

    private bool canPickUp;
    private CameraChange cameraChange;

    void Start()
    {
        cameraChange = FindFirstObjectByType<CameraChange>();
        canPickUp = false;
        itemInHand = false;
    }

    void Update()
    {
        PickUp();
        Drop();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "Nailgun" tag
        if (other.CompareTag("Nailgun"))
        {
            // Debug.Log("Nailgun in range");
            canPickUp = true;
            pickableItem = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        canPickUp = false;
    }

    void PickUp()
    {
        if (!itemInHand && canPickUp && Input.GetKeyDown(KeyCode.E))
        {
            // Attach the nailgun to the player's hand

            cameraChange.camMode = 1;

            pickableItem.transform.localPosition = Vector3.zero; // Adjust position as needed
            pickableItem.transform.localRotation = Quaternion.identity; // Adjust rotation as needed
            pickableItem.transform.SetParent(pickUpParent.transform, false);
            // Optionally disable physics and collider
            Rigidbody rb = pickableItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
            Collider col = pickableItem.GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = false;
            }
            // Activate the in-hand item boolean
            itemInHand = true;
        }
    }

    void Drop()
    {
        if (itemInHand && Input.GetKeyDown(KeyCode.Q))
        {
            cameraChange.camMode = 0;

            // Detach the nailgun from the player's hand
            pickableItem.transform.SetParent(null);
            // Optionally enable physics and collider
            Rigidbody rb = pickableItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
            Collider col = pickableItem.GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = true;
            }
            // Deactivate the in-hand item boolean
            itemInHand = false;
            pickableItem.transform.position = pickUpParent.position + pickUpParent.forward; // Drop in front of the player
        }
    }
}
