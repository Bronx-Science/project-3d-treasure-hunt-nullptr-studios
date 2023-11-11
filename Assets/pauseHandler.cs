using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    public static bool isPaused = false;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
        if (isPaused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            playerMovement.instance.hasLookingRights = false;
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            playerMovement.instance.hasLookingRights = true;
        }
    }
}
