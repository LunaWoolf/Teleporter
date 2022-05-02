using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using System;

public class GuideManager : MonoBehaviour
{
    public GameObject GuideCanvas;
    public TextMeshProUGUI GuideText;
    public VideoPlayer VideoPlayer;
    public Image image;
    
    void DisableGuideCanvas()
    {
        GuideCanvas.SetActive(false);

    }

    void EnableGuideCanvas()
    {
        GuideCanvas.SetActive(true);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Guide")
        {
            if (other.gameObject.TryGetComponent<Guide>(out Guide g))
            {
               
                 EnableGuideCanvas();
                

                if (g.instruction != null)
                {
                    GuideText.text = g.instruction;
                }

                if (g.video != null)
                {
                    VideoPlayer.clip = g.video;
                }

                if (g.Image != null)
                {
                    image.gameObject.SetActive(true);

                    image.sprite = g.Image;
                }
                else
                {
                    image.gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Guide")
        {

            if (other.gameObject.TryGetComponent<Guide>(out Guide g))
            {
                
                 DisableGuideCanvas();

            }
            image.sprite = null;
            VideoPlayer.clip = null;
        }


    }
}
