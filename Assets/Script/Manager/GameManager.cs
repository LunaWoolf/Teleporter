//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MilkShake;
using TMPro;

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

    void Start()
    {

        //CreateQuest("01", "01", "01");

        //CreateQuest("01", "02", "02");

        //CreateQuest("01", "03", "03");

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
                UpdateHealth(-3f);
            }
            else
            {
                UpdateHealth(-1f);
            }
           
        }
    }

  

    public void CreateQuest(string id, string title, string description)
    {
        Debug.Log(id);
        Debug.Log(title);
        Debug.Log(description);

        Quest questNew = new Quest(id,title,description);

        GameObject questObject = Instantiate(QuestPrefab);

        questObject.GetComponent<QuestObject>().SetQuest(new Quest(id, title, description));

        questObject.transform.SetParent(QuestPanel.transform,false);

        questList.Add(questNew);

    }

    public void GetQuest(Quest quest)
    {

    }

    public void CompleteQuest(Quest quest)
    {

    }

    public void LoadQuest()
    {
        /*for (int i = 0; i < questList.Count; i++)
        {
            Quest quest = questList[i];
        }*/
    }

}
