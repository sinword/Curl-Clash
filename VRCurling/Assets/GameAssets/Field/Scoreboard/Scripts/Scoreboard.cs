using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Detector detector;
    private int score = 0;
    public void ResetScore(){
        score = 0;
        updateScoreText();
    }

    private void updateScoreText(){
        scoreText.text = score.ToString();
    }

    private void Start()
    {
        detector.OnDetectStoneStay1.AddListener(AddScore);
        detector.OnDetectStoneStay2.AddListener(AddScore);
        detector.OnDetectStoneStay3.AddListener(AddScore);
    }

    private void AddScore()
    {
        score++;
        updateScoreText();
    }
}
