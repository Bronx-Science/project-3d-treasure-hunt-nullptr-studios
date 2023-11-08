using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;


public class wordbank : MonoBehaviour
{
    public string apiUrl = "https://random-word-api.herokuapp.com/word";
    public static wordbank instance;
    public string word = "";

    public Words wordManager;



    void Awake()
    {
        instance = this;
    }
    public void getWordFromApi(){
        Debug.Log("Getting word from API");
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        StartCoroutine(getWord(request, (response) => {
            if (response != null) {
                // Debug.Log(response);
                word = response;
                wordManager.getWord();
            } 
            else {
                Debug.Log("Error");
                word = null;
            }
        }));
        
        
    }
    public static wordbank getInstance(){
        return instance;
    }

    IEnumerator getWord(UnityWebRequest request, Action<string> callback){
        

        UnityWebRequestAsyncOperation operation = request.SendWebRequest();
        yield return operation;

        if(request.result == UnityWebRequest.Result.Success){
            word = request.downloadHandler.text;
            // clean json formatting from word
            word = word.Replace("[", "");
            word = word.Replace("]", "");
            word = word.Replace("\"", "");
            callback(word);
        }
        else{
            Debug.Log(request.error);
            callback("this is an error");
        }
        
       
    }
    
}
