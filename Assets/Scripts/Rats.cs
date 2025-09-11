using UnityEngine;
using System.Collections;

public class Rats : MonoBehaviour
{
    public float speed = 3f;         // Geschwindigkeit
    public float range = 10f;        // Distanz pro Lauf
    public float rotationStep = 10f; // Drehung pro Schritt
    public float waitTime = 0.05f;   // Zeit zwischen den Drehschritten

    private bool movingForward = true;
    private bool moving = true;
    private float startZ;
    private float endZ;

    void Start()
    {
        startZ = transform.localPosition.z;
        endZ = startZ + range;
        StartCoroutine(MoveRat());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            moving = false;
    }

    IEnumerator MoveRat()
    {
        while (true)
        {
            if (moving)
            {
                // Geradeaus laufen auf der Z-Achse (global)
                while ((movingForward && transform.localPosition.z < endZ) ||
                       (!movingForward && transform.localPosition.z > startZ))
                {
                    float step = speed * Time.deltaTime;
                    Vector3 pos = transform.localPosition;
                    pos.z += movingForward ? step : -step;
                    transform.localPosition = pos;
                    yield return null; // wartet bis zum nächsten Frame
                }

                // Richtung wechseln
                movingForward = !movingForward;

                // Stufenweise drehen (Y-Achse)
                float totalRotation = 180f; // drehen um 180° am Ende der Strecke
                int steps = Mathf.CeilToInt(totalRotation / rotationStep);
                for (int i = 0; i < steps; i++)
                {
                    transform.Rotate(0, rotationStep, 0);
                    yield return new WaitForSeconds(waitTime);
                }
            }
            else
            {
                yield return null; // pausiert, wenn Bewegung gestoppt
            }
        }
    }
}
