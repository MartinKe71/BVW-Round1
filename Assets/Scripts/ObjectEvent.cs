using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEvent : MonoBehaviour
{
    public GameObject PreviewObject;
    public float destroyTime = 3;
    private bool spawned = false;
    private GameObject instantiatedPreview;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = 9;
        foreach(Transform child in gameObject.transform)
        {
            child.gameObject.layer = 9;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreatePreview()
    {
        if (!spawned)
        {
            instantiatedPreview = Instantiate(PreviewObject, this.transform.parent.position, this.transform.parent.rotation);
            instantiatedPreview.GetComponent<Animator>().speed = 1 / Time.timeScale;
            spawned = true;
        }
    }

    public void destroyPreview()
    {
        if (instantiatedPreview != null) Destroy(instantiatedPreview);
    }

    public void OnCollisionEnter(Collision collision)
    {
        
        if (collision.transform.CompareTag("Enemy"))
        {
            Debug.Log("Tree collided with: " + collision.transform.name);
            Debug.Log("Going to destory");
            StartCoroutine(SelfDestroy());
        }
    }

    public void OnCollisionExit(Collision collision)
    {

        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("exit: " + collision.transform.name);
            this.GetComponent<AudioSource>().Play();
        }
    }

    public void DestoryObject()
    {
        StartCoroutine(SelfDestroy());
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSecondsRealtime(destroyTime);
        Destroy(transform.parent.gameObject);
    }
}
