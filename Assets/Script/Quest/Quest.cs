using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{

    public enum Status { NotReady, ReadyPickUp, PickedUp, Delivered };

    [SerializeField]
    public bool isActive;
    [SerializeField]
    public string id;
    [SerializeField]
    public string title;
    [SerializeField]
    public string description;
    [SerializeField]
    public Status status;
    [SerializeField]
    public GameObject questObject;
   


    public void Complete()
    {
        isActive = false;
    }

    public Quest(string id, string title, string description)
    {
        this.isActive = true;
        this.id = id;
        this.title = title;
        this.status = Status.NotReady;
        this.description  = description;
        
    }
}
