using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    public TextMeshProUGUI highScore;

    void Start()
    {
        highScore.text = "HIGHSCORE: " + PlayerPrefs.GetInt("HighScore");
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        Debug.Log("New Game");
        SceneManager.LoadScene("Main");
    }
}
