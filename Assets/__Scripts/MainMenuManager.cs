

using System.Collections;
using System.Collections.Generic;
using Pose = Thalmic.Myo.Pose;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject myo = null;
    private ThalmicMyo thalmicMyo;

    private void Start() {
        
    }
    private void Update() {
        // Access the ThalmicMyo component attached to the Myo object.
        thalmicMyo = myo.GetComponent<ThalmicMyo>();

        if(thalmicMyo.pose==Pose.DoubleTap){
            SceneManager.LoadScene("Game");
        }

        if(thalmicMyo.pose==Pose.WaveOut){
            SceneManager.LoadScene("Practice");
        }

        if(thalmicMyo.pose==Pose.WaveIn){
            SceneManager.LoadScene("Quit");
        }
        
    }

    public void LoadGame(){
        SceneManager.LoadScene("Game");
    }

    public void LoadPractice(){
        SceneManager.LoadScene("Practice");
    }

    public void Quit(){
        Application.Quit();
    }

}