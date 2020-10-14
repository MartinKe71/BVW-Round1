using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBunnyAnimation : MonoBehaviour
{
    public float timeScale = 0.5f;
    public Camera cam;
    private void Start()
    {
        Time.timeScale = timeScale;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            cam.GetComponent<VisionEnabled>().ChangeVision(5f);
        } 
    }
}
