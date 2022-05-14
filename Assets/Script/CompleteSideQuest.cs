using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteSideQuest : MonoBehaviour
{
    public GameManager gm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gm.CompleteQuest("SideQuest_01");
            gm.CompleteQuestAnimation("Climb/To/The/Top", "Complete");
            gameObject.SetActive(false);
        }

    }


}
