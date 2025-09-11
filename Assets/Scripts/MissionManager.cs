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
    private bool hammer = false; 
    private bool wall = false;
    private int ratCount = 0;
    private int ratCountTotal;
    private bool showMovementUI = false;

    public bool missionBekommen = false;
    public bool haveHammer = false;   // gesetzt sobald der Spieler den Hammer aufnimmt
    public bool haveBricks = false;   // gesetzt sobald der Spieler 3 Ziegel hat
    public bool wallFinish = false;   // gesetzt sobald die Mauer gebaut wurde

    [SerializeField]
    private GameObject hammerObject;
    [SerializeField]
    private GameObject bricksObject;
    [SerializeField]
    private GameObject wallObject;
    [SerializeField]
    private GameObject ratsObject;
    [SerializeField]
    private GameObject nailGunObject;

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
        if (hammer)
        {
            haveHammer = true;
            currentMission = "Bringe den Hammer zu deinem Chef! 1/1";
            missionUI.SetMission(currentMission);
            hammer = false; // das Flag in PickUpHammer zurücksetzen (sonst würde es jedes Update erneut triggern)
        }

        if (aufgabeueberpruefen == 3)
        {
            if (wall)
            {
                wallFinish = true;
                currentMission = "Gehe zurück zu deinem Boss";
                missionUI.SetMission(currentMission);
                wall = false; // das Flag in WandBauen zurücksetzen (sonst würde es jedes Update erneut triggern)
            }
        }
        

        // Brick-Sammel-Status (nur UI-Update während der Brick-Mission)
        if (aufgabeueberpruefen == 2)
        {
            if (brickTwo < 3)
            {
                currentMission = "Finde die 3 Ziegelsteine " + brickTwo + "/3";
                missionUI.SetMission(currentMission);
                bricksObject.SetActive(true);
            }
            else // >=3
            {
                haveBricks = true;
                currentMission = "Bringe die 3 Ziegelsteine zu deinem Chef! " + brickTwo + "/3";
                missionUI.SetMission(currentMission);
                // Achtung: Wir setzen nicht sofort missionFinished; das passiert erst beim Boss-Dialog (Übergabe)
            }
        }

        if (aufgabeueberpruefen == 4)
        {
            
            currentMission = "Finde die Nagelpistole und töte die Ratten! " + ratCount + "/" + ratCountTotal;
            missionUI.SetMission(currentMission);
            if (ratCount >= ratCountTotal)
            {
                currentMission = "Gehe zurück zu deinem Boss";
                missionUI.SetMission(currentMission);
            }
        }
    }

    // Wird vom PickUpBrick aufgerufen, wenn ein Ziegel aufgenommen wurde
    public void addPoint()
    {
        brickTwo = Mathf.Min(brickTwo + 1, 3);
    }

    public void addRat()
    {
        ratCount++;
    }

    // UI-Hilfsmethoden (wie vorher)
    public void mission1()
    {
        currentMission = "Finde den Hammer! 0/1";
        missionUI.SetMission(currentMission);
        hammerObject.SetActive(true);
    }

    public void mission3()
    {
        currentMission = "Baue die Mauer auf dem Dach!";
        missionUI.SetMission(currentMission);
        wallObject.SetActive(true);
    }

    public void mission4()
    {
        showMovementUI = true;
        ratsObject.SetActive(true);
        nailGunObject.SetActive(true);
        GameObject[] rats = GameObject.FindGameObjectsWithTag("Rat");
        ratCountTotal = rats.Length; // Gesamtzahl der Ratten im Level
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

    public void setHammer(bool status)
    {
        hammer = status;
    }

    public void setWall(bool status)
    {
        wall = status;
    }

    public void setShowMovementUI(bool status)
    {
        showMovementUI = status;
    }

    public bool getShowMovementUI()
    {
        return showMovementUI;
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
        if (aufgabeueberpruefen == 4) // Ratten-Mission
        {
            return ratCount >= ratCountTotal;
        }
        // weitere Checks bei weiteren Missionen ergänzen
        return false;
    }
}
