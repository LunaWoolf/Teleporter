using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public bool isActive;
    public string title;

    public void Complete()
    {
        isActive = false;
    }

}
