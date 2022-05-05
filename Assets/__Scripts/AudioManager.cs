using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Singleton
public class AudioManager : MonoBehaviour
{   
    private static AudioManager _instance;

    public static AudioManager Instance { get { return _instance; } }

    [SerializeField] private Sound[] sounds;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

        foreach(Sound sound in sounds)
        {
            //set the source of the sound 
            sound.source=gameObject.AddComponent<AudioSource>();

            //set the source to the clip
            sound.source.clip=sound.clip;

            sound.source.volume=sound.volume;
            //sound.source.pitch=sound.pitch;
            sound.source.loop=sound.loop;
        }
    }
    private void Start() 
    {
        //Play("Music");
    }

    public void Play(string name)
    {
       Sound sound= Array.Find(sounds, sound=>sound.name == name);
       sound.source.Play();
    }

    public void PlayOnce(string name)
    {
       Sound sound= Array.Find(sounds, sound=>sound.name == name);
       sound.source.PlayOneShot(sound.clip);
    }

    public void PlayClash(){
        Play("Clash");
    }

    public void PlayHit(){
        Play("Hit");
    }

    public void PlayFire(){
        Play("Fire");
    }
    public void LightSaberOn(){
        PlayOnce("SaberOn");
    }

    public void LightSaberOff(){
        Play("SaberOff");
    }


    public void LightSaberHum(){
        PlayOnce("Hum");
    }

    public void MenuClick(){
        Play("Click");
    }

    public void Pickup()
    {
        Play("PickUp");
    }


    

}
    
