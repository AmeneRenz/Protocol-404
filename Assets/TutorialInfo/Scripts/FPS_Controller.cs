using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // NEW input system

[RequireComponent(typeof(CharacterController))]
public class FPS_Controller : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // --- Read input using the new Input System ---

        // Movement input: WASD / Arrow keys OR Gamepad left stick
        Vector2 moveInput = Vector2.zero;

        // Keyboard fallback
        if (Keyboard.current != null)
        {
            float v = 0f;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) v += 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) v -= 1f;

            float h = 0f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) h += 1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) h -= 1f;

            moveInput = new Vector2(h, v);
        }

        // Gamepad override if present (analog)
        if (Gamepad.current != null)
        {
            var gs = Gamepad.current.leftStick.ReadValue();
            // leftStick.x -> horizontal, leftStick.y -> vertical
            moveInput = new Vector2(gs.x, gs.y);
        }

        // Running (Left Shift)
        bool isRunning = false;
        if (Keyboard.current != null && Keyboard.current.leftShiftKey != null)
            isRunning = Keyboard.current.leftShiftKey.isPressed;
        if (Gamepad.current != null)
            isRunning = isRunning || Gamepad.current.leftStickButton.isPressed; // optional: press stick to "run"

        float speed = isRunning ? runningSpeed : walkingSpeed;

        // Convert input to world space
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float curSpeedX = canMove ? speed * moveInput.y : 0f; // forward/back
        float curSpeedY = canMove ? speed * moveInput.x : 0f; // left/right

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // Jump (Space)
        bool jumpPressed = false;
        if (Keyboard.current != null && Keyboard.current.spaceKey != null)
            jumpPressed = Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.spaceKey.isPressed;
        if (Gamepad.current != null)
            jumpPressed = jumpPressed || Gamepad.current.buttonSouth.wasPressedThisFrame || Gamepad.current.buttonSouth.isPressed;

        if (jumpPressed && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity only when not grounded
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Mouse / Look input
        if (canMove)
        {
            Vector2 mouseDelta = Vector2.zero;

            if (Mouse.current != null)
            {
                // Use delta (pixels since last frame). Scale down to match old GetAxis feel.
                Vector2 raw = Mouse.current.delta.ReadValue();
                mouseDelta = raw * 0.02f; // tweak multiplier if sensitivity feels off
            }
            else if (Gamepad.current != null)
            {
                // Use right stick for camera on gamepad
                Vector2 rs = Gamepad.current.rightStick.ReadValue();
                mouseDelta = rs * 10f * Time.deltaTime; // scale; tweak as needed
            }

            rotationX += -mouseDelta.y * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

            transform.rotation *= Quaternion.Euler(0, mouseDelta.x * lookSpeed, 0);
        }
    }
}
