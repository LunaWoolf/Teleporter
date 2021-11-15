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


    public void SetQuest(Quest q)
    {
        quest = q;
        Debug.Log(q.title);
        Debug.Log(quest.title);
        this.title.text = quest.title;
        this.description.text = quest.description;
    }

}
