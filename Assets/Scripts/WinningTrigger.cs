using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningTrigger : MonoBehaviour
{
    //public GameObject nextTarget;

    [SerializeField] GameObject nextTarget;

    public GameObject winScreen;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player") && nextTarget == null)
        {
            winScreen.SetActive(true);
        }
        else
        {
            //other.GetComponent<Bunny>().Destination = nextTarget.transform;
        }
    }
}
