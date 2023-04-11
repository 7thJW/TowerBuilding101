using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Score : MonoBehaviour
{
    public static Score instance;
    [SerializeField] private int highestScore;
    [SerializeField] private int scoreValue;
    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
   
    public int AddScoreToEnd(int addScore)
    {
        scoreValue += addScore;
        if (scoreValue > highestScore)
        {
            highestScore = scoreValue;
        }
        return scoreValue;
    }
    public void ResetScore()
    {
        scoreValue = 0;
    }
    public int GetHighScore()
    {
        return highestScore;
    }
}
