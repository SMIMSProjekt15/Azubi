using UnityEngine;

public class Chef : MonoBehaviour
{
    private bool canTalk;

    public MissionManager missionManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bool canTalk = false;
        missionManager = FindFirstObjectByType<MissionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canTalk && Input.GetKey(KeyCode.E))
        {
            // Destroy the brick
            missionManager.aufgabenErhalten = true;
            canTalk = false;    
        }
        
    }
    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            canTalk = true;
        }
    }
}
