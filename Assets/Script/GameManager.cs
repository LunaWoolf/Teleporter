using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MilkShake;
using TMPro;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    public static float playerHealth = 1000;

    [SerializeField]
    public List<Quest> questList = new List<Quest>();

    [SerializeField]
    public List<Quest> inventoryList = new List<Quest>();

    public Shaker MyShaker;
    public ShakePreset MyShakePreset;
    public GameObject healthUi;
    public TextMeshProUGUI healthText;
    public Slider timeSlider;

    public bool inLight = false;

    void Start()
    {
        if (healthText != null)
        {
            healthText = healthUi.GetComponent<TextMeshProUGUI>();
            healthText.text = " " + playerHealth + " % ";
        }
        StartCoroutine(Timer());
    }

   
    void Update()
    {
       
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

    public void GetQuest(Quest quest)
    {

    }

    public void CompleteQuest(Quest quest)
    {

    }


}
