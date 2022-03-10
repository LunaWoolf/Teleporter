using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public RandomDialogue[] RandomDialogueList;
    public LineView lineView;
    private PlayerControls PlayerControls;

    private InputAction StartConversation;

    public TextLineProvider textLineProvider;


    private void Awake()
    {
        PlayerControls = new PlayerControls();
        if(PresistenceManagerScript.Instance != null)
            textLineProvider.textLanguageCode = PresistenceManagerScript.Instance.Language;

    }

    void Update()
    {
        
    }

    private void OnEnable()
    {


        StartConversation = PlayerControls.PlayerAction.StartConversation;
        StartConversation.Enable();

        PlayerControls.PlayerAction.StartConversation.performed += ctx => NextDialogue();


    }

    private void NextDialogue()
    {
        Debug.Log("click");

        lineView.OnContinueClicked();

    }



}
