using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class TutorialPart3Trigger : MonoBehaviour
{
    public PlayableDirector tutorialPart3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Object"))
        {
            tutorialPart3.Play();
        }
    }
}
