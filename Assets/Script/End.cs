using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    public bool canRestart;
    public Restart restart;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitForRestart());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canRestart && Input.anyKeyDown)
        {
            restart.RestartGame();
           
        }
    }

    IEnumerator waitForRestart()
    {
        
        yield return new WaitForSeconds(3f);
        canRestart = true;

    }
}
