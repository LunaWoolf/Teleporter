using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Keyboard : MonoBehaviour
{

    string word = null;
    int wordIndex = 0;
    string alpha;
    public Text myName = null;

    public bool isCap = false;

    public EventSystem eventSystem;
    public GameObject Q;

    public void alphabetFunction(string alphabet)
    {

        wordIndex++;

        if (isCap)
        {
            word = word + alphabet.ToUpper();
            foreach (Text t in KeyboardText)
            {
                t.text = t.text.ToLower();
            }
            isCap = !isCap;

        }
        else
        {
            word = word + alphabet.ToLower();
        }
        myName.text = word;

    }

    public void Update()
    {

        if (eventSystem.currentSelectedGameObject == null)
        {
            eventSystem.SetSelectedGameObject(Q);

        }
    }

    [SerializeField]
    public Text[] KeyboardText;

    public void isShift()
    {
        isCap = !isCap;

        if (isCap)
        {
            foreach (Text t in KeyboardText)
            {
                t.text = t.text.ToUpper();
            }


        } else
        {
            foreach (Text t in KeyboardText)
            {
                t.text = t.text.ToLower();
            }

        }
    }


    public void isDelet()
    {
        if (wordIndex > 0)
        {
            wordIndex--;

            word = word.Substring(0, word.Length - 1);
          
            myName.text = word;

        }
        
    }

    public PresistenceManagerScript pms;
    public void Enter()
    {
        pms.ChangePlayerName(word);
    }

}
