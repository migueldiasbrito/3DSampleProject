using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public CharacterController controller;
    public Vector3 playerVelocity;
    public bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;

    private Vector3 move = Vector3.zero;
    private bool jump = false;

    public void UpdateMovement(InputAction.CallbackContext callbackContext)
    {
        Vector2 input = callbackContext.ReadValue<Vector2>();
        move = new Vector3(input.x, 0, input.y);
    }

    public void Jump(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && groundedPlayer)
        {
            jump = true;
        }
    }

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;

        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (jump)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            jump = false;
        }
        else if (!groundedPlayer)
        {
            playerVelocity.y += gravityValue * Time.deltaTime;
        }

        controller.Move(playerVelocity * Time.deltaTime);
    }
}