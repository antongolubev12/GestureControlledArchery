using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{   
    private static Timer _instance;

    public static Timer Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    [SerializeField] Text timerText;
    private float time;
    private float roundedTime;

    private void Start() {
        time=60f;
        timerText.text="Time Remaining: "+time;
    }

    private void Update() {
        time -= Time.deltaTime;
        roundedTime= Mathf.Round(time);
        if ( time < 0 )
        {
            MenuManager.Instance.GameOverMenu();
        }

        timerText.text="Time Remaining: "+roundedTime;
    }

}