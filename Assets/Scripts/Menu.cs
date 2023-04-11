using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Menu : MonoBehaviour
{
    [SerializeField] private Score score;
    public TextMeshProUGUI scoreText;
    private void Start()
    {
        score = GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<Score>();
        scoreText.text = "Highest Score : " + score.GetHighScore();
    }
    public void EnterGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
