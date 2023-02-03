using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class MouseLookAt : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public float lookUpClampAngle = 80.0f;
    public float lookDownClampAngle = 80.0f;

    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis
    bool isUpdatingRotation = true;
    [SerializeField] bool allowXRotation;
    [SerializeField] bool allowYRotation;
    [SerializeField] bool rotatePlayerBody;
    [SerializeField] GameObject ControlledPlayer;
    private void Awake()
    {
        //this.GetComponent<CmBlendFinishedNotifier>().OnBlendFinished.AddListener(StartUpdateRotation);
        //ApplicationManager.instance.OnCameraSwitched.AddListener(StopUpdateRotation);

    }
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        if (rotatePlayerBody)
            rot = ControlledPlayer.GetComponent<Transform>().localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        isUpdatingRotation = true;

    }

    public void StartUpdateRotation()
    {
        isUpdatingRotation = true;
        Vector3 rot = transform.localRotation.eulerAngles;
        if (rotatePlayerBody)
            rot = ControlledPlayer.GetComponent<Transform>().localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }
    public void StopUpdateRotation() { isUpdatingRotation = false; }

    void Update()
    {
        if (!isUpdatingRotation) return;
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");

            if (allowYRotation)
                rotY += mouseX * mouseSensitivity * Time.deltaTime;
            if (allowXRotation)
                rotX += mouseY * mouseSensitivity * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, -lookUpClampAngle, lookDownClampAngle);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);

            if (rotatePlayerBody)
                ControlledPlayer.GetComponent<Transform>().rotation = Quaternion.Euler(0.0f, rotY, 0.0f);


            transform.rotation = localRotation;
        }
    }
}