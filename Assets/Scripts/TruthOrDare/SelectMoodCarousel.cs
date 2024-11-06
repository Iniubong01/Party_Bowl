using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMoodCarousel : MonoBehaviour
{
    public GameObject scrollBar;
    public GameObject dotPrefab;       // Dot prefab reference
    public Transform dotContainer;     // Dot container for holding dots
    public Color activeDotColor = Color.white;
    public Color inactiveDotColor = Color.gray;

    private float scrollPos = 0;
    private float[] pos;
    private Scrollbar scrollbarComponent;
    private List<GameObject> dots = new List<GameObject>();

    void Start()
    {
        // Cache the Scrollbar component to improve performance
        scrollbarComponent = scrollBar.GetComponent<Scrollbar>();

        // Initialize positions based on the number of child items
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;

            // Instantiate a dot for each item and set it in the dot container
            GameObject dot = Instantiate(dotPrefab, dotContainer);
            dots.Add(dot);
        }

        UpdateDotIndicators(0); // Highlight the initial center dot
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // Update scroll position based on scrollbar value when dragging
            scrollPos = scrollbarComponent.value;
        }
        else
        {
            // Snap to the nearest position
            for (int i = 0; i < pos.Length; i++)
            {
                if (scrollPos < pos[i] + (0.5f / (pos.Length - 1)) && scrollPos > pos[i] - (0.5f / (pos.Length - 1)))
                {
                    scrollbarComponent.value = Mathf.Lerp(scrollbarComponent.value, pos[i], 0.1f);
                    UpdateDotIndicators(i); // Update the active dot based on the center item
                }
            }
        }

        // Adjust scale based on closeness to the center
        for (int i = 0; i < pos.Length; i++)
        {
            if (scrollPos < pos[i] + (0.5f / (pos.Length - 1)) && scrollPos > pos[i] - (0.5f / (pos.Length - 1)))
            {
                // Enlarge the centered item
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);

                // Reduce the scale of other items
                for (int j = 0; j < pos.Length; j++)
                {
                    if (j != i) // Only apply this to non-centered items
                    {
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }
            }
        }
    }

    // Updates the dot colors to indicate which item is centered
    private void UpdateDotIndicators(int activeIndex)
    {
        for (int i = 0; i < dots.Count; i++)
        {
            Image dotImage = dots[i].GetComponent<Image>();
            if (i == activeIndex)
            {
                dotImage.color = activeDotColor; // Highlight the active dot
            }
            else
            {
                dotImage.color = inactiveDotColor; // Dim other dots
            }
        }
    }
}
