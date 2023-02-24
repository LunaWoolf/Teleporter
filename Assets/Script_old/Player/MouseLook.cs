using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float mapMouseSensitivity = 100f;
    public float mapZoomSensitivity = 2f;
    public Transform playerBody;
    private PlayerControls PlayerControls;
    private InputAction CameraMovement;
    private InputAction MapZoom;
    float xRotation = 0f;
    public GameManager gm;
    public GameObject BigMapCamera;
    public float mapZoomInLimit = 60f;
    public float mapZoomOutLimit = 900f;



    private void Awake()
    {
        PlayerControls = new PlayerControls();

    }

    private void OnEnable()
    {
        CameraMovement = PlayerControls.PlayerAction.CameraMovement;
        CameraMovement.Enable();

        MapZoom = PlayerControls.PlayerAction.MapZoom;
        MapZoom.Enable();


    }

    private void OnDestroy()
    {
     
        CameraMovement.Disable();

        
        MapZoom.Disable();


    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

   
    void Update()
    {
      

        if (/*gm.UIPageOpen*/ true) //如果打开UI界面， 玩家无法移动，移动相机
        {
            //float mouseX = CameraMovement.ReadValue<Vector2>().x * mapMouseSensitivity * Time.deltaTime;
            


            //playerBody.Rotate(Vector3.up * mouseX); //roate playerbody


            float mouseY = MapZoom.ReadValue<Vector2>().y * mapZoomSensitivity * -1f;

            Vector3 mapZoom = transform.up * mouseY;

            //Vector3 mapZoomClamp = transform.up * Mathf.Clamp(BigMapCamera.GetComponent<Transform>().position.y + mapZoom.y, 60f, 1000f);

            if ((BigMapCamera.GetComponent<Transform>().position + mapZoom).y > mapZoomInLimit && (BigMapCamera.GetComponent<Transform>().position + mapZoom).y < mapZoomOutLimit)
            {
                BigMapCamera.GetComponent<Transform>().position += mapZoom;

            }
            
        }
        /*else if(!gm.InstructionPageOpen)//如果打开UI界面关闭， 玩家移动
        {
            float mouseX = CameraMovement.ReadValue<Vector2>().x * mouseSensitivity * Time.deltaTime;
            float mouseY = CameraMovement.ReadValue<Vector2>().y * mouseSensitivity * Time.deltaTime;



            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //rotate self(camera)
            playerBody.Rotate(Vector3.up * mouseX); //roate playerbody

        }*/


    }
}
