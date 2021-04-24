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

    string messageBacklog;
    float fadeOutTimer = -200f;

    [ContextMenu("Test Console")]
    public void MessageTest()
    {
        StartMessage("Engaging\n Jupiter\n atmosphere@.@.@.\n\n\nYour mom's a hoe");
    }

    public void StartMessage(string message)
    {
        textElement.gameObject.SetActive(true);

        messageBacklog = message;

        StartCoroutine(TypeWrite());
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
    }

    void Update()
    {
    
    }

}
