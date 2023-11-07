using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Words : MonoBehaviour
{
    
    public string word = "";
    public TMPro.TextMeshProUGUI wordText;

    public char[] revealedLetters;
    public int lettersRevealed = 0;

    public int wordLength = 0;

    public string displayString = "Getting Word...";


    // Start is called before the first frame update
    void Start()
    {
        wordbank.instance.getWordFromApi();
    }

    // Update is called once per frame
    void Update()
    {
         updateDisplay();
        wordText.text = displayString;
       
    }

    public void getWord(){
        word = wordbank.instance.word;
        Debug.Log(word);
        init();
    }

    public void init(){
        wordLength = word.Length;
        revealedLetters = new char[26];
        displayString = "";
        for(int i = 0; i < wordLength; i++){
            displayString += "_ ";
        }
    }

    public void updateDisplay(){
        displayString = "";
        for (int i = 0; i < wordLength; i++)
        {
            char toAdd = '_';
            foreach (char letter in revealedLetters)
            {
                if (letter == word[i])
                {
                    toAdd = letter;
                    break;
                }
            }
            displayString += toAdd + " ";
        }

        
    }

    public void revealLetter(char letter){
        revealedLetters[lettersRevealed] = letter;
        lettersRevealed++;
        updateDisplay();
    }

    public void guessWord(string guess){
        if (guess == word)
        {
            Debug.Log("You Win!");
            displayString = "Getting Word...";
            wordbank.instance.getWordFromApi();

        }
        else
        {
            Debug.Log("You Lose!");
        }
    }




}
