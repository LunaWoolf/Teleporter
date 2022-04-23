using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip phoneBuzz;

    public Sound[] sounds;


    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = this.gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.playOnAwake = s.playOnAwake;


        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.Play();

        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewQuestAduio()
    {

    }

    public void UpdateQuestAduio()
    {
    }

    public void CompleteQuestAduio()
    {
    }
}
