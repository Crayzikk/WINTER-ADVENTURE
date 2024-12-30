using UnityEngine;
using TMPro;

public class TaskManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textTask;
    [SerializeField] private string[] tasks;

    public static int indexTask = 0;

    void Update()
    {
        if(indexTask <= tasks.Length - 1)
            textTask.text = tasks[indexTask];
    }
}
