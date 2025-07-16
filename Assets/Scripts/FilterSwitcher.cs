using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;


public class FilterSwitcher : MonoBehaviour
{
    public List<GameObject> filters; // Assign filters manually or auto-load from children
    private int currentIndex = 0;

    private Vector2 touchStartPos;
    private bool isSwiping = false;
    private float swipeThreshold = 50f; // Minimum swipe distance in pixels

    public List<Sprite> previewSprites; // PNG images of filters
    public Image previewImageRight;
    public Image previewImageLeft;
    public CanvasGroup leftGroup;
    public CanvasGroup rightGroup;
    public float fadeDuration = 0.3f;

    public TextMeshProUGUI filterNameText; // Text to display the filter name

    void Start()
    {
        ShowOnly(currentIndex);
        filterNameText.text = filters[currentIndex].name;

       

    }

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        // Ignore if touching UI
        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            return;
        filterNameText.text = filters[currentIndex].name; // Update filter name text
        switch (touch.phase)
        {
            case TouchPhase.Began:
                touchStartPos = touch.position;
                isSwiping = true;
                break;

            case TouchPhase.Moved:
                // Optional: Can check for swipe in real-time here
                break;

            case TouchPhase.Ended:
                if (!isSwiping) return;

                Vector2 touchEndPos = touch.position;
                float deltaX = touchEndPos.x - touchStartPos.x;

                if (Mathf.Abs(deltaX) > swipeThreshold)
                {
                    if (deltaX > 0)
                        SwitchToPrevious(); // Swiped right
                    else
                        SwitchToNext();     // Swiped left
                }

                isSwiping = false;
                break;
        }
    }

    void SwitchToNext()
    {
        currentIndex = (currentIndex + 1) % filters.Count;
        ShowOnly(currentIndex);
        filterNameText.text = filters[currentIndex].name;
    }

    void SwitchToPrevious()
    {
        currentIndex = (currentIndex - 1 + filters.Count) % filters.Count;
        ShowOnly(currentIndex);
        filterNameText.text = filters[currentIndex].name;
    }

    void ShowOnly(int index)
    {
        for (int i = 0; i < filters.Count; i++)
        {
            filters[i].SetActive(i == index);
        }
        // Set Right Preview Image (next filter)
        if (previewSprites != null && previewSprites.Count > 0 && previewImageRight != null)
        {
            int rightIndex = (index + 1) % previewSprites.Count;
            previewImageRight.sprite = previewSprites[rightIndex];
        }

        // Set Left Preview Image (previous filter)
        if (previewSprites != null && previewSprites.Count > 0 && previewImageLeft != null)
        {
            int leftIndex = (index - 1 + previewSprites.Count) % previewSprites.Count;
            previewImageLeft.sprite = previewSprites[leftIndex];
        }
   
        //animations
        if (rightGroup != null)
            StartCoroutine(FadeIn(rightGroup));
        if (leftGroup != null)
            StartCoroutine(FadeIn(leftGroup));
    }
    IEnumerator FadeIn(CanvasGroup group)
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            group.alpha = Mathf.Lerp(0f,1, t/fadeDuration);
            yield return null;
        }
        group.alpha = 1f;
    }

}
