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
    private InputAction StartConversation;

    public GameObject bigMap;

    public GameObject Monologue_Text;

    public Camera cam;



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
        StartConversation = PlayerControls.PlayerAction.StartConversation;
        StartConversation.Enable();

        PlayerControls.PlayerAction.NextLine.performed += ctx => NextDialogue();
        PlayerControls.PlayerAction.OpenBigMap.performed += ctx => OpenMap();
        PlayerControls.PlayerAction.StartConversation.performed += ctx => StartNPCConversation();

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

                if (Input.GetMouseButton(0))
                {
                    result.gameObject.GetComponent<Button>().onClick.Invoke();
                }

            }
        }


        Vector3 InteractRay;
        InteractRay = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 15));
        Debug.DrawRay(transform.position, InteractRay - this.transform.position, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, InteractRay - this.transform.position, out hit))
        {
            if (hit.transform.gameObject.tag == "NPC")
            {
                GameObject curNPC = hit.transform.gameObject;

                PossessNPC npcScript = curNPC.GetComponent<PossessNPC>();
                
                if (npcScript != null && npcScript.talkable)
                {
                    if (conversationNPC == null)
                    {
                        conversationNPC = hit.transform.gameObject;
                        npcScript.ShowTalkUI(true);
                    }
                    else if (hit.transform.gameObject != conversationNPC)
                    {
                        conversationNPC = hit.transform.gameObject;
                        npcScript.ShowTalkUI(true);
                    }
                    
                }
            }

            if (hit.transform.gameObject == null || (conversationNPC != null && hit.transform.gameObject != conversationNPC))
            {
                if (conversationNPC.TryGetComponent<PossessNPC>(out PossessNPC NPCscript))
                {
                  
                    NPCscript.ShowTalkUI(false);

                }

                conversationNPC = null;
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

    public void EndDialogue()
    {
        inTheMiddleOfConversation = false;
        Monologue_Text.SetActive(true);
    }

    public GameObject conversationNPC;
    public bool inTheMiddleOfConversation = false;

    public void StartNPCConversation()
    {
        if (!inTheMiddleOfConversation)
        {
            if (conversationNPC != null && conversationNPC.TryGetComponent<PossessNPC>(out PossessNPC npcScript))
            {
                
                npcScript.SetDialogue();
                npcScript.ShowTalkUI(false);
                inTheMiddleOfConversation = true;
                Monologue_Text.SetActive(false);
            }

        }
        
    }




}
