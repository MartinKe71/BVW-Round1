using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpQTE : MonoBehaviour
{
    public float qteFillSpeed = 0.3f;
    public AudioSource AS;
    private float fillAmount = 0;
    private float timeThresHold = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            AS.Play();
            fillAmount += qteFillSpeed;
        }
        timeThresHold += (Time.deltaTime * 5);

        if(timeThresHold > 0.05)
        {
            timeThresHold = 0;
            fillAmount -= 0.02f;
        }

        if(fillAmount < 0)
        {
            fillAmount = 0;
        }

        if(fillAmount > 1)
        {
            Debug.Log("succeed!!!!");
        }

        GetComponent<Image>().fillAmount = fillAmount;
    }

    public void OnEnable()
    {
        fillAmount = 0;
        GetComponent<Image>().fillAmount = fillAmount;
    }
}
