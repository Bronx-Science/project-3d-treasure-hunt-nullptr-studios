using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playGame : MonoBehaviour
{
    [SerializeField]
    private AudioClip clip;

    // Start is called before the first frame update
    public void play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        Camera.main.GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
