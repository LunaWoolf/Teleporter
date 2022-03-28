﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    private PlayerControls PlayerControls;
    private InputAction movement;
    private InputAction CameraMovement;
    private InputAction uiInputMap;


    public float speed = 12f;
    public float gravity = -9.18f;
    public float jumpHeight = 3.5f;
    public bool jump;
 
    public Vector3 gravityVelocity;

    public Transform groundCheck;
    public float groundDistance = 0.5f;
  
    public bool isGrounded;
    public bool Aimming;
    public bool Teleporting;
    public int TeleportTimes = 2;
    public bool Moveable = true;

    public GameObject Phantom;
    public GameObject Brette_Phantom;

    public GameObject firstPersonCamera;
    public GameObject BigMapCamera;
    public GameObject thridPersonCamera;
    public GameManager gm;
    public PlayerInteraction playerInteraction;

    Camera cam;

    public AnimationCurve TeleportCurve;

    [SerializeField] float teleportingDistance = 5.0f;

    [Header("Transform Reference")]
    public Transform headTransform;
    public Transform cameraTransform;

    [Header("Possess")]
    public bool thirdPersonPossess = false;
    public GameObject PossessBody;
    public bool possess;
    public float possessTime = 5f;
    public Image PossessTimer;
    public GameObject AimPossessTarget;
    public Image BretteEye;
    public Color EyeColor_Ready;
    public Color EyeColor_Charge;

   [Header("Head Bob")]
    [SerializeField] bool headBob = true;
    [SerializeField] float bobFrequncy = 5f;
    [SerializeField] float bobHorizontalAmplitude = 0.1f;
    [SerializeField] float bobVerticalAmplitude = 0.1f;
    [Range(0,1)][SerializeField] public float headBobSmoothing = 0.1f;
    float walkingTime = 0;
    Vector3 targetCameraPosition;

    [Header("Post Processing")]
    public GameObject PostProcessing;
    Volume TeleportPostProcessingVolume;

    [Header("LayerMask")]
    public LayerMask groundMask;
    public LayerMask impenetrableMask;
    public LayerMask PossessebleMask;

    //___________________________________________________________________________________________________________
    [Header("TEST")]
    public GameObject Building_regular;
    public GameObject Building_transparent;
    //___________________________________________________________________________________________________________
    public VolumeProfile SpotLightProfile;
    public Color SpotLightRed;
    public Color SpotLightGreen;



    private void Awake()
    {
        PlayerControls = new PlayerControls();

        Moveable = true;


    }

    private void OnEnable() { 
   
        movement = PlayerControls.PlayerAction.Movement;
        movement.Enable();

        CameraMovement = PlayerControls.PlayerAction.CameraMovement;
        CameraMovement.Enable();

        PlayerControls.PlayerAction.Teleport.started += ctx => AimAction();
        PlayerControls.PlayerAction.Teleport.performed += ctx => TeleportAction();
        PlayerControls.PlayerAction.Teleport.Enable();

        PlayerControls.UI.OpenUIPage.performed += ctx => gm.ToggleUIPage();
        PlayerControls.UI.OpenUIPage.Enable();

        PlayerControls.PlayerAction.InstructionMenu.performed += ctx => gm.ToggleInstructionPage();
        PlayerControls.PlayerAction.InstructionMenu.Enable();

        PlayerControls.PlayerAction.AbortAimming.performed += ctx => AbortAiming();
        PlayerControls.PlayerAction.AbortAimming.Enable();

        PlayerControls.PlayerAction.Jump.performed += ctx => Jump();
        PlayerControls.PlayerAction.Jump.Enable();



        //___________________________________________________________________________________________________________


        PlayerControls.PlayerAction.SuperTeleport.started += ctx => SuperTeleport();
        PlayerControls.PlayerAction.SuperTeleport.performed += ctx => CancleSuperTeleport();
        PlayerControls.PlayerAction.SuperTeleport.Enable();


        //___________________________________________________________________________________________________________
    }

    private void OnDestroy()
    {

        
        movement.Disable();
        CameraMovement.Disable();

        PlayerControls.PlayerAction.Teleport.started -= ctx => AimAction();
        PlayerControls.PlayerAction.Teleport.performed -= ctx => TeleportAction();
        PlayerControls.PlayerAction.Teleport.Disable();

        PlayerControls.UI.OpenUIPage.performed -= ctx => gm.ToggleUIPage();
        PlayerControls.UI.OpenUIPage.Disable();

        PlayerControls.PlayerAction.InstructionMenu.performed -= ctx => gm.ToggleInstructionPage();
        PlayerControls.PlayerAction.InstructionMenu.Disable();

        PlayerControls.PlayerAction.AbortAimming.performed -= ctx => AbortAiming();
        PlayerControls.PlayerAction.AbortAimming.Disable();

        PlayerControls.PlayerAction.Jump.performed -= ctx => Jump();
        PlayerControls.PlayerAction.Jump.Disable();



        //___________________________________________________________________________________________________________


        PlayerControls.PlayerAction.SuperTeleport.started -= ctx => SuperTeleport();
        PlayerControls.PlayerAction.SuperTeleport.performed -= ctx => CancleSuperTeleport();
        PlayerControls.PlayerAction.SuperTeleport.Disable();


        //___________________________________________________________________________________________________________
    }

    public Material TransparentMaterial;
    public Material[] orginalMaterials;
    public GameObject SuperTeleportObject;
    //___________________________________________________________________________________________________________
    void SuperTeleport()
    {
      
        Vector3 phantomTargetPosition = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, teleportingDistance));
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, phantomTargetPosition - this.transform.position, out hit, teleportingDistance, impenetrableMask))
        {
           
            SuperTeleportObject = hit.transform.gameObject;
            orginalMaterials = SuperTeleportObject.GetComponentInChildren<MeshRenderer>().materials;

            SuperTeleportObject.GetComponentInChildren<MeshRenderer>().materials[0] = TransparentMaterial;

            SuperTeleportObject.GetComponent<MeshRenderer>().materials[0] = TransparentMaterial;

            Material[] tMaterials = new Material[hit.transform.gameObject.GetComponentInChildren<MeshRenderer>().materials.Length];


            for (int i = 0; i < tMaterials.Length; i++)
            {
                tMaterials[i] = TransparentMaterial;
                
            }

            SuperTeleportObject.GetComponentInChildren<MeshRenderer>().materials = tMaterials;
            SuperTeleportObject.GetComponent<MeshRenderer>().materials = tMaterials;


        }

        
    }

    void CancleSuperTeleport()
    {
        if (SuperTeleportObject != null && orginalMaterials.Length > 0)
        {
            SuperTeleportObject.GetComponentInChildren<MeshRenderer>().materials = orginalMaterials;
        }
        
    }    
    //___________________________________________________________________________________________________________

    void Start()
    {
        Debug.Log("LoadStart");
        cam = firstPersonCamera.GetComponent<Camera>();
        TeleportPostProcessingVolume = PostProcessing.GetComponent<Volume>();
        if (gm == null)
        {
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        if (SpotLightProfile.TryGet<ColorAdjustments>(out var c))
        {
            c.colorFilter.value = SpotLightRed;
        }

    }

    bool fallHurt = false;
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position,groundDistance,groundMask);


        if (isGrounded)
        {
            gravityVelocity.y = -2f;
            if (fallHurt)
            {/*
                gm.UpdateHealth(-5f);
                gm.CameraShake();
                fallHurt = false;*/
            }

         
        }
        else
        {
            if (!Teleporting && !jump)
            {
                gravityVelocity.y += gravity * Time.deltaTime;

                controller.Move(gravityVelocity * Time.deltaTime);

                if (gravityVelocity.y < -20f)
                {
                    fallHurt = true;
                }

            }
           

        }

        if (!Teleporting && Moveable && !Aimming && !playerInteraction.inTheMiddleOfConversation)
        {
            if (gm.UIPageOpen) //如果打开UI界面， 玩家无法移动，移动相机
            {
                float x = movement.ReadValue<Vector2>().x;
                float z = movement.ReadValue<Vector2>().y;

                Vector3 move = transform.right * x + transform.forward * z;

                BigMapCamera.GetComponent<Transform>().position += move;

            }
            else //如果打开UI界面关闭， 玩家移动
            {
                float x = movement.ReadValue<Vector2>().x;
                float z = movement.ReadValue<Vector2>().y;



                Vector3 move = transform.right * x + transform.forward * z;

              

                controller.Move(move * speed * Time.deltaTime);

                Vector3 jumpMove = new Vector3(0,0,0);

                if (jump && isGrounded && !Aimming)
                {

                    jumpMove= transform.up * Mathf.Sqrt(jumpHeight * gravity * -2f) ;
                    jump = false;

                }

                controller.Move(jumpMove * Time.deltaTime);


                if (possess && PossessBody != null)
                {
                    PossessBody.transform.position += move * speed * Time.deltaTime;
                }

                if (isGrounded && headBob && Mathf.Abs(x) + Mathf.Abs(z) > 0.1f)
                {
                    walkingTime += Time.deltaTime;
                    targetCameraPosition = headTransform.position + CalculateHeadBobOffset(walkingTime);
                    cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetCameraPosition, headBobSmoothing);

                    if ((cameraTransform.position - targetCameraPosition).magnitude <= 0.001f)
                        cameraTransform.position = targetCameraPosition;

                }
                else
                {
                    walkingTime = 0;
                }

            }

            
        }

        if (Aimming && !playerInteraction.inTheMiddleOfConversation) //实时更新Aiming 位置
        {
            CheckAimming();
        }

    }

    void Jump()
    {
        if (isGrounded)
        {
            jump = true;

        }
    }

    void AbortAiming()
    {

        if (Aimming) 
        {
            Aimming = false;
            TeleportPostProcessingVolume.weight = 0;
            Teleporting = false;
            Phantom.GetComponent<MeshRenderer>().enabled = false;
            Brette_Phantom.SetActive(false);
            this.GetComponent<CharacterController>().enabled = true;

            gm.ToggleUIInstruction("Teleport", false);
        }
    }


    void AimAction()
    {
        if (!Teleporting && TeleportTimes > 0 && !playerInteraction.inTheMiddleOfConversation)
        {
            Aimming = true;
            CheckAimming();
        }

        gm.ToggleUIInstruction("Aim",false);
        gm.ToggleUIInstruction("Teleport", true);
    }

    void TeleportAction()
    {
        gm.ToggleUIInstruction("Teleport",false);

        if (Aimming)
        {
            Aimming = false;
            if (CheckPossess() == null)
            {
                StartCoroutine(Teleport(0f, Phantom.transform));
            }
            else
            {
                StartCoroutine(Possess(0f, CheckPossess().transform));
            }

        }
       
        
    }

    public GameObject CheckPossess()
    {
        bool possessTargetExist = false;
        Vector3 phantomTargetPosition = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, teleportingDistance));
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, phantomTargetPosition - this.transform.position, out hit, teleportingDistance, PossessebleMask))
        {

            GameObject possessTarget = hit.transform.gameObject;
            possessTargetExist = true;
            return hit.transform.gameObject;
        }
        return null;

    }

    //public GameObject Testxxxx;

    Vector3 phantomTargetPosition;
    float targetDistance = 15f;
    void CheckAimming()
    {
        if (Aimming)
        {

            //Testxxxx.SetActive(true);
            float z = movement.ReadValue<Vector2>().y * speed * Time.deltaTime;


            targetDistance = Mathf.Clamp(targetDistance + z,0, teleportingDistance);



            bool possessTargetExist = false;
            //PostProcessing Effect 
            LensDistortion _LensDistortion;
            TeleportPostProcessingVolume.profile.TryGet<LensDistortion>(out _LensDistortion);
            _LensDistortion.intensity.value = 0f;
            TeleportPostProcessingVolume.weight = 1;

            if (cam == null)
            {
                cam = firstPersonCamera.GetComponent<Camera>();
            }else
            { 

           


            phantomTargetPosition = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, targetDistance));
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, phantomTargetPosition - this.transform.position, out hit, teleportingDistance, PossessebleMask))
            {
                if (AimPossessTarget!= null && AimPossessTarget == hit.transform.gameObject)
                {
                    //do nothing
                }
                else
                {
                    if (AimPossessTarget != null && AimPossessTarget.TryGetComponent<PossessNPC>(out PossessNPC pNPC_old))
                    {
                        pNPC_old.ChangeMaterial(false);
                    }
                    AimPossessTarget = hit.transform.gameObject;

                    if (AimPossessTarget.TryGetComponent<PossessNPC>(out PossessNPC pNPC_new))
                    {
                        pNPC_new.ChangeMaterial(true);
                    }

                }
                possessTargetExist = true;
            }
            else if (Physics.Raycast(this.transform.position, phantomTargetPosition - this.transform.position, out hit, teleportingDistance, impenetrableMask))
            {
                phantomTargetPosition = Vector3.Lerp(transform.position, hit.point, 0.8f);
                if (AimPossessTarget != null)
                {
                    if (AimPossessTarget.TryGetComponent<PossessNPC>(out PossessNPC pNPC_old))
                    {
                        pNPC_old.ChangeMaterial(false);
                    }
                    AimPossessTarget = null;
                }
            }
            else
            {
                if (AimPossessTarget != null)
                {
                    if (AimPossessTarget.TryGetComponent<PossessNPC>(out PossessNPC pNPC_old))
                    {
                        pNPC_old.ChangeMaterial(false);
                    }
                    AimPossessTarget = null;
                }
            }
            }


            Phantom.GetComponent<MeshRenderer>().enabled = true; //make phantom visible
            Brette_Phantom.SetActive(true);
            Phantom.transform.position = phantomTargetPosition; //transform it into correct position
        }

    }

    IEnumerator Teleport(float delayTime, Transform teleportPosition)
    {
        //Testxxxx.SetActive(false);
        TeleportTimes--;

        if (TeleportTimes < 1)
        {
            BretteEye.color = EyeColor_Charge;
        }

        if (possess)
        {
            Moveable = true;
            PossessTimer.enabled = false;
            EjectPossess();
        }

        if (AimPossessTarget != null)
        {
            if (AimPossessTarget.TryGetComponent<PossessNPC>(out PossessNPC pNPC_old))
            {
                pNPC_old.ChangeMaterial(false);
            }
            AimPossessTarget = null;
        }

        Teleporting = true;

        //PostProcessing Effect 
        LensDistortion _LensDistortion;
        TeleportPostProcessingVolume.profile.TryGet<LensDistortion>(out _LensDistortion);
        _LensDistortion.intensity.value = -0.55f;


        this.GetComponent<CharacterController>().enabled = false;
        yield return new WaitForSeconds(delayTime); 
        float startTime = Time.time; 
        while (Time.time - startTime <= 1)
        { 
            transform.position = Vector3.Lerp(transform.position,teleportPosition.position, TeleportCurve.Evaluate(Time.time - startTime));
            yield return 1; 
        }
        StartCoroutine(Float(1f));

        StartCoroutine(RestoreTeleportTimes());
    }

    IEnumerator Float(float floatTime)
    {
        yield return new WaitForSeconds(0.2f);

        //PostProcessing Effect 
        TeleportPostProcessingVolume.weight = 0;

        Teleporting = false;
        gravity = -3f;
        Phantom.GetComponent<MeshRenderer>().enabled = false;
        Brette_Phantom.SetActive(false);
        this.GetComponent<CharacterController>().enabled = true;
        gravityVelocity.y = -2f;
        yield return new WaitForSeconds(floatTime);
        gravity = -9.18f;
    }

    IEnumerator RestoreTeleportTimes()
    {
        yield return new WaitForSeconds(5f);

        if (TeleportTimes < 2)
        {
            TeleportTimes++;
        }

        if (TeleportTimes > 0)
        {
            BretteEye.color = EyeColor_Ready;
        }
    }

   


    IEnumerator Possess(float delayTime, Transform teleportPosition)
    {
        Teleporting = true;
      

        //PostProcessing Effect 
        LensDistortion _LensDistortion;
        TeleportPostProcessingVolume.profile.TryGet<LensDistortion>(out _LensDistortion);
        _LensDistortion.intensity.value = -0.55f;

       

        this.GetComponent<CharacterController>().enabled = false;
        yield return new WaitForSeconds(delayTime);
        float startTime = Time.time;
        while (Time.time - startTime <= 0.8f)
        {
            transform.position = Vector3.Lerp(transform.position, teleportPosition.position, TeleportCurve.Evaluate(Time.time - startTime));
            yield return 1;
        }

        possess = true;
        PossessBody = teleportPosition.gameObject;
        TeleportPostProcessingVolume.weight = 0;
        cam.cullingMask ^= 1 << LayerMask.NameToLayer("PlayerEye");
        cam.cullingMask ^= 1 << LayerMask.NameToLayer("GuardEye");

        if (SpotLightProfile.TryGet<ColorAdjustments>(out var c))
        {
            c.colorFilter.value = SpotLightGreen;
        }


        Teleporting = false;
        gravity = -3f;
        this.GetComponent<CharacterController>().enabled = true;

        Phantom.GetComponent<MeshRenderer>().enabled = false;
        Brette_Phantom.SetActive(false);
        

        if (AimPossessTarget != null)
        {
            if (AimPossessTarget.TryGetComponent<PossessNPC>(out PossessNPC pNPC_old))
            {
                pNPC_old.ChangeMaterial(false);
            }
            AimPossessTarget = null;
        }

        if (thirdPersonPossess) // Thirdperson mode
        {
            firstPersonCamera.SetActive(false);
            thridPersonCamera.SetActive(true);
        }
        else
        {
            PossessBody.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            
        }

        //UI
        StartCoroutine(UpdatePossessUI(possessTime));


    }

    IEnumerator UpdatePossessUI(float possessTime)
    {
        if(PossessTimer != null)
            PossessTimer.enabled = true;

        while (PossessTimer != null && possessTime > 0)
        {
            possessTime -= 0.1f;
            PossessTimer.fillAmount = possessTime / 5f;
            yield return new WaitForSeconds(0.1f);
        }

        PossessTimer.enabled = false;
        if (possess)
        {
            EndPossess();
        }
        
    }

    public void EndPossess()
    {
        Moveable = false;
        PossessBody.GetComponent<CapsuleCollider>().enabled = false;
        //Wait for eject;
    }

    public void EjectPossess()
    {
        
        cam.cullingMask ^= 1 << LayerMask.NameToLayer("PlayerEye");
        cam.cullingMask ^= 1 << LayerMask.NameToLayer("GuardEye");


        if (SpotLightProfile.TryGet<ColorAdjustments>(out var c))
        {
            c.colorFilter.value = SpotLightRed;
        }


        if (thirdPersonPossess) // Thirdperson mode
        {
            firstPersonCamera.SetActive(true);
            thridPersonCamera.SetActive(false);
        }
        else
        {
            PossessBody.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
            PossessBody.GetComponent<CapsuleCollider>().enabled = true;
        }
        possess = false;
        PossessBody = null;
        Moveable = true;

    }

    Vector3 CalculateHeadBobOffset(float t)
    {
        float hoffset = 0;
        float voffset = 0;
        Vector3 offset = Vector3.zero;
        if (t > 0)
        {
            hoffset = Mathf.Cos(t * bobFrequncy) * bobHorizontalAmplitude;
            voffset = Mathf.Sin(t * bobFrequncy) * bobVerticalAmplitude;
            offset = Vector3.right * hoffset + Vector3.up * voffset;

        }
        return offset;

    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Light" && !possess)
        {
            gm.inLight = true;

        }

        /*if (other.gameObject.tag == "XRayInstruction")
       {
           gm.ToggleUIInstruction("Xray", true);

       }
       */

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Light")
        {
            gm.inLight = false;

        }

        /*if (other.gameObject.tag == "XRayInstruction")
        {
            gm.ToggleUIInstruction("Xray", false);

        }*/

    }

    public void DisableAllInput()
    {
        PlayerControls.PlayerAction.Disable();
    }

    public void EnableAllInput()
    {
        PlayerControls.PlayerAction.Enable();
    }

    public void RepositionBigMapCamera()
    {
       
        BigMapCamera.GetComponent<Transform>().position = new Vector3(0, 500, 0);
        
    }
     

}
