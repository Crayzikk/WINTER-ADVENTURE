using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speedPlayer;
    [SerializeField] private float speedPlayerInRunning;
    [SerializeField] private float jumpForcePlayer;
    [SerializeField] private GroundCheck groundCheck;

    private Rigidbody rigidbodyPlayer;
    private float horizontalInput = 0f;
    private float verticalInput = 0f;
    private float jumpInput = 0f;
    
    private bool isJumping;
    private bool isRunning;

    void Start()
    {
        rigidbodyPlayer = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        jumpInput = Input.GetAxisRaw("Jump");

        isJumping = jumpInput > 0;
        isRunning = Input.GetKey(KeyCode.LeftShift);
    }

    void FixedUpdate()
    {
        RunPlayer();

        if(isJumping && groundCheck.checkGroundPlayer)
            JumpPlayer();
    }

    private void RunPlayer()
    {
        float targetSpeed = 0f;

        if(isRunning && groundCheck.checkGroundPlayer)
        {
            targetSpeed = speedPlayerInRunning;    
        }
        else
        {
            targetSpeed = speedPlayer;
        }

        float speed = Mathf.Lerp(rigidbodyPlayer.linearVelocity.magnitude, targetSpeed, 4f * Time.deltaTime);

        Vector3 moveDirection = (transform.forward * verticalInput + transform.right * horizontalInput).normalized;
        Vector3 velocity = moveDirection * speed;
        velocity.y = rigidbodyPlayer.linearVelocity.y;

        rigidbodyPlayer.linearVelocity = velocity;
    }

    private void JumpPlayer()
    {
        rigidbodyPlayer.linearVelocity = new Vector3(rigidbodyPlayer.linearVelocity.x, 0, rigidbodyPlayer.linearVelocity.z);
        rigidbodyPlayer.AddForce(Vector3.up * jumpForcePlayer, ForceMode.Impulse);

        groundCheck.checkGroundPlayer = false;
    }
}
