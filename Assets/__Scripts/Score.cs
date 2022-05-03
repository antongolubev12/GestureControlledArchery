using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{   
    private static Score _instance;

    public static Score Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    [SerializeField] Text scoreText;
    [SerializeField] Text highScoreText;

    private float currentScore;

    private static float highScore;
    // Update is called once per frame

    private void Start()
    {
        //get the saved high score from PlayerPrefs
        highScore = PlayerPrefs.GetFloat("HighScore");
        print(highScore);
        //set UI text to players high score
        highScoreText.text = "High Score: " + highScore.ToString("0");
        currentScore=0;
    }

    public void SetScore(float score)
    {
        currentScore +=score;
        //set the players score to their z position
        //save to PlayerPrefs
        PlayerPrefs.SetFloat("Score", currentScore);

        //convert players score into string with no decimals and 
        //set the UI text to the score
        scoreText.text = "Score: " + currentScore.ToString("0");


        //set highScore if player beats it
        if (currentScore > highScore)
        {
            highScore = currentScore;
            SetHighScore(currentScore);

        }

    }

    public void SetHighScore(float highscore)
    {
        PlayerPrefs.SetFloat("HighScore", highscore);
        highScoreText.text = "High Score: " + PlayerPrefs.GetFloat("HighScore").ToString("0");
    }
}
