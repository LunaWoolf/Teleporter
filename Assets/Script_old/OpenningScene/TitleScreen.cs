using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TitleScreen : MonoBehaviour
{
    public OpenningSceneManager osm;
    public AudioSource audio;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            audio.Stop();
            osm.VideoStart();
            
            this.gameObject.SetActive(false);
        }


    }
}
