using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MilkShake;
using TMPro;
using Yarn.Unity;
using UnityEngine.SceneManagement;

[System.Serializable]
public class QuestManager : MonoBehaviour
{

    private PlayerControls PlayerControls;

    public List<Quest> questList = new List<Quest>();
    public GameObject QuestPanel;
    public GameObject QuestPrefab;


    public List<Quest> inventoryList = new List<Quest>();


    public GameObject UICanvas;
    public bool UIPageOpen;

    public DialogueRunner MainDialogueRunner;

    PresistenceManagerScript pms;

 
    void Awake()
    {
        PlayerControls = new PlayerControls();


    }

 
    private void OnEnable()
    {

        /*PlayerControls.UI.OpenUIPage.performed += ctx =>ToggleUIPage();
        PlayerControls.UI.OpenUIPage.Enable();

        PlayerControls.PlayerAction.InstructionMenu.performed += ctx => ToggleInstructionPage();
        PlayerControls.PlayerAction.InstructionMenu.Enable();*/

    }

    private void OnDestroy()
    {

        PlayerControls.UI.OpenUIPage.performed -= ctx => ToggleUIPage();
        PlayerControls.UI.OpenUIPage.Disable();

        PlayerControls.PlayerAction.InstructionMenu.performed -= ctx => ToggleInstructionPage();
        PlayerControls.PlayerAction.InstructionMenu.Disable();

    }


    [YarnCommand("InputSystemEnable")]
    public void InputSystemEnable(string input)
    {
        switch (input)
        {
            case "OpenUIPage":
                PlayerControls.UI.OpenUIPage.performed += ctx => ToggleUIPage();
                PlayerControls.UI.OpenUIPage.Enable();
                break;
            case "InstructionMenu":
                PlayerControls.PlayerAction.InstructionMenu.performed += ctx => ToggleInstructionPage();
                PlayerControls.PlayerAction.InstructionMenu.Enable();
                break;
            default:
                break;

        }

    }


    [YarnCommand("CreateQuest_Q")]
    public void CreateQuest(string id, string title, string description)
    {

        title = title.Replace('/', ' ');
        description = description.Replace('/', ' ');



        Quest questNew = new Quest(id, title, description);

        GameObject questObject = Instantiate(QuestPrefab);

        questNew.questObject = questObject;

        questObject.GetComponent<QuestObject>().SetQuest(new Quest(id, title, description));

        questObject.transform.SetParent(QuestPanel.transform, false);

        questList.Add(questNew);

        QuestUI.GetComponentInChildren<QuestObject>().SetQuest(questNew);
        PlayAddQuestAnimation();

    }

    public GameObject QuestUI;
    public TextMeshProUGUI QuestUI_Title;
    public TextMeshProUGUI QuestUI_Description;
    public void PlayAddQuestAnimation()
    {
        QuestUI.GetComponent<Animator>().SetTrigger("appear");
    }



    public GameObject CompleteQuestUI;
    public TextMeshProUGUI Complete_Title;
    public TextMeshProUGUI Complete_Description;
    [YarnCommand("CompleteQuest_Q")]
    public void CompleteQuest(string id)
    {
        foreach (Quest q in questList)
        {
            if (q.id == id)
            {
                q.questObject.SetActive(false);

                PlayerStatus.questStatus = id;
            }
        }


    }



    public void InCompleteQuest(string id)
    {
        foreach (Quest q in questList)
        {
            if (q.id == id)
            {
                q.questObject.SetActive(false);
            }
        }


    }

    [YarnCommand("CompleteQuestAnimation_Q")]
    public void CompleteQuestAnimation(string title, string des)
    {

        title = title.Replace('/', ' ');
        des = des.Replace('/', ' ');

        Complete_Title.text = title;
        Complete_Description.text = des;
        CompleteQuestUI.GetComponent<Animator>().SetTrigger("complete");



    }

    public void InCompleteQuestAnimation(string title, string des)
    {

        title = title.Replace('/', ' ');
        des = des.Replace('/', ' ');

        Complete_Title.text = title;
        Complete_Description.text = des;
        CompleteQuestUI.GetComponent<Animator>().SetTrigger("complete");

    }

    [YarnCommand("UpdateQuestAnimation_Q")]
    public void UpdateQuestAnimation(string title, string des)
    {
        title = title.Replace('/', ' ');
        des = des.Replace('/', ' ');

        QuestUI_Title.text = title;
        QuestUI_Description.text = des;
        QuestUI.GetComponent<Animator>().SetTrigger("appear");

    }





    [YarnCommand("UpdateQuest_Q")]
    public void UpdateQuest(string id, string status)
    {
        foreach (Quest q in questList)
        {
            if (q.id == id)
            {
                switch (status)
                {
                    case "NotReady":
                        q.status = Quest.Status.NotReady;
                        q.questObject.GetComponent<QuestObject>().ChangeStatus(Quest.Status.NotReady);
                        break;
                    case "ReadyPickUp":
                        q.status = Quest.Status.ReadyPickUp;
                        q.questObject.GetComponent<QuestObject>().ChangeStatus(Quest.Status.ReadyPickUp);
                        break;
                    case "PickedUp":
                        q.status = Quest.Status.PickedUp;
                        q.questObject.GetComponent<QuestObject>().ChangeStatus(Quest.Status.PickedUp);
                        break;
                    case "Delivered":
                        q.status = Quest.Status.Delivered;
                        q.questObject.GetComponent<QuestObject>().ChangeStatus(Quest.Status.Delivered);
                        break;
                    default:
                        break;
                }

            }
        }


    }

    [YarnCommand("UpdateQuestDescription_Q")]
    public void UpdateQuestDescription(string id, string des)
    {
        foreach (Quest q in questList)
        {
            if (q.id == id)
            {
                q.questObject.GetComponent<QuestObject>().ChangeDescription(des);

            }
        }


    }

    [YarnCommand("SetCurrentQuest_Q")]
    public void SetCurrentQuest(string questID)
    {
        PlayerStatus.currentQuest = questID;
    }




    public void ToggleUIPage()
    {
        Debug.Log("UI");
       
        if (UICanvas != null)
        {
            Debug.Log("UI111");
            UICanvas.SetActive(!UICanvas.activeSelf);
            UIPageOpen = UICanvas.activeSelf;
       

        }


    }

    public GameObject InstructionCanvas;
    public void ToggleInstructionPage()
    {



        if (InstructionCanvas != null)
        {
            InstructionCanvas.SetActive(!InstructionCanvas.activeSelf);

        }


    }




    public GameObject Dialoguebox;
    public void ToogleDialogueUI()
    {
        Dialoguebox.SetActive(!Dialoguebox.activeSelf);

    }
}
