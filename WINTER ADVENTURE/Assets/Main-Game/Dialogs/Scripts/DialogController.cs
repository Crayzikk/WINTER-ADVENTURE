using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  

public class DialogController : MonoBehaviour
{
    [SerializeField] private Image windowDialog;
    [SerializeField] private TextMeshProUGUI textDialog;  
    [SerializeField] private TextMeshProUGUI textCharacter;  
    [SerializeField] private TextMeshProUGUI textExit;  
    [SerializeField] private string[] phrasesDialog;
    [SerializeField] private string[] nameCharacterDialog;
    [SerializeField] private float time;

    public static bool dialogStart = false;
    public static bool dialogIsContinue = false;
    public static bool endDialog = false;
    public static int countDialogPhrases;

    private int indexNameCharacterDialog;
    private int indexPharesesDialog;

    private bool dialog;

    void Start()
    {
        indexNameCharacterDialog = 0;
        indexPharesesDialog = 0;

        HideDialog();
    }

    void Update()
    {
        if(dialogStart)
        {
            dialog = true;
            dialogStart = false;
            endDialog = false;

            ShowDialog();
        }

        if(Input.GetKey(KeyCode.F) && dialog)
        {        
            dialog = false;

            indexNameCharacterDialog++;
            indexPharesesDialog++;
            
            if(dialogIsContinue && countDialogPhrases > 0)
            {
                dialogStart = true;
                countDialogPhrases--;

                HideDialog();
                return;
            }
            else
            {
                endDialog = true;
                HideDialog();
            }
        }
    }

    private void RunDialog()
    {

        textCharacter.text = nameCharacterDialog[indexNameCharacterDialog];

        textDialog.text = phrasesDialog[indexPharesesDialog];

    }

    private void ShowDialog()
    {
        windowDialog.enabled = true;
        textExit.enabled = true;
        RunDialog();
    }

    private void HideDialog()
    {
        windowDialog.enabled = false;
        textExit.enabled = false;
        textDialog.text = string.Empty;
        textCharacter.text = string.Empty;
    }

}
