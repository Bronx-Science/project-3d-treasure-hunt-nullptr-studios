using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instruction : MonoBehaviour
{
    [SerializeField] private GameObject panel;


    public void togglePanel(){
        if (panel.activeSelf){
            panel.SetActive(false);
        } else {
            panel.SetActive(true);
        }
    }
}
