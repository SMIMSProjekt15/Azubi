using UnityEngine;

public class MissionUIController : MonoBehaviour
{
    [SerializeField] private GameObject overlayField; // dein UI-Element
    [SerializeField] private GameObject overlaytextField; // dein UI-Element
    [SerializeField] private GameObject overlayTextField; // dein UI-Element
    [SerializeField] private MissionManager missionManager;

    void Start()
    {
        if (missionManager == null)
            missionManager = FindFirstObjectByType<MissionManager>();
    }

    void Update()
    {
        // Beispiel: Mission 1 hat begonnen
        if (missionManager.aufgabeueberpruefen == 1)
        {
            overlayField.SetActive(false); // versteckt das Feld
            overlaytextField.SetActive(false); // versteckt auch text
            overlayTextField.SetActive(false); // versteckt anderen text
        }
    }
}
