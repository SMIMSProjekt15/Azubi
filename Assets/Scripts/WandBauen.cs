using UnityEngine;

public class WandBauen : MonoBehaviour
{
    private bool canBuild;
    public bool wall = false;
    public MissionManager missionManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canBuild = false;
        missionManager = FindFirstObjectByType<MissionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canBuild && Input.GetKeyDown(KeyCode.E))
        {
            transform.localScale = transform.localScale * 2f;
            wall = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            canBuild = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        canBuild = false;
    }
}