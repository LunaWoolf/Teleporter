using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerInteraction : MonoBehaviour
{
    

    public DialogueRunner dialogueRunner;
    public GameObject dialogueManager;
    private PlayerControls PlayerControls;

    public EventSystem eventSystem;
    public GameObject Canvas;
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    private InputAction NextLine;
    private InputAction OpenBigMap;

    public GameObject bigMap;

    //___________________________________________________________________________________________________________
    /*[Header("TEST")]
    public GameObject CG_1;
    public GameObject CG_2;
    public TMP_Text text;
    public TMP_FontAsset FontAssetA;
    public TMP_FontAsset FontAssetB;*/
    //___________________________________________________________________________________________________________



    private void Awake()
    {
        PlayerControls = new PlayerControls();

    }

    private void OnEnable()
    {
        NextLine = PlayerControls.PlayerAction.NextLine;
        NextLine.Enable();
        OpenBigMap = PlayerControls.PlayerAction.OpenBigMap;
        OpenBigMap.Enable();

        PlayerControls.PlayerAction.NextLine.performed += ctx => NextDialogue();
        PlayerControls.PlayerAction.OpenBigMap.performed += ctx => OpenMap();


    }

    void Start()
    {
        
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = Canvas.GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = eventSystem.GetComponent<EventSystem>();
        
    }



  
    void Update()
    {
       
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<Button>() != null)
            {
                result.gameObject.GetComponent<Button>().Select();
                //result.gameObject.GetComponent<Button>().OnSelect(null);
                //EventSystem.current.SetSelectedGameObject(result.gameObject.GetComponentInChildren<Button>().gameObject, null);
                if (Input.GetMouseButton(0))
                {
                    result.gameObject.GetComponent<Button>().onClick.Invoke();
                }

            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("NPC");
        if (other.gameObject.tag == "NPC")
        {
            if (other.gameObject.TryGetComponent(out NPC npcScript))
            {
                npcScript.SetDialogue();
            }
            
        }

    }

    private void NextDialogue()
    {
        dialogueManager.GetComponent<DialogueUI>().MarkLineComplete();
        //___________________________________________________________________________________________________________
        /*CG_1.SetActive(!CG_1.activeSelf);
        CG_2.SetActive(!CG_2.activeSelf);
        if (text.font == FontAssetA)
        {
            text.font = FontAssetB;
        }
        else
        {
            text.font = FontAssetA;
        }*/
   

    //___________________________________________________________________________________________________________

    }


    private void OpenMap()
    {
        if (bigMap != null)
        {
            bigMap.SetActive(!bigMap.activeSelf);

        }
    }

    public void DisableAllInput()
    {
        PlayerControls.PlayerAction.Disable();
    }


    public void EnableAllInput()
    {
        PlayerControls.PlayerAction.Enable();
    }



}
