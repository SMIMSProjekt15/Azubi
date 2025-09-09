using TMPro;
using UnityEngine;

public class MissionUI : MonoBehaviour
{
    [SerializeField] private TMP_Text missionText;

    // Text updaten
    public void SetMission(string newMission)
    {
        missionText.text = newMission;
    }
}
