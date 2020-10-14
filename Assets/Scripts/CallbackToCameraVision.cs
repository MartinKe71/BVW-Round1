using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbackToCameraVision : MonoBehaviour
{
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
    }

    //public void FadeOutFinished()
    //{
    //    cam.GetComponent<VisionEnabled>().PostFadeAnimation();
    //}
}
