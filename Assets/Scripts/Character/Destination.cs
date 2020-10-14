using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    public GameObject winScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player")) {
            if (other.GetComponent<Bunny>().current == other.GetComponent<Bunny>().Destinations.Length - 1)
            {
                other.GetComponent<Bunny>().BunnyInitSpeed = 0;
                winScreen.SetActive(true);
            }
            other.GetComponent<Bunny>().changeDest();
        }
    }
}
