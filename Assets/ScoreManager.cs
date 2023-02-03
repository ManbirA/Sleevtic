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
    int combo = 0;

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

    public void UpdateText() {
        scoreText.text = score.ToString() + " Points";
        PlayerPrefs.SetInt("highscore", score);
        if (score > highscore)
            PlayerPrefs.SetInt("highscoreValue", score);
    }

    public int GetComboMultiplier() {
        if (combo <= 50) {
            return combo/10 + 1;
        } 

        return 10;
    }

    public void AddPoint() {
        combo += 1;
        score += 2 * GetComboMultiplier();
        UpdateText();
    }

    public void AddBonus() {
        combo += 1;
        score += 50;
        UpdateText();
    }

    public void ResetCombo() {
        combo = 0;
    }
}
