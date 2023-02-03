using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InstructionMenu : MonoBehaviour
{

    public EventSystem eventSystem;
    public GameObject firstButton;
    // Start is called before the first frame update
    void Start()
    {
        eventSystem.SetSelectedGameObject(firstButton);
    }

    // Update is called once per frame
    void Update()
    {
        if (eventSystem.currentSelectedGameObject == null)
        {
            eventSystem.SetSelectedGameObject(firstButton);

        }

    }
}
