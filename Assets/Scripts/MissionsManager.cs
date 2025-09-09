using UnityEngine;

public class MissionManager : MonoBehaviour
{
    int aufgabeueberpruefen = 1;
    [SerializeField] private MissionUI missionUI;
    private string currentMission;

    private void Start()
    {
        // Startmission setzen
        currentMission = "Bringe den Hammer zu deinem Chef!";
        missionUI.SetMission(currentMission);
    }

    private void Update()
    {
        // Beispiel: Taste M drücken → Mission ändern
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentMission = "Bringe den Hammer zu deinem Chef!";
            missionUI.SetMission(currentMission);
            aufgabeueberpruefen = 1;
        }

        // Beispiel: Taste N drücken → nächste Mission
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (aufgabeueberpruefen == 1) 
            {
                currentMission = "Bringe die 5 Ziegelsteine zum Chef!";
                missionUI.SetMission(currentMission);
                aufgabeueberpruefen = aufgabeueberpruefen + 1;
            }

            else if (aufgabeueberpruefen == 2)
            {
                currentMission = "Baue die Mauer!";
                missionUI.SetMission(currentMission);
                aufgabeueberpruefen = aufgabeueberpruefen + 1;
            }

            else if (aufgabeueberpruefen == 3)
            {
                currentMission = "Töte die Ratten!";
                missionUI.SetMission(currentMission);
                aufgabeueberpruefen = aufgabeueberpruefen + 1;
            }

            else if (aufgabeueberpruefen == 4)
            {
                currentMission = "Bringe den Hammer zu deinem Chef!";
                missionUI.SetMission(currentMission);
                aufgabeueberpruefen = 1;
            }

        }
    }
}
