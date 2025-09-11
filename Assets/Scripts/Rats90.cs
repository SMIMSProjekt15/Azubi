using UnityEngine;
using System.Collections;

public class Rats90 : MonoBehaviour
{
    public float speed = 3f;         // Geschwindigkeit
    public float range = 10f;        // Distanz pro Lauf
    public float rotationStep = 10f; // Drehung pro Schritt
    public float waitTime = 0.05f;   // Zeit zwischen den Drehschritten

    private bool movingRight = true;
    private bool moving = true;
    private float startX;
    private float endX;

    void Start()
    {
        startX = transform.localPosition.x;
        endX = startX + range;
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
                while ((movingRight && transform.localPosition.x < endX) ||
                       (!movingRight && transform.localPosition.x > startX))
                {
                    float step = speed * Time.deltaTime;
                    Vector3 pos = transform.localPosition;
                    pos.x += movingRight ? step : -step;
                    transform.localPosition = pos;
                    yield return null; // wartet bis zum nächsten Frame
                }

                // Richtung wechseln
                movingRight = !movingRight;

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
