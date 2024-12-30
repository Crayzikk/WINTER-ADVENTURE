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

        if (isJumping && groundCheck.checkGroundPlayer)
            JumpPlayer();
    }

    private void RunPlayer()
    {
        float targetSpeed = isRunning ? speedPlayerInRunning : speedPlayer;
        float speed = Mathf.Lerp(rigidbodyPlayer.linearVelocity.magnitude, targetSpeed, 4f * Time.deltaTime);

        RaycastHit hit;
        Vector3 moveDirection = (transform.forward * verticalInput + transform.right * horizontalInput).normalized;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
        {
            Vector3 surfaceNormal = hit.normal;
            float slopeAngle = Vector3.Angle(Vector3.up, surfaceNormal);

            float maxSlopeAngle = 45f; 
            if (slopeAngle > maxSlopeAngle)
            {
                moveDirection = Vector3.zero; 
            }
        }

        Vector3 velocity = moveDirection * speed;
        velocity.y = rigidbodyPlayer.linearVelocity.y;

        float maxSpeed = isRunning ? speedPlayerInRunning : speedPlayer;
        Vector3 clampedVelocity = Vector3.ClampMagnitude(new Vector3(velocity.x, 0, velocity.z), maxSpeed);
        velocity.x = clampedVelocity.x;
        velocity.z = clampedVelocity.z;

        rigidbodyPlayer.linearVelocity = velocity;
    }

    private void JumpPlayer()
    {
        rigidbodyPlayer.linearVelocity = new Vector3(rigidbodyPlayer.linearVelocity.x, 0, rigidbodyPlayer.linearVelocity.z);
        rigidbodyPlayer.AddForce(Vector3.up * jumpForcePlayer, ForceMode.Impulse);

        groundCheck.checkGroundPlayer = false;
    }
}
