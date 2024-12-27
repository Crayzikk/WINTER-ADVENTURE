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
