using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugConsole : MonoBehaviour
{
    bool showConsole;

    string input;

    private PlayerControls PlayerControls;
    private InputAction toggleDebug;

    public PlayerMovement playerMovement;
    private void Awake()
    {
        PlayerControls = new PlayerControls();
    

    }

    private void OnEnable()
    {
        toggleDebug = PlayerControls.Debug.ToggleDebug;
        toggleDebug.Enable();

        PlayerControls.Debug.ToggleDebug.performed += ctx => OnToggleDebug();

    }

    public void OnToggleDebug()
    {

        showConsole = !showConsole;
        if (showConsole)
        {
            Cursor.lockState = CursorLockMode.None;
            playerMovement.DisableAllInput();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            playerMovement.EnableAllInput();
        }
    }

    private void OnGUI()
    {
        if (!showConsole){ return; }

        float y = 0f;

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color (0,0,0,0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20, 20), input);

    }

}
