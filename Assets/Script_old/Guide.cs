using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Guide : MonoBehaviour
{
    public bool CanvasOn;
    public VideoClip video;
    [TextArea(5, 20)]
    public string instruction;
    public Sprite Image;
    
}
