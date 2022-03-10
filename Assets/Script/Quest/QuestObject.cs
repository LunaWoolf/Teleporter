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

}
