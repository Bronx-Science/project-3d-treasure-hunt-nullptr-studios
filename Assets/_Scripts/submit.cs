using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class submit : MonoBehaviour
{
    // Start is called before the first frame update
    // get input from input field
    [SerializeField] private TMP_InputField input;
    [SerializeField] private Words wordManager;

    // Update is called once per frame
    public void submitGuess(){ 
        wordManager.guessWord(input.text);
    }
}
