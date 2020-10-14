using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class BounceUp : MonoBehaviour
{
    public Image JumpQteFill;
    public float upMultiplier;
    public float forwardMultiplier;

    public Transform destination;
    public float speed;

    private Transform bunny;

    private void Update()
    {
        upMultiplier = JumpQteFill.fillAmount * 10;
        Debug.Log("upMultiplier: " + upMultiplier);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with something: " + collision.transform.name);
        if (collision.transform.CompareTag("Player"))
        {
            bunny = collision.transform;

            Debug.Log("Collided with player: " + bunny.name);
            Debug.Log("up tranform is: " + bunny.up);
            speed = bunny.GetComponent<Bunny>().BunnyInitSpeed;
            destination = bunny.GetComponent<Bunny>().currentDestination;
            Debug.Log("destination: " + destination.position);
            bunny.GetComponent<Bunny>().enabled = false;
            bunny.GetComponent<Rigidbody>().velocity = transform.up * upMultiplier - transform.right * speed * forwardMultiplier;
            StartCoroutine(RestoreNavMeshAgent(bunny));
        }
    }

    IEnumerator RestoreNavMeshAgent(Transform bunny)
    {
        yield return new WaitForSeconds(1f);
        bunny.GetComponent<Bunny>().enabled = true;
        //bunny.GetComponent<NavMeshAgent>().speed = speed;
        //bunny.GetComponent<NavMeshAgent>().enabled = true;

    }

}
