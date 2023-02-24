using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoSingleton<UIManager>
{

    public GameObject healthUi;
    public TextMeshProUGUI healthText;
    public Slider timeSlider;

    public GameObject UICanvas;
    public GameObject BigMapCamera;
    public bool UIPageOpen;

    [Header("UI Instruction")]
    public GameObject Aim;
    public GameObject Teleport;
    public GameObject Possess;
    public GameObject Xray;

    public EventSystem eventSystem;

    // Start is called before the first frame update
    void Start()
    {
        if (healthText != null)
        {
            healthText = healthUi.GetComponent<TextMeshProUGUI>();
            //healthText.text = " " + playerHealth + " % ";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void UpdateHealth(float change)
    {
        playerHealth = Mathf.Clamp(playerHealth + change, 0, 100);


        if (timeSlider != null)
            timeSlider.value = playerHealth;

        if (playerHealth <= 0 && sceneManager != null)
        {
            sceneManager.LoadLevel("DeadScene");

        }
    }*/

    /* public void ToggleUIPage()
     {


         if (UICanvas != null)
         {

             UICanvas.SetActive(!UICanvas.activeSelf);
             UIPageOpen = UICanvas.activeSelf;
             //BigMapCamera.GetComponent<Transform>().position = new Vector3(0, 700, 0);

             if (!UIPageOpen)
             {
                 BigMapCamera.GetComponent<Transform>().position = new Vector3(0, 700, 0);
                 //Player.GetComponent<PlayerMovement>().RepositionBigMapCamera();
             }

             if (UIPageOpen)
             {
                 foreach (GameObject t in InGameTargetUI)
                 {
                     t.SetActive(false);
                 }
             }

             if (!UIPageOpen)
             {
                 foreach (GameObject t in InGameTargetUI)
                 {
                     t.SetActive(true);
                 }
             }


         }

     }*/

    /*

    public List<GameObject> InGameTargetUI = new List<GameObject>();


    public GameObject InstructionCanvas;
    public bool InstructionPageOpen = false;
    public void ToggleInstructionPage()
    {


        if (InstructionCanvas != null)
        {
            //Debug.Log("Inr");
            InstructionCanvas.SetActive(!InstructionCanvas.activeSelf);

            if (InstructionCanvas.activeSelf)
            {

                InstructionPageOpen = true;
                if (pms != null)
                    pms.pause = true;

            }
            else
            {
                InstructionPageOpen = false;
                if (pms != null)
                    pms.pause = false;
            }


        }


    }

 
    public void ToggleUIInstruction(string ui, bool on)
    {
        switch (ui)
        {
            case "Aim":
                Aim.SetActive(on);
                break;
            case "Teleport":
                Teleport.SetActive(on);
                break;
            case "Possess":
                Possess.SetActive(on);
                break;
            case "Xray":
                Xray.SetActive(on);
                break;


        }


    }


    [YarnCommand("MapQuestObject")]
    public void ToggleQuestSign(string questName, bool on)
    {

        QuestSignDictionary[questName].SetActive(on);


    }

    public GameObject Dialoguebox;
    public void ToogleDialogueUI()
    {
        //Dialoguebox.SetActive(!Dialoguebox.activeSelf);

    }*/

}
