using UnityEngine;

public class NailCollision : MonoBehaviour
{

    public float damage;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Entity enemy))
        {
            enemy.Health -= damage;
        }
        Destroy(gameObject); // Projektil zerstören
    }
}
