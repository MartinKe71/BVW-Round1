using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public Animator monsterAnim;
    public AudioSource AS;
    public float objectDeceleration = 1f;

    private bool decelerated = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            //monsterAnim.SetTrigger("Attack");
            Debug.Log("attack!");
            other.gameObject.GetComponent<Bunny>().GotCaught();
            this.gameObject.GetComponentInParent<UnityEngine.AI.NavMeshAgent>().speed = 0;
            AS.Play();
        }

        if (!decelerated && other.transform.tag == "Object")
        {
            Debug.Log("hitted!!!");
            decelerated = true;
            GetComponentInParent<UnityEngine.AI.NavMeshAgent>().speed -= objectDeceleration;
            GetComponentInParent<ChaseTarget>().monsterAcceleration += 0.3f;
            if (other.TryGetComponent<ObjectEvent>(out ObjectEvent objectEvent))
            {
                objectEvent.DestoryObject();
            }
            StartCoroutine(ResetBoolean());
        }
    }

    IEnumerator ResetBoolean()
    {
        yield return new WaitForSecondsRealtime(1f);
        decelerated = false;
    }
}
