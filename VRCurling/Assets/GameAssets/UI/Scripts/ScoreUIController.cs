using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private string scorePrefix = "Accuracy: ";
    [SerializeField] private string scoreSuffix = "%";
    public void SetScore(int score){
        scoreText.text = scorePrefix + score.ToString() + scoreSuffix;
    }
}
