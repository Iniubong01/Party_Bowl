using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PB_LoadingScript : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject playButton;
    float loadDuration, elapsedTime;
    bool isPaused = false;

    void Start()
    {
        loadDuration = Random.Range(1f, 4f);  // A faster, yet smooth loading duration
        slider.value = 0;
        playButton.SetActive(false);
        StartCoroutine(FillSliderWithPauses());
    }

    IEnumerator FillSliderWithPauses()
    {
        while (slider.value < 100)
        {
            if (!isPaused)
            {
                // Gradually increase elapsed time for smooth loading
                elapsedTime += Time.deltaTime;

                // Calculate progress, clamping the value between 0 and 100
                float progress = Mathf.Clamp01(elapsedTime / loadDuration) * 100;

                // Update the slider value based on progress
                slider.value = progress;

                // Pause for a noticeable duration (between 0.1 to 0.3 seconds)
                float pauseDuration = Random.Range(0.1f, 0.3f);
                yield return new WaitForSeconds(pauseDuration);  // This gives you the visual break

                // Optional: Add some debug output if you'd like to see the pauses
                Debug.Log($"Paused for {pauseDuration} seconds");
            }
            else
            {
                yield return null;  // If paused, just wait for the next frame
            }
        }

        // When loading completes, show the play button and hide the slider
        playButton.SetActive(true);
        slider.gameObject.SetActive(false);
    }

    public void ClickPlayButton()
    {
        // Reduce the scale of the playButton slightly (e.g., by 10%)
        playButton.transform.localScale *= 0.9f;
    }
}
