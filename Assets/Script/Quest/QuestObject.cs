using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestObject : MonoBehaviour
{
    [SerializeField]
    public Quest quest;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public GameObject Status_N;
    public GameObject Status_R;
    public GameObject Status_P;
    public GameObject Status_D;

    public float time = 0;
    float timeLeft;
    public Slider timeSlider;
    public GameManager gm;
    public bool useTimer;

    public void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (useTimer)
        {
            timeLeft = time;
            StartCoroutine(Timer());
        }
  
        
        
    }

    public void SetQuest(Quest q)
    {
        quest = q;
        this.title.text = quest.title;
        this.description.text = quest.description;
    }

    public void ChangeStatus(Quest.Status status)
    {
        Status_N.SetActive(false);
        Status_R.SetActive(false);
        Status_P.SetActive(false);
        Status_D.SetActive(false);

        switch (status)
        {
            case Quest.Status.NotReady:
                Status_N.SetActive(true);
                break;
            case Quest.Status.ReadyPickUp:
                Status_R.SetActive(true);
                break;
            case Quest.Status.PickedUp:
                Status_P.SetActive(true);
                break;
            case Quest.Status.Delivered:
                Status_D.SetActive(true);
                break;
            default:
                break;
        }

    }

    public void ChangeDescription(string d)
    {

        d = d.Replace('/', ' ');
        quest.description = d;
        this.description.text = d;
    }

    IEnumerator Timer()
    {
        while (timeLeft > 0)
        {

            yield return new WaitForSeconds(1f);
            
            timeLeft--;
            timeSlider.value = timeLeft / (time / 100);
            
        }

        if (timeLeft == 0)
        {
            if (gm != null)
            {
                gm.InCompleteQuest(quest.id);
                gm.InCompleteQuestAnimation(quest.title, "Out of Time");

            }
            


        }
    }


}
