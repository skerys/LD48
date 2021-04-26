using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ConsoleScreen : MonoBehaviour
{
    public static ConsoleScreen instance;

    public TMP_Text textElement;
    public float timeBetweenLetters;
    public GameObject infoInConsole;

    string messageBacklog;
    float fadeOutTimer = -200f;

    public bool isWriting = false;

    Coroutine currentCoroutine;


    public void StartMessage(string message)
    {
        if(!textElement.gameObject.activeInHierarchy) infoInConsole.SetActive(true);
        
        Clear();
        textElement.gameObject.SetActive(true);

        messageBacklog = message;

        if(currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(TypeWrite());
        isWriting = true;
    }

    IEnumerator TypeWrite()
    {
        foreach(char c in messageBacklog)
        {
            if(c == '@')
            {
            yield return new WaitForSeconds(timeBetweenLetters * 3f);
            }
            else
            {
                textElement.text += c;
                yield return new WaitForSeconds(timeBetweenLetters);
            }
                
        }
        isWriting = false;
    }

    public void Clear()
    {
        textElement.text = "";
    }

    void Update()
    {
    
    }

}
