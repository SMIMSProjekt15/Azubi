using System.Collections ;
using System.Collections.Generic ;
using UnityEngine ;

public class CameraChange : MonoBehaviour
{
    public GameObject ThirdCam ;
    public GameObject FirstCam ;
    public int camMode = 0 ;
    private int storedCamMode = 0;

    void Update()
    {
        if (Input.GetButtonDown("Camera"))
        {
            if (camMode == 1)
            {
                camMode = 0 ;
            }
            else
            {
                camMode += 1;
            }

            StartCoroutine(CamChange()) ;
        }
        else if (camMode != storedCamMode)
        {
            storedCamMode = camMode;
            StartCoroutine(CamChange());
        }
    }

    IEnumerator CamChange()
    {
        yield return new WaitForSeconds(0.01f) ;
        if (camMode == 0)
        {
            ThirdCam.SetActive(true) ;
            FirstCam.SetActive(false) ;
        }
        if (camMode == 1)
        {
            FirstCam.SetActive(true) ;
            ThirdCam.SetActive(false) ;
        }
    }
}