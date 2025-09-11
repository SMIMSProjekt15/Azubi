using UnityEngine;

public class WandBauen : MonoBehaviour
{
    private bool canBuild;
    private MissionManager missionManager;

    [SerializeField]
    private GameObject wallObject;
    [SerializeField]
    private GameObject wallShowPlaceObject;

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
            wallObject.SetActive(true);
            wallShowPlaceObject.SetActive(false);
            missionManager.setWall(true);
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