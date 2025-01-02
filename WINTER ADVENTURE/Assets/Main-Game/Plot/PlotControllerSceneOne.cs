using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlotControllerSceneOne : MonoBehaviour
{
    [SerializeField] private Character characterSister;
    [SerializeField] private GameObject[] wayPointsDecorationTask;
    [SerializeField] private GameObject wayPointBox;
    [SerializeField] private GameObject wayPointFriend;
    [SerializeField] private GameObject wayPointWalkHome;
    [SerializeField] private GameObject wayPointPaper;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float maxScale;
    [SerializeField] private float minScale;
    [SerializeField] private float maxDistance;
    [SerializeField] private Image imageEnd;

    private int maxCountDecorationActive;
    private bool startTask = false;
    
    public static bool playerTalkSisters;
    public static bool playerTakeTheBox;
    public static bool playerDecorationHouse;
    public static bool playerWalkInFriends;
    public static bool playerWalkInHouse;
    public static bool playerTakePaper;

    public static int countDecorationActive = 0;

    private bool startFinalDialog;

    void Start()
    {
        maxCountDecorationActive = GameObject.FindGameObjectsWithTag("Chrismas").Length;
        characterSister.Idialog = true;
        wayPointBox.SetActive(false);
        wayPointFriend.SetActive(false);
        wayPointPaper.SetActive(false);
        wayPointWalkHome.SetActive(false);

        foreach (GameObject wp in wayPointsDecorationTask)
        {
            wp.SetActive(false);
        }
    }

    void Update()
    {
        
        if(playerTakeTheBox && !startTask)
        {
            startTask = true;
            foreach (GameObject wp in wayPointsDecorationTask)
            {
                wp.SetActive(true);
            }
            TaskManager.indexTask++;
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

            if (wayPointBox != null)
            {
                wayPointBox.SetActive(true);
            }
            
        }
        else if(playerTakeTheBox && playerDecorationHouse)
        {
            TaskManager.indexTask++;
            characterSister.Idialog = true;
            playerTalkSisters = false;
            playerTakeTheBox = false;
        }

        if(playerDecorationHouse && !characterSister.Idialog)
        {
            if(wayPointFriend != null)
            {
                wayPointFriend.SetActive(true);
                AdjustSizeRelativeToPlayer(wayPointFriend);                
            }

        }

        if(playerWalkInFriends)
        {
            DialogController.dialogStart = true;
            wayPointPaper.SetActive(true);
            playerWalkInFriends = false;
        }

        if(playerTakePaper)
        {
            wayPointPaper.SetActive(false);
            DialogController.dialogStart = true;
            playerTakePaper = false;
            SpawnEnemy.enemySpawn = true;
            CameraPlayer.rotationPlayer = true;
            wayPointWalkHome.SetActive(true);
            TaskManager.indexTask++;
        }

        if(playerWalkInHouse)
        {
            SpawnEnemy.enemyDie = true;

            if(!startFinalDialog)
            {
                DialogController.dialogStart = true;
                DialogController.dialogIsContinue = true;
                DialogController.countDialogPhrases = 8; 
                startFinalDialog = true;               
            }

            if(DialogController.endDialog)
            {
                StartCoroutine(FadeInUIImage());
            }
        }
    }

    private IEnumerator FadeInUIImage()
    {
        Color color = imageEnd.color;
        float elapsedTime = 0f;

        while (elapsedTime < 2f)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / 2f);
            imageEnd.color = color;

            yield return null;
        }

        color.a = 1f;
        imageEnd.color = color;
    }

    private void AdjustSizeRelativeToPlayer(GameObject target)
    {
        float distance = Vector3.Distance(playerTransform.position, target.transform.position);
        float scale = Mathf.Lerp(minScale, maxScale, Mathf.Clamp01(distance / maxDistance));

        target.transform.localScale = new Vector3(scale, scale, scale);
    }
}
