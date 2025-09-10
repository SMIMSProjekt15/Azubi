using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.01f;

    [SerializeField] private bool PlayerSpeakingFirst;

    [SerializeField] private TextMeshProUGUI playerDialougeText;

    [SerializeField] private GameObject boxCollider;

    [TextArea]
    [SerializeField] private string[] playerDialougeSentences;

    private MissionManager missionManager;

    private bool inArea;

    private bool finished;

    public bool end;

    private int playerIndex;


    //Glaub mir du wirst nichts verstehen. Frag einfach Leon.

    void Start()
    {
        inArea = false;
        finished = false;
        boxCollider.SetActive(false);
        end = false;
    }


    void Update()
    {
        if (inArea && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(TypePlayerDialouge());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = true;
            boxCollider.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        inArea = false;
        boxCollider?.SetActive(false);
    }



    public void StartDialouge()
    {
        if (PlayerSpeakingFirst)
        {
            StartCoroutine(TypePlayerDialouge());
        }
    }

    private IEnumerator TypePlayerDialouge()
    {
        if (playerDialougeSentences[playerIndex] == "")
        {
            end = true;
            boxCollider.SetActive(false);
            missionManager.aufgabenErhalten = true;
        }
        else
        {
            end = false;
            boxCollider.SetActive(true);
        }

        if (finished)
        {
            playerDialougeText.text = "";
            finished = false;
        }

        foreach (char letter in playerDialougeSentences[playerIndex].ToCharArray())
        {
            playerDialougeText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        counter();
        finished = true;
    }

    private int counter()
    {
        if (playerDialougeSentences[playerIndex] != "" || missionManager.getMissionFinished() == true)
        {
            playerIndex++;
        }
        return playerIndex;
    }

}
