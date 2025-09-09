using UnityEngine;

public class MissionManager : MonoBehaviour
{
    int aufgabeueberpruefen = 1;
    [SerializeField] private MissionUI missionUI;
    private string currentMission;
    private PickUpHammer pickUpHammer;

    private void Start()
    {
        pickUpHammer = FindObjectOfType<PickUpHammer>();
        // Startmission setzen

        currentMission = "Bringe den Hammer zu deinem Chef! 0/1";
        missionUI.SetMission(currentMission);
        aufgabeueberpruefen = 1;
    }

    void Update()
    {

        // Beispiel: Taste M drücken → Mission ändern
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentMission = "Bringe den Hammer zu deinem Chef! 0/1";
            missionUI.SetMission(currentMission);
            aufgabeueberpruefen = 1;
        }

        if (pickUpHammer.hammer == true)
        {
            currentMission = "Bringe den Hammer zu deinem Chef! 1/1";
            missionUI.SetMission(currentMission);
            aufgabeueberpruefen = 1;
        }

        // Beispiel: Taste N drücken → nächste Mission
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (aufgabeueberpruefen == 1)
            {
                currentMission = "Bringe die 5 Ziegelsteine zum Chef! 0/5";
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
                currentMission = "Bringe den Hammer zu deinem Chef! 0/1";
                missionUI.SetMission(currentMission);
                aufgabeueberpruefen = 1;
            }

        }
    }
}
