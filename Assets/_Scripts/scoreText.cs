using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreText : MonoBehaviour
{
    // ahhhhhhhhhhhh

    [SerializeField]
    private TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
        score.text = "Score: " + GameManager.lastScore;
    }

    // Update is called once per frame
    void Update() { }
}
