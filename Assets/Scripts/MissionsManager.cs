using UnityEngine;

public class MissionManager : MonoBehaviour
{
    // "aufgabeueberpruefen" ist hier die Missionsstufe (0 = Start, 1 = erste Mission, ...)
    public int aufgabeueberpruefen = 0;
    [SerializeField] private MissionUI missionUI;
    private string currentMission;
    private PickUpHammer pickUpHammer;
    private PickUpBrick pickUpBrick;
    private WandBauen wandBauen;
    private int brickTwo = 0;

    public bool missionBekommen = false;
    public bool haveHammer = false;   // gesetzt sobald der Spieler den Hammer aufnimmt
    public bool haveBricks = false;   // gesetzt sobald der Spieler 3 Ziegel hat
    public bool wallFinish = false;   // gesetzt sobald die Mauer gebaut wurde

    private void Start()
    {
        pickUpHammer = FindFirstObjectByType<PickUpHammer>();
        pickUpBrick = FindFirstObjectByType<PickUpBrick>();
        wandBauen = FindFirstObjectByType<WandBauen>();

        // Startmission (nur Anzeige: gehe zum Boss)
        aufgabeueberpruefen = 0;
        currentMission = "Hole die erste Mission bei deinem Boss ab";
        missionUI.SetMission(currentMission);
    }

    void Update()
    {
        // Wenn der PickUp-Script ein Flag setzt (z.B. hammer = true), merken wir uns das als Besitz
        if (pickUpHammer != null && pickUpHammer.hammer)
        {
            haveHammer = true;
            currentMission = "Bringe den Hammer zu deinem Chef! 1/1";
            missionUI.SetMission(currentMission);
            pickUpHammer.hammer = false; // das Flag in PickUpScript zurücksetzen (sonst würde es jedes Update erneut triggern)
        }

        if (wandBauen.wall)
        {
            wallFinish = true;
            currentMission = "Gehe zurück zu deinem Boss";
            missionUI.SetMission(currentMission);
            wandBauen.wall = false; // das Flag in WandBauen zurücksetzen (sonst würde es jedes Update erneut triggern)
        }

        // Brick-Sammel-Status (nur UI-Update während der Brick-Mission)
        if (aufgabeueberpruefen == 2)
        {
            if (brickTwo < 3)
            {
                currentMission = "Finde die 3 Ziegelsteine " + brickTwo + "/3";
                missionUI.SetMission(currentMission);
            }
            else // >=3
            {
                haveBricks = true;
                currentMission = "Bringe die 3 Ziegelsteine zu deinem Chef! " + brickTwo + "/3";
                missionUI.SetMission(currentMission);
                // Achtung: Wir setzen nicht sofort missionFinished; das passiert erst beim Boss-Dialog (Übergabe)
            }
        }
    }

    // Wird vom PickUpBrick aufgerufen, wenn ein Ziegel aufgenommen wurde
    public void addPoint()
    {
        brickTwo = Mathf.Min(brickTwo + 1, 3);
    }

    // UI-Hilfsmethoden (wie vorher)
    public void mission1()
    {
        currentMission = "Finde den Hammer! 0/1";
        missionUI.SetMission(currentMission);
    }

    public void mission3()
    {
        currentMission = "Baue die Mauer!";
        missionUI.SetMission(currentMission);
    }

    public void mission4()
    {
        currentMission = "Töte die Ratten!";
        missionUI.SetMission(currentMission);
    }

    public bool getMissionBekommen()
    {
        return missionBekommen;
    }

    public void setMissionBekommen(bool status)
    {
        missionBekommen = status;
        // wenn neue Mission gegeben wird, sind Objectives noch nicht erfüllt
        if (status)
        {
            haveHammer = false;
            haveBricks = false;
            // ggf. brickTwo = 0; je nach gewünschtem Verhalten
        }
    }

    // Erhöht die "Stage" und setzt die entsprechende Mission (wird vom DialogManager aufgerufen)
    public void aufgabeUpdate()
    {
        // nächstes Missions-Stadium
        aufgabeueberpruefen++;
        missionBekommen = true;

        if (aufgabeueberpruefen == 1)
        {
            mission1(); // z. B. Hammer-Mission
        }
        else if (aufgabeueberpruefen == 2)
        {
            // Ziegel sammeln
            currentMission = "Finde die 3 Ziegelsteine 0/3";
            missionUI.SetMission(currentMission);
        }
        else if (aufgabeueberpruefen == 3)
        {
            mission3();
        }
        else if (aufgabeueberpruefen == 4)
        {
            mission4();
        }
        else if (aufgabeueberpruefen >= 5)
        {
            currentMission = "Alle Missionen abgeschlossen!";
            missionUI.SetMission(currentMission);
            missionBekommen = false;
        }
    }

    // DialogManager fragt das ab, um zu wissen, ob der Spieler die Aufgabe bereits erfüllt hat
    public bool IsObjectiveComplete()
    {
        if (aufgabeueberpruefen == 1) // Hammer-Mission
        {
            return haveHammer;
        }
        if (aufgabeueberpruefen == 2) // Ziegel-Mission
        {
            return brickTwo >= 3;
        }
        if (aufgabeueberpruefen == 3) // Mauer-Mission
        {
            return wallFinish;
        }
        // weitere Checks bei weiteren Missionen ergänzen
        return false;
    }
}
