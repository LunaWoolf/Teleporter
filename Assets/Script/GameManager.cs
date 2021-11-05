using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    public static float playerHealth = 1000;
    

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

    public void updateHealth(float change)
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
                updateHealth(-3f);
            }
            else
            {
                updateHealth(-1f);

            }
           
        }
    }

}
