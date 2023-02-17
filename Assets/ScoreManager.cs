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
    public TMP_Text comboText;
    public TMP_Text multiplierText;

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
        comboText.text = "Combo: " + combo.ToString();
        PlayerPrefs.SetInt("currentScoreValue", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText() {
        scoreText.text = score.ToString() + " Points";
        PlayerPrefs.SetInt("currentScoreValue", score);
        if (score > highscore)
            PlayerPrefs.SetInt("highscoreValue", score);
        comboText.text = "Combo: " + combo.ToString();
    }

    public int GetComboMultiplier() {
        if (combo <= 50) {
            int multiplier = combo/10 + 1;
            multiplierText.text = multiplier.ToString() + "x";
            return multiplier;
        } 

        return 10;
    }

    public void AddPoint() {
        int comboMultiplier = GetComboMultiplier();
        combo += 1;
        score += 2 * comboMultiplier;
        UpdateText();
    }

    public void AddBonus() {
        combo += 1;
        score += 50;
        UpdateText();
    }

    public void ResetCombo() {
        combo = 0;
        comboText.text = "Combo: " + combo.ToString();
        multiplierText.text = "1x";
    }
}
