using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorD_LoadingScene : MonoBehaviour
{

    [SerializeField] Text loadingText;
    private string baseText = "";  // This sha remains constant
    int dotCount = 0;  // Number of dots added
    float timeBetweenDots = 1f;  // Time delay between adding a dot (1 second)
    
    [SerializeField] GameObject gameScene;
   

    void Start()
    {
        // Start the coroutine to add dots to the loading text
        StartCoroutine(UpdateLoadingText());
        float invokeDelay = Random.Range(4, 7);
        Invoke("LoadGameScene", invokeDelay);
    }

    IEnumerator UpdateLoadingText()
    {
        while (true)
        {
            // Wait for the specified time before adding a dot
            yield return new WaitForSeconds(timeBetweenDots);

            // Add a dot based on the current dot count
            dotCount++;

            // Update the text by appending dots
            loadingText.text = baseText + new string('.', dotCount);

            // If there are 3 dots, reset the count back to 1
            if (dotCount >= 3)
            {
                dotCount = 0;  // Reset to 1
            }
        }
    }

    public void LoadGameScene()
    {
        gameScene.SetActive(true);
        this.gameObject.SetActive(false);
    }
}


