using UnityEngine;

public class CustomGravityNail : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float gravityScale = 0.5f;  // Faktor für schwächere Gravitation

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;  // eingebaute Gravitation ausschalten
    }

    void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * gravityScale * rb.mass, ForceMode.Acceleration);
    }
}
