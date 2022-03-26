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

    void OnDisable()
    {
        DisableTalkable();
    }

    public void SetDialogue()
    {

        if (dialogueRunner != null && talkable)
        {
   
            dialogueRunner.StartDialogue("Start");
            startDialogue = true;
        }
    }

    public void PlayerExitBound()
    {
        if (dialogueRunner != null)
        {
            Debug.Log("Exit");

           
            dialogueRunner.Stop();
            //dialogueRunner.lineProvider.gameObject.SetActive(false);
            dialogueRunner.dialogueViews[0].gameObject.transform.parent.gameObject.SetActive(false);
          
            //dialogueRunner.Clear();
        }


    }


    private void NextDialogue()
    {
        

    }


    public void ShowTalkUI(bool show)
    {
        Debug.Log("NPC3");

        if (show)
        {
            Debug.Log("NPC4");
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
