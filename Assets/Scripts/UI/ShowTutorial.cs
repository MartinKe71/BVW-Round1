using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTutorial : MonoBehaviour
{
    public GameObject tutorial1;
    public GameObject tutorial2;
    public GameObject nextButton;
    public GameObject previousButton;

    public void nextTutorial()
    {
        tutorial1.SetActive(false);
        tutorial2.SetActive(true);
        nextButton.SetActive(false);
        previousButton.SetActive(true);
    }

    public void previousTutorial()
    {
        tutorial1.SetActive(true);
        tutorial2.SetActive(false);
        nextButton.SetActive(true);
        previousButton.SetActive(false);
    }
}
