using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTarget : MonoBehaviour
{
    public Transform Destination;
    public float monsterInitSpeed = 5f;
    public float maxSpeed = 10f;
    public float monsterAcceleration = 0.01f;
    private UnityEngine.AI.NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = monsterInitSpeed;
        GetComponent<Animator>().SetFloat("MoveSpeed", 0.8f);
    }

    void Update()
    {
        if (agent.speed < maxSpeed) agent.speed += monsterAcceleration * Time.deltaTime;
        agent.SetDestination(Destination.position);
        transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
    }
    void FixedUpdate()
    {
        gameObject.transform.LookAt(Destination.position);
    }
}
