using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.InputSystem;
using UnityEngine.UI;


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

        //PlayerControls.PlayerAction.StartConversation.performed += ctx => NextDialogue();

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

    public SpriteRenderer TalkUIText;

    public void ChangeTalkUIColor(bool active)
    {
        if (TalkUIText != null)
        {
            if (active)
            {
                TalkUIText.color = new Color(255f, 243f, 0f);
            }
            else
            {
                TalkUIText.color = new Color(0.5188f, 0.5188f, 0.5188f);
                
            }

        }
      

    }
        
    public void DisableTalkable()
    {
        talkable = false;

    }


}
