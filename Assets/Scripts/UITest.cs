using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
    public GameObject passEffect;
    public GameObject failEffect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void QTEPass()
    {
        StartCoroutine(passUI());
    }

    public void QTEFail()
    {
        StartCoroutine(failUI());
    }

    IEnumerator passUI()
    {
        passEffect.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        passEffect.SetActive(false);
    }

    IEnumerator failUI()
    {
        failEffect.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        failEffect.SetActive(false);
    }
}
