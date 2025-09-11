using UnityEngine;

public class Test : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float moveDuration = 2f; // Zeit bis zur Drehung in Sekunden
    private Rigidbody rb;
    private float timer;
    private bool turned = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timer = 0f;
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (!turned)
        {
            // Vorw�rts bewegen
            rb.linearVelocity = transform.forward * moveSpeed;

            if (timer >= moveDuration)
            {
                // 180 Grad drehen
                Quaternion turnRotation = Quaternion.Euler(0, 180, 0);
                rb.MoveRotation(rb.rotation * turnRotation);

                turned = true;
                timer = 0f; // Timer f�r R�ckweg zur�cksetzen
            }
        }
        else
        {
            // R�ckw�rts bewegen (eigene Vorw�rtsrichtung nach Drehung)
            rb.linearVelocity = transform.forward * moveSpeed;

            if (timer >= moveDuration)
            {
                // Stoppen
                rb.linearVelocity = Vector3.zero;
                enabled = false; // Script abschalten, wenn komplett fertig
            }
        }
    }
}
