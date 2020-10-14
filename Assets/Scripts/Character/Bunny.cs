using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : MonoBehaviour
{
    public Transform[] Destinations;
    public Transform currentDestination;
    //public Transform Destination;
    public float BunnyInitSpeed = 7f;
    public float jumptime = 1f;
    public GameObject loseUI;
    public int current = 0;
    //private UnityEngine.AI.NavMeshAgent agent;
    private float curTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        currentDestination = Destinations[0];
        GetComponent<Animator>().SetFloat("MoveSpeed", 0.8f);
    }

    void Update()
    {
        if (transform.position != Destinations[current].position)
        {
            //Vector3 pos = Vector3.MoveTowards(transform.position, Destinations[current].position, BunnyInitSpeed * Time.deltaTime);
            //GetComponent<Rigidbody>().MovePosition(pos);
            
            currentDestination = Destinations[current].transform;
            //gameObject.transform.LookAt(currentDestination.position);
            Vector3 direction = currentDestination.position - transform.position;
            GetComponent<Rigidbody>().velocity = direction.normalized * BunnyInitSpeed;
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime);


            //gameObject.transform.LookAt(currentDestination.position);
        }
        //gameObject.transform.LookAt(Destination.position);
        //agent.SetDestination(Destination.position);
    }

    public void GotCaught()
    {
        this.gameObject.GetComponent<Animator>().SetBool("Die", true);
        Time.timeScale = 0;
        loseUI.SetActive(true);
        
    }

    public void changeDest()
    {
        current = (current + 1) % Destinations.Length;
    }
}
