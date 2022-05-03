using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    private Vector3 Velocity;
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRotation;
    private float speedMultiplier = 1.0f;

    private AnimationManager animManager;
    private CharacterController controller;
    [SerializeField] private Transform cam;
    
    [Space]
    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float sprintSpeedMultiplier;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float gravity = -18f;

    private void Awake()
    {
        animManager = GetComponent<AnimationManager>();
        controller = GetComponentInParent<CharacterController>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        if(PlayerMovementInput != Vector3.zero)
        {
            MovePlayer();
        }
        if (PlayerMovementInput == Vector3.zero) animManager.StopAnimation("Walk");

        MoveCamera();
    }
    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput);

        if (controller.isGrounded)
        {
            Velocity.y = -1f;

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                speedMultiplier = sprintSpeedMultiplier;
                animManager.SetSprintAnimation(true, speedMultiplier);
            }
        }
        else
        {
            Velocity.y += gravity * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedMultiplier = 1.0f;
            animManager.SetSprintAnimation(false, speedMultiplier);
        }

        controller.Move(MoveVector * movementSpeed * speedMultiplier * Time.deltaTime);
        controller.Move(Velocity * Time.deltaTime);

        animManager.PlayAnimation("Walk");
    }
    private void MoveCamera()
    {
        xRotation -= PlayerMouseInput.y * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -75f, 75f);

        transform.Rotate(0f, PlayerMouseInput.x * mouseSensitivity, 0f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}