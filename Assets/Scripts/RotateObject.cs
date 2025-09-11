using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Rotation axis (e.g., Vector3.up for Y axis)
    public Vector3 rotationAxis = Vector3.up;
    // Rotation speed in degrees per second
    public float rotationSpeed = 90f;

    void Update()
    {
        // Rotate around the specified axis at the given speed
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }
}
