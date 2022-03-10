using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.InputSystem;

public class NPCDialogueManager : MonoBehaviour
{
    

    private PlayerControls PlayerControls;
    private InputAction StartConversation;


    public string CurrentStartNode = "Start";

    public DialogueRunner dialogueRunner;
    public bool talkable;
    public bool startDialogue;
    public GameObject TalkUI;

    private void Awake()
    {
        PlayerControls = new PlayerControls();

    }


    private void OnEnable()
    {

        StartConversation = PlayerControls.PlayerAction.StartConversation;
        StartConversation.Enable();

        PlayerControls.PlayerAction.StartConversation.performed += ctx => NextDialogue();

    }


    public void SetDialogue()
    {


        if (dialogueRunner != null && talkable)
        {
            dialogueRunner.StartDialogue("Start");
            startDialogue = true;
        }
    }

    private void NextDialogue()
    {
        

    }


    public void ShowTalkUI(bool show)
    {
        if (show)
        {
            TalkUI.SetActive(true);
        }
        else
        {
            TalkUI.SetActive(false);

        }


    }

    public void DisableTalkable()
    {
        talkable = false;

    }

}
