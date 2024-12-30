using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    [SerializeField] private float senseX;
    [SerializeField] private float senseY;
    [SerializeField] private Transform player;
    [SerializeField] private float smoothness;

    private float rotateY;
    private float rotateX;
    private float currentXRotation;
    private float currentYRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MoveCamera();

        if(Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(transform.position, transform.forward);

            if(Physics.Raycast(ray, out RaycastHit hit, 2f))
            {
                if(hit.collider.CompareTag("Door"))
                {
                    hit.collider.GetComponent<DoorController>().OpenOrCloseDoor();
                }
                else if(hit.collider.CompareTag("Character"))
                {
                    Character character = hit.collider.GetComponent<Character>();
                    
                    if(character.Idialog && !character.IdialogPlayer)
                    {
                        DialogController.dialogStart = true;                    
                        
                        if(character.IdialogContinue)
                        {
                            DialogController.dialogIsContinue = true;
                            DialogController.countDialogPhrases = character.countPhrases;
                        }
                    }
                    
                    character.IdialogPlayer = true;
                }
                else if(hit.collider.CompareTag("Chrismas"))
                {
                    PlotControllerSceneOne.countDecorationActive++;
                    hit.collider.GetComponent<DecorationHouse>().ShowChrismas();
                }
                else if(hit.collider.CompareTag("ChrismasBox"))
                {
                    PlotControllerSceneOne.playerTakeTheBox = true;
                    hit.collider.GetComponent<DecorationHouse>().ShowChrismas();
                }
            }
        }
    }

    private void MoveCamera()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * senseX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * senseY;
        
        rotateY += mouseX;
        rotateX -= mouseY;

        rotateX = Mathf.Clamp(rotateX, -90f, 90f);

        currentXRotation = Mathf.LerpAngle(currentXRotation, rotateX, smoothness);
        currentYRotation = Mathf.LerpAngle(currentYRotation, rotateY, smoothness);

        transform.rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);
        player.rotation = Quaternion.Euler(0, currentYRotation, 0);
    }
}
