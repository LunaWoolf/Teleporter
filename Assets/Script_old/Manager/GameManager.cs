//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MilkShake;
using TMPro;
using Yarn.Unity;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

[System.Serializable]
public class GameManager : MonoBehaviour
{
  
    public float playerHealth = 100;

    [Header("Transform Reference")]
  
    public List<Quest> questList = new List<Quest>();
    public GameObject QuestPanel;
    public GameObject QuestPrefab;
    public AudioManager AudioManager;

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

    public PresistenceManagerScript pms;
    public GameObject Player;
    public SceneManage sceneManager;

    
    [System.Serializable]

    public struct QuestSign
    {
        public string Questname;
        public GameObject QuestSignObject;
        public bool complete;
    }
    public QuestSign[] QuestSignList;
    [SerializeField] public Dictionary<string, GameObject> QuestSignDictionary = new Dictionary<string, GameObject>();

    public EventSystem eventSystem;

    void Awake()
    {
    

        foreach (QuestSign q in QuestSignList)
        {
            QuestSignDictionary.Add(q.Questname, q.QuestSignObject);

        }
    


        float player_x = PlayerPrefs.GetFloat("p_x");
        float player_y = PlayerPrefs.GetFloat("p_y");
        float player_z = PlayerPrefs.GetFloat("p_z");

        string q_id = PlayerPrefs.GetString("q_id");
        string q_title = PlayerPrefs.GetString("q_title");
        string q_des = PlayerPrefs.GetString("q_des");

        /*
        if (PlayerPrefs.GetInt("save") == 1)
        {
            Player.transform.position = new Vector3(player_x, player_y, player_z);
            CreateQuest(q_id, q_title, q_des);
            PlayerPrefs.SetInt("save", 0);
            PlayerPrefs.Save();
        }*/

        if (AudioManager == null)
        {
            AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        }

        if (sceneManager == null)
        {
            sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManage>();

        }



    }

    void Start()
    {
        pms = FindObjectOfType<PresistenceManagerScript>();
            
        /*pms.LoadPlayerPosition();
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


        /*if (PlayerPrefs.GetInt("mono") == 2)
        {
            MainDialogueRunner.yarnProject = Monologue2;
            PlayerPrefs.SetInt("mono", 0);
            PlayerPrefs.Save();
        }*/
        


        if (healthText != null)
        {
            healthText = healthUi.GetComponent<TextMeshProUGUI>();
            healthText.text = " " + playerHealth + " % ";
        }
        StartCoroutine(Timer());

        if (AudioManager == null)
        {
            AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        }

        if (sceneManager == null)
        {
            sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManage>();

        }
       

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

        if (playerHealth <= 0 && sceneManager != null)
        {
            sceneManager.LoadLevel("DeadScene");
         
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

        if (id != "Top")
        {
            PlayerPrefs.SetString("q_id", id);
            PlayerPrefs.SetString("q_title", title);
            PlayerPrefs.SetString("q_des", description);
            PlayerPrefs.Save();

        }
   

    }

    public GameObject QuestUI;
    public TextMeshProUGUI QuestUI_Title;
    public TextMeshProUGUI QuestUI_Description;
    public void PlayAddQuestAnimation()
    {
        QuestUI.GetComponent<Animator>().SetTrigger("appear");
        if(AudioManager != null)
            AudioManager.Play("CreateQuest");
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


               
                //PlayerStatus.questStatus = id;
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

        AudioManager.Play("CompleteQuest");



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

        AudioManager.Play("UpdateQuest");
       
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
      
        PlayerPrefs.SetString("q_des", des);
        PlayerPrefs.Save();





    }

    [YarnCommand("SetCurrentQuest")]
    public void SetCurrentQuest(string questID)
    {
        PlayerStatus.currentQuest = questID;

    }


    public List<GameObject> InGameTargetUI = new List<GameObject>();

    public void ToggleUIPage()
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
       

    }

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
