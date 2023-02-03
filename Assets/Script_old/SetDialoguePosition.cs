using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDialoguePosition : MonoBehaviour
{
    public GameObject Player;
    public Camera UICamera;
    float dist;
    public float initialDist = 1000f;
    public bool adjustCanvasScale = false;
    void Start()
    {
        if (Player == null)
        {
            Player = GameObject.Find("Player");

        }

        if (UICamera == null)
        {
            UICamera = GameObject.Find("UICamera").GetComponent<Camera>();
        }    

    }
    void Update()
    {
       if(Player != null)
       {
            this.transform.LookAt(Player.transform);
            this.transform.RotateAround(transform.position, transform.up, 180f);
       }

       if (adjustCanvasScale && UICamera != null)
       {
            dist = Vector3.Distance(this.transform.position, UICamera.transform.position);
            this.transform.localScale = Vector3.one * dist / initialDist;
        }
        
    }

}
