using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float timeRemaining = 300f;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;

    public float score = 0f;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;


        if (timeRemaining <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        scoreText.text = "Score: " + score;
        timeText.text = "Time: " + (int)timeRemaining;
    }

    public void addScore(float scoreToAdd)
    {
        score += scoreToAdd;
    }

    public void addTime(float timeToAdd)
    {
        timeRemaining += timeToAdd;
    }
}
