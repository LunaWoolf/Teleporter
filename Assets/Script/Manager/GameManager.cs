//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MilkShake;
using TMPro;
using Yarn.Unity;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameManager : MonoBehaviour
{
  
    public float playerHealth = 100;

    [Header("Transform Reference")]
  
    public List<Quest> questList = new List<Quest>();
    public GameObject QuestPanel;
    public GameObject QuestPrefab;


    public List<Quest> inventoryList = new List<Quest>();
  


    public Shaker MyShaker;
    public ShakePreset MyShakePreset;
    public GameObject healthUi;
    public TextMeshProUGUI healthText;
    public Slider timeSlider;

    public bool inLight = false;
    public int LightDemange = 10;

    public GameObject UICanvas;
    public GameObject BigMapCamera;
    public bool UIPageOpen;

    public DialogueRunner MainDialogueRunner;
    public YarnProject Monologue2;

    PresistenceManagerScript pms;
    public GameObject Player;

    [System.Serializable]

    public struct QuestSign
    {
        public string Questname;
        public GameObject QuestSignObject;
        public bool complete;
    }
    public QuestSign[] QuestSignList;
    [SerializeField] public Dictionary<string, GameObject> QuestSignDictionary = new Dictionary<string, GameObject>();


   
    void Awake()
    {
    

        foreach (QuestSign q in QuestSignList)
        {
            QuestSignDictionary.Add(q.Questname, q.QuestSignObject);

        }

        
       
    }

    void Start()
    {
        /*pms = GameObject.Find("PersistenceManager").GetComponent<PresistenceManagerScript>();
        pms.LoadPlayerPosition();
        if (pms.Player != null)
        {
           
        }
        else
        {
            pms = GameObject.Find("PersistenceManager").GetComponent<PresistenceManagerScript>();
            pms.GetPlayer();
        }*/

        ToggleQuestSign(PlayerStatus.currentQuest, true);

        if (PlayerStatus.QuestList == null)
        {
            PlayerStatus.QuestList = QuestSignList;
        }

        if (PlayerStatus.currentYarnProject != null)
        {
            MainDialogueRunner.yarnProject = PlayerStatus.currentYarnProject;

        }



        Player.transform.position = PlayerStatus.playerPosition;


        //PlayerStatus.currentYarnProject = Monologue2;





        if (healthText != null)
        {
            healthText = healthUi.GetComponent<TextMeshProUGUI>();
            healthText.text = " " + playerHealth + " % ";
        }
        StartCoroutine(Timer());


    }

    public void CameraShake()
    {
        //Debug.Log("Shake");
        MyShaker.Shake(MyShakePreset);

    }

    public void UpdateHealth(float change)
    {
        playerHealth = Mathf.Clamp(playerHealth + change, 0, 100);

        
        if (timeSlider != null)
            timeSlider.value = playerHealth;

        if (playerHealth == 0)
        {
            SceneManager.LoadScene("DeadScene", LoadSceneMode.Single);
        }
    }

    IEnumerator Timer()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(1f);
            if (inLight && !Player.GetComponent<PlayerMovement>().possess)
            {
                UpdateHealth(-LightDemange);
            }
            if (Player.GetComponent<PlayerMovement>().notEfject)
            {
                UpdateHealth(-1f);
            }
            else if (playerHealth < 100f)
            {
                UpdateHealth(1f);
            }
           
        }
    }


    [YarnCommand("CreateQuest")]
    public void CreateQuest(string id, string title, string description)
    {
        //Debug.Log(id);
        //Debug.Log(title);
        //Debug.Log(description);

        title = title.Replace('/', ' ');
        description = description.Replace('/', ' ');

   

        Quest questNew = new Quest(id,title,description);

        GameObject questObject = Instantiate(QuestPrefab);

        questNew.questObject = questObject;

        questObject.GetComponent<QuestObject>().SetQuest(new Quest(id, title, description));

        questObject.transform.SetParent(QuestPanel.transform,false);

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
    [YarnCommand("CompleteQuest")]
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

    [YarnCommand("CompleteQuestAnimation")]
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

    [YarnCommand("UpdateQuestAnimation")]
    public void UpdateQuestAnimation(string title, string des)
    {
        title = title.Replace('/', ' ');
        des = des.Replace('/', ' ');

        QuestUI_Title.text = title;
        QuestUI_Description.text = des;
        QuestUI.GetComponent<Animator>().SetTrigger("appear");

    }

 



    [YarnCommand("UpdateQuest")]
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

    [YarnCommand("UpdateQuestDescription")]
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

    [YarnCommand("SetCurrentQuest")]
    public void SetCurrentQuest(string questID)
    {
        PlayerStatus.currentQuest = questID;
    }




    public void ToggleUIPage()
    {
        //if (UICanvas == null)
        //{
        //UICanvas = GameObject.Find("UICanvas");
        

        if (UICanvas != null)
        {
            UICanvas.SetActive(!UICanvas.activeSelf);
            UIPageOpen = UICanvas.activeSelf;
            BigMapCamera.GetComponent<Transform>().position = new Vector3(0, 500, 0);

            if (!UIPageOpen)
            {
                Player.GetComponent<PlayerMovement>().RepositionBigMapCamera();
            }
           

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

    [Header("UI Instruction")]
    public GameObject Aim;
    public GameObject Teleport;
    public GameObject Possess;
    public GameObject Xray;
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

    }






}
