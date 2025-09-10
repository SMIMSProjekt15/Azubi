using UnityEngine;

public class MissionManager : MonoBehaviour
{
    int aufgabeueberpruefen = 1;
    [SerializeField] private MissionUI missionUI;
    private string currentMission;
    private PickUpHammer pickUpHammer;
    private PickUpBrick pickUpBrick;
    private int brickTwo;
    private int ueberpruefen;
    public bool aufgabenErhalten = false;
    private bool missionFinished;

    private void Start()
    {
        pickUpHammer = FindFirstObjectByType<PickUpHammer>();
        pickUpBrick = FindFirstObjectByType<PickUpBrick>();

        // Startmission setzen
        aufgabeueberpruefen = 1;
        aufgabenErhalten = true;    
    }

    public void addPoint()
    {
        brickTwo = brickTwo + 1;
    }

    public void mission2()
    {
        if (brickTwo < 3)
        {
            currentMission = "Finde die 3 Ziegelsteine " + brickTwo + "/3";
            missionUI.SetMission(currentMission);
            aufgabeueberpruefen = 3;
            aufgabenErhalten = false;
        }
        else
        {
            currentMission = "Bringe die 3 Ziegelsteine zu deinem Chef! " + brickTwo + "/3";
            missionUI.SetMission(currentMission);
            aufgabeueberpruefen = 3;
            aufgabenErhalten = false;
        }
    }

    public void mission3()
    {
        currentMission = "Baue die Mauer!";
        missionUI.SetMission(currentMission);
        aufgabeueberpruefen = 4;
        aufgabenErhalten = false;
    }

    public void mission4()
    {
        currentMission = "Töte die Ratten!";
        missionUI.SetMission(currentMission);
        aufgabeueberpruefen = 1;
        aufgabenErhalten = false;
    }

    public void mission1()
    {
        currentMission = "Finde den Hammer! 0/1";
        missionUI.SetMission(currentMission);
        aufgabeueberpruefen = 2;
        aufgabenErhalten = false;
    }

    void Update()
    {
        if (brickTwo == 3)
        {
            currentMission = "Bringe die 3 Ziegelsteine zu deinem Chef! " + brickTwo + "/3";
            missionUI.SetMission(currentMission);
            aufgabeueberpruefen = 3;
        }


        if (pickUpHammer.hammer == true)
        {
            currentMission = "Bringe den Hammer zu deinem Chef! 1/1";
            missionUI.SetMission(currentMission);
            aufgabeueberpruefen = 1;
            pickUpHammer.hammer = false;
            missionFinished = true;
        }

   
        if (aufgabenErhalten == true)
        {
            if (aufgabeueberpruefen == 1)
            {
                mission1();
            }

            else if (aufgabeueberpruefen == 2)
            {
                missionFinished = false;
                mission2();
            }

            else if (aufgabeueberpruefen == 3)
            {
                mission3();
            }

            else if (aufgabeueberpruefen == 4)
            {
                mission4();
            }

        aufgabenErhalten = false;
        }
    }

    public bool getMissionFinished()
    {
        return missionFinished;
    }
}
