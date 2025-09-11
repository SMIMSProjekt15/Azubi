using UnityEngine;

public class UseNailgun : MonoBehaviour
{
    public GameObject nailPrefab;
    public Transform firePoint;
    public float nailForce = 20f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject nail = Instantiate(nailPrefab, firePoint.position, firePoint.rotation);
        nail.transform.Rotate(-90f, 0f, 0f); // Rotate nail to point forward
        Rigidbody rb = nail.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * nailForce, ForceMode.Impulse);
    }
}
