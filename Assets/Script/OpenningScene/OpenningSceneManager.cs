using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class OpenningSceneManager : MonoBehaviour
{
    public VideoPlayer vid;
    public SceneManage sceneManager;
    public GameObject LanguagechoicePage;
    public EventSystem EventSystem;
    public GameObject FirstButton;
    public TMP_InputField PlayerNameInputField;

    VirtualKeyboard vk = new VirtualKeyboard();
    void Start()
    {
        PlayerStatus.playerPosition = new Vector3(233.65f, 1.55f, -21.5f);
        //Debug.Log(PlayerStatus.playerPosition);

        //vk.ShowTouchKeyboard();
    }

    public void VideoStart()
    {
        vid.Play();
        vid.loopPointReached += CheckOver;
        TouchScreenKeyboard.Open("");
    }

    void Update()
    {
        if (EventSystem.currentSelectedGameObject == null)
        {
            EventSystem.SetSelectedGameObject(FirstButton);

        }

        PlayerNameInputField.onEndEdit.AddListener(val =>
        {
            if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.KeypadEnter))
                Debug.Log("End edit on enter");
        });


    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        LanguagechoicePage.SetActive(true);
    }

    public void SkipVideo()
    {
        vid.Stop();
    }

    public void SetLanaguage(string lan)
    {
        PresistenceManagerScript.Instance.Language = lan;
        //sceneManager.LoadLevel("Chapter0");
        sceneManager.LoadLevel("Dialogue_00_01");
    }

    public void SelectInputField()
    {
        // PlayerNameInputField.select();

    }
   
}
