using System.Collections;
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
    private bool isTyping;
    private int playerIndex;

    void Start()
    {
        inArea = false;
        isTyping = false;
        playerIndex = 0;
        if (boxCollider != null) boxCollider.SetActive(false);
        missionManager = FindFirstObjectByType<MissionManager>();
    }

    void Update()
    {
        // Spieler drückt E
        if (inArea && Input.GetKeyDown(KeyCode.E) && !isTyping)
        {
            // ⚠️ Blockieren, wenn Mission läuft aber noch NICHT abgeschlossen ist
            if (missionManager.getMissionBekommen() && !missionManager.IsObjectiveComplete())
            {
                return; // Nichts machen → Text bleibt stehen, keine neuen Zeilen
            }

            StartCoroutine(HandleDialogue());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = true;
            if (boxCollider != null) boxCollider.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = false;
            if (boxCollider != null) boxCollider.SetActive(false);
        }
    }

    public void StartDialouge()
    {
        if (PlayerSpeakingFirst && !isTyping)
        {
            StartCoroutine(HandleDialogue());
        }
    }

    private IEnumerator HandleDialogue()
    {
        if (playerDialougeSentences == null || playerDialougeSentences.Length == 0) yield break;
        if (playerIndex >= playerDialougeSentences.Length) playerIndex = playerDialougeSentences.Length - 1;

        string sentence = playerDialougeSentences[playerIndex];

        // === LEERE ZEILE = MISSION-MARKER ===
        if (string.IsNullOrEmpty(sentence))
        {
            if (missionManager != null && (!missionManager.getMissionBekommen() || missionManager.IsObjectiveComplete()))
            {
                missionManager.aufgabeUpdate();           // neue Mission setzen
                missionManager.setMissionBekommen(true);
            }

            // Dialog nach Mission beenden: Text löschen, Blase schließen
            playerDialougeText.text = "";
            if (boxCollider != null) boxCollider.SetActive(false);

            // Index eins weiter → falls irgendwann noch Dialog danach käme
            playerIndex = Mathf.Min(playerIndex + 1, playerDialougeSentences.Length - 1);
            yield break;
        }

        // === NORMALE ZEILE TIPPSEN ===
        isTyping = true;
        playerDialougeText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            playerDialougeText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

        // Nächste Zeile vorbereiten
        playerIndex = Mathf.Min(playerIndex + 1, playerDialougeSentences.Length - 1);
    }
}
