using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDialoguePosition : MonoBehaviour
{
    public GameObject Player;

    void Update()
    {
       
        this.transform.LookAt(Player.transform);
        this.transform.RotateAround(transform.position, transform.up, 180f);
    }

}
