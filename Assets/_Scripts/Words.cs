using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Words : MonoBehaviour
{
    [SerializeField]
    private AudioClip win;

    [SerializeField]
    private AudioClip miss;
    public string word = "";
    public TMPro.TextMeshProUGUI wordText;

    // adafwafwa

    public char[] revealedLetters;
    public int lettersRevealed = 0;

    public int wordLength = 0;

    [SerializeField]
    private TMPro.TextMeshProUGUI revealText;
    public string displayString = "Getting Word...";
    string revealLetterString = "";

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
        revealText.text = revealLetterString;
    }

    public void getWord()
    {
        word = wordbank.instance.word;
        Debug.Log(word);
        init();
    }

    public void init()
    {
        wordLength = word.Length;
        revealedLetters = new char[26];
        displayString = "";
        for (int i = 0; i < wordLength; i++)
        {
            displayString += "_ ";
        }
    }

    public void updateDisplay()
    {
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
        revealLetterString = "";
        for (int i = 0; i < lettersRevealed; i++)
        {
            revealLetterString += revealedLetters[i] + " ";
        }
        
    }

    public void revealLetter(char letter)
    {
        revealedLetters[lettersRevealed] = letter;
        lettersRevealed++;
        updateDisplay();
    }

    public void guessWord(string guess)
    {
        if (guess == word)
        {
            AudioSource aus = Camera.main.GetComponent<AudioSource>();
            aus.PlayOneShot(win);
            GameManager.instance.addScore(100 * wordLength );
            Debug.Log("You Win!");
            displayString = "Getting Word...";
            wordbank.instance.getWordFromApi();
            TreasureSpawner.instance.clear();
            
            lettersRevealed = 0;
        }
        else
        {
            AudioSource aus = Camera.main.GetComponent<AudioSource>();
            aus.PlayOneShot(miss);
            displayString = "Getting Word...";
            wordbank.instance.getWordFromApi();
            TreasureSpawner.instance.clear();
            lettersRevealed = 0;
            Debug.Log("You Lose!");
        }
    }
}
