using UnityEngine;

public class Rats : MonoBehaviour
{
    public float speed = 3f;
    public float range = 10f;
    private bool movingForward = true;
    private bool moving = true;

    private float startZ;
    private float endZ;

    void Start()
    {
        startZ = transform.position.z;
        endZ = startZ + range;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            moving = false;

        if (!moving) return;

        float step = speed * Time.deltaTime;

        // Bewegung nur auf Z-Achse
        Vector3 pos = transform.position;

        if (movingForward)
        {
            pos.z += step;
            transform.rotation = Quaternion.Euler(0, 0, 0); // schaut vorwärts
        }
        else
        {
            pos.z -= step;
            transform.rotation = Quaternion.Euler(0, 180, 0); // schaut rückwärts
        }

        transform.position = pos;

        // Richtung wechseln nur am Ende der Strecke
        if (movingForward && transform.position.z >= endZ)
            movingForward = false;
        else if (!movingForward && transform.position.z <= startZ)
            movingForward = true;
    }
}
