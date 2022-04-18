using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TitleScreen : MonoBehaviour
{
    public OpenningSceneManager osm;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            osm.VideoStart();
            this.gameObject.SetActive(false);
        }


    }
}
