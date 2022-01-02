using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController player;

    // Speed
    private Vector3 velocity;

    private float speed = 1f;
    public float baseSpeed = 7f;
    public float walkSpeed = 14f;
    public float sprintSpeed = 18f;

    // Bullet time factor
    public float bulletTime = 0.05f;

    // Jump
    public Transform groundCheck;
    public LayerMask groundMask;

    public float gravity = -39.24f;
    public float jumpHeight = 4f;
    private bool isGrounded = false;
    public float groundDistance = 0.4f;

    private void Start()
    {
       player = GetComponent<CharacterController>();
    }
    private void Update()
    {
        // Gain velocity each frame, reset when player is on the ground
        GainVelocity();
        Move();
        BulletTime();

        // True if player's lowest point is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = sprintSpeed;
            }
            else
            {
                speed = walkSpeed;
            }

            if (velocity.y < 0)
            {
                ResetVelocity();
            }
        }
        else
        {
            speed = baseSpeed;
        }
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Move relative to the way the player is facing
        Vector3 move = transform.right * x + transform.forward * z;
        player.Move(move * speed * Time.deltaTime);
    }
    private void BulletTime()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (PauseMenu.isPaused == false)
        {
            // Slow down time if player isn't moving
            if (z == 0 && x == 0 && isGrounded)
            {
                Time.timeScale = bulletTime;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

    private void Jump()
    {
        // Set jump takeoff speed
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    private void ResetVelocity()
    {
        velocity.y = 0;
    }
    private void GainVelocity()
    {
        // Apply gravity on player
        velocity.y += gravity * Time.deltaTime;
        player.Move(velocity * Time.deltaTime);
    }
}
