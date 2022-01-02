using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform player;

    private float mouseSensitivity = SensitivitySlider.mouseSensitivity;

    private float xRotation = 0f;

    private void Update()
    {
        // Scaling the time doesn't affect this script because it's using unscaled time
        // When the game isn't paused enable player look around and lock the cursor
        if (PauseMenu.isPaused == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            RotateAroundX();
            RotateAroundY();
        }
        // When the game is paused unlock the cursor to allow clicking buttons in pause menu
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void RotateAroundX()
    {
        // Get player rotation looking up or down unaffected by bullet time
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.unscaledDeltaTime;

        xRotation -= mouseY;
        // Restrict player looking up and down to 180 degrees
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        // Apply vertical rotation to player's camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void RotateAroundY()
    {
        // Get player rotation looking left or right unaffected by bullet time
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.unscaledDeltaTime;

        // Apply horizontal rotation to both the player and it's camera
        player.Rotate(Vector3.up * mouseX);
    }
}
