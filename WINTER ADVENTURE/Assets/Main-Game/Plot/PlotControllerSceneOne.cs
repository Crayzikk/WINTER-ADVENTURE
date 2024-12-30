using UnityEngine;
using UnityEngine.AI;

public class PlotControllerSceneOne : MonoBehaviour
{
    [SerializeField] private Character characterSister;
    [SerializeField] private GameObject[] wayPointsDecorationTask;
    [SerializeField] private GameObject wayPointBox;

    private int maxCountDecorationActive;
    private bool startTask = false;
    
    public static bool playerTalkSisters;
    public static bool playerTakeTheBox;
    public static bool playerDecorationHouse;
    public static bool playerWalkInFriends;
    public static bool playerWalkInHouse;
    public static int countDecorationActive = 0;

    void Start()
    {
        maxCountDecorationActive = GameObject.FindGameObjectsWithTag("Chrismas").Length;
        characterSister.Idialog = true;
        wayPointBox.SetActive(false);

        foreach (GameObject wp in wayPointsDecorationTask)
        {
            wp.SetActive(false);
        }
    }

    void Update()
    {
        Debug.LogWarning(playerTakeTheBox);
        
        if(playerTakeTheBox && !startTask)
        {
            startTask = true;
            foreach (GameObject wp in wayPointsDecorationTask)
            {
                wp.SetActive(true);
            }
        }

        if(countDecorationActive == maxCountDecorationActive)
        {
            playerDecorationHouse = true;
        }

        if(characterSister.IdialogPlayer && DialogController.endDialog && !playerTalkSisters)
        {
            characterSister.IdialogPlayer = false;
            characterSister.Idialog = false;
            playerTalkSisters = true;
            TaskManager.indexTask++;
            wayPointBox.SetActive(true);
        }
        else if(playerTakeTheBox && playerDecorationHouse)
        {
            characterSister.Idialog = true;
            playerTalkSisters = false;
            playerTakeTheBox = false;
        }
        
    }
}
