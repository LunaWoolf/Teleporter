//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MilkShake;
using TMPro;
using Yarn.Unity;

[System.Serializable]
public class GameManager : MonoBehaviour
{
  
    public static float playerHealth = 1000;

    //[Header("Transform Reference")]
  
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

    public GameObject UICanvas;

    void Start()
    {

        if (healthText != null)
        {
            healthText = healthUi.GetComponent<TextMeshProUGUI>();
            healthText.text = " " + playerHealth + " % ";
        }
        StartCoroutine(Timer());


    }

    public void CameraShake()
    {
        Debug.Log("Shake");
        MyShaker.Shake(MyShakePreset);

    }

    public void UpdateHealth(float change)
    {
        playerHealth = Mathf.Clamp(playerHealth + change, 0, 100);
        if (timeSlider != null)
            timeSlider.value = playerHealth;
        if(healthText != null)
            healthText.text = " " + playerHealth + " % ";
    }

    IEnumerator Timer()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(1f);
            if (inLight)
            {
                UpdateHealth(-5f);
            }
            else
            {
                //UpdateHealth(-1f);
            }
           
        }
    }


    [YarnCommand("CreateQuest")]
    public void CreateQuest(string id, string title, string description)
    {
        Debug.Log(id);
        Debug.Log(title);
        Debug.Log(description);

        title = title.Replace('/', ' ');
        description = description.Replace('/', ' ');

   

        Quest questNew = new Quest(id,title,description);

        GameObject questObject = Instantiate(QuestPrefab);

        questObject.GetComponent<QuestObject>().SetQuest(new Quest(id, title, description));

        questObject.transform.SetParent(QuestPanel.transform,false);

        questList.Add(questNew);


        QuestUI.GetComponentInChildren<QuestObject>().SetQuest(questNew);
        PlayAddQuestAnimation();

    }

    public GameObject QuestUI;
    public void PlayAddQuestAnimation()
    {
        QuestUI.GetComponent<Animator>().SetTrigger("appear");
    }

    

    public void GetQuest(Quest quest)
    {

    }

    public GameObject CompleteQuestUI;
    [YarnCommand("CompleteQuest")]
    public void CompleteQuest()
    {

        CompleteQuestUI.GetComponent<Animator>().SetTrigger("complete");

    }

    public void LoadQuest()
    {
        /*for (int i = 0; i < questList.Count; i++)
        {
            Quest quest = questList[i];
        }*/
    }

    public void ToogleUIPage()
    {
        if (UICanvas == null)
        {
            UICanvas = GameObject.Find("UICanvas");
        }

        UICanvas.SetActive(!UICanvas.activeSelf);


    }

}
