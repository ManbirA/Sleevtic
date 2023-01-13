using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager scoreManagerInstance;

    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    int score = 0;
    int highscore = 0;

    private void Awake() {
        scoreManagerInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscoreValue", 0);
        scoreText.text = score.ToString() + " Points";
        highScoreText.text = "High score: " + highscore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoint() {
        score += 1;
        scoreText.text = score.ToString() + " Points";
        PlayerPrefs.SetInt("highscore", score);
        if (score > highscore)
            PlayerPrefs.SetInt("highscoreValue", score);
    }
}
