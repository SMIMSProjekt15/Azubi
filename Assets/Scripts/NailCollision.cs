using UnityEngine;

public class NailCollision : MonoBehaviour
{

    public float damage;
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Kollision!");
        if (collision.gameObject.TryGetComponent(out Entity enemy))
        {
            Debug.Log("Boom");

            enemy.Health -= damage;
        }
        Destroy(gameObject); // Projektil zerstören
    }
}
