

using System.Collections;
using System.Collections.Generic;
using Pose = Thalmic.Myo.Pose;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class SceneController : MonoBehaviour
{
    private static SceneController _instance;

    public static SceneController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void LoadPractice()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Practice");
    }
}