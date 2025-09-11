using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public Toggle fullscreenTog, vsyncTog;  

    public List<ResItem> resolutions = new List<ResItem>();
    private int selectedRes;

    public TMP_Text resoltionLabel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fullscreenTog.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
        {
            vsyncTog.isOn = false;
        }else
        {
            vsyncTog.isOn = true;
        }

        bool foundRes = false;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].width && Screen.height == resolutions[i].height)
            {
                foundRes = true;
                selectedRes = i;
                UpdateResLabel();
            }
        }

        if (!foundRes)
        {
            ResItem newRes = new ResItem();
            newRes.width = Screen.width;
            newRes.height = Screen.height;
            resolutions.Add(newRes);
            selectedRes = resolutions.Count - 1;
            UpdateResLabel();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResLeft()
    {
        selectedRes--;
        if (selectedRes < 0)
        {
            selectedRes = 0;
        }
        UpdateResLabel();
        //Screen.SetResolution(resolutions[selectedRes].width, resolutions[selectedRes].height, Screen.fullScreen);
    }

    public void ResRight()
    {
        selectedRes++;
        if (selectedRes > resolutions.Count - 1)
        {
            selectedRes = resolutions.Count -1;
        }
        //Screen.SetResolution(resolutions[selectedRes].width, resolutions[selectedRes].height, Screen.fullScreen);
        UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        //Screen.SetResolution(resolutions[selectedRes].width, resolutions[selectedRes].height, Screen.fullScreen);
        resoltionLabel.text = resolutions[selectedRes].width.ToString() + " x " + resolutions[selectedRes].height.ToString();
    }

    public void ApplyGraphics()
    {
        //Screen.fullScreen = fullscreenTog.isOn;
        if (vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }else
        {
            QualitySettings.vSyncCount = 0;
        }
        Screen.SetResolution(resolutions[selectedRes].width, resolutions[selectedRes].height, fullscreenTog.isOn);
    }

    [System.Serializable]
    public class ResItem
    {
         public int width, height;
    }
}
