using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowButton : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    public RectTransform toSlide;
    bool isClicked = false;
    int slideSpeed = 5;
    int yDown = 25;
    int yUp = 75;
    public void slide(){
        // if not clicked, slide up if clicked, slide down
        // rotate arrow
        if (isClicked){
            // use rect transform to move
            toSlide.anchoredPosition = new Vector2(toSlide.anchoredPosition.x, yDown);
            arrow.transform.Rotate(0, 0, 180);
            isClicked = false;

        } else {
            toSlide.anchoredPosition = new Vector2(toSlide.anchoredPosition.x, yUp);
            arrow.transform.Rotate(0, 0, 180);
            isClicked = true;
        }

    }
}
