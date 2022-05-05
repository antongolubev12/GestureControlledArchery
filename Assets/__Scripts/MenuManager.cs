using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class MenuManager : MonoBehaviour
{
    private static MenuManager _instance;

    public static MenuManager Instance { get { return _instance; } }

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
    public bool isPaused = false;

    public bool isOver = false;

    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private GameObject myo = null;

    private ThalmicMyo thalmicMyo;

    private Scene scene;

    private void Start()
    {
        Time.timeScale=1f;
        scene = SceneManager.GetActiveScene();
    }
    // Update is called once per frame
    private void Update()
    {
        thalmicMyo = myo.GetComponent<ThalmicMyo>();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        //double tap to pause
        if (isPaused)
        {
            if (thalmicMyo.pose == Pose.Fist)
            {
                Pause();
            }
        }
        else
        {
            if (thalmicMyo.pose == Pose.DoubleTap)
            {
                Pause();
            }
        }




        //WaveIn to play again
        if (isOver)
        {
            if (thalmicMyo.pose == Pose.WaveIn)
            {
                if (scene.name == "Game")
                {
                    SceneController.Instance.LoadGame();
                }
                else if (scene.name == "Practice")
                {
                    SceneController.Instance.LoadPractice();
                }

            }
        }

        //Spread fingers to go to main menu if game is paused or over
        if (Time.timeScale == 0f)
        {
            if (thalmicMyo.pose == Pose.FingersSpread)
            {
                SceneController.Instance.LoadMainMenu();
            }
        }
    }

    public void GameOverMenu()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
        isOver = true;
    }

    public void Pause()
    {
        if (isPaused)
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
        else
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }

    }

}