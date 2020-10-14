    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using TMPro;

public class QTE : MonoBehaviour
{
    public GameObject displayBox;
    public ChangeCamera levelTrigger;
    public int QTEGen;
    public float timeToClick = 1f;
    public GameObject KeyFillBack;
    public GameObject KeyFillContent;
    public bool WaitingForKey = true;

    //click effect gameobject
    public GameObject passEffect;
    public GameObject failEffect;

    [Header("For Tutorial Only")]
    public bool forTutorial = false;
    public GameObject tutorialAfterQTE;
    public float freezeTime;


    [Header("Two-key QTE")]
    public bool twoKeyQTE = false;

    private bool isCorrect;
    private bool isPressed = false;
    private float timeLeft;

    //sound
    public AudioClip pass;
    public AudioClip fail;
    public AudioSource UISound;

    void Start()
    {
        KeyFillBack.SetActive(true);
        KeyFillContent.SetActive(true);
        timeLeft = timeToClick;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime / Time.timeScale;
        KeyFillContent.GetComponent<Image>().fillAmount = (timeLeft/timeToClick);

        // fill color
        if (timeLeft / timeToClick >= 0.5)
            KeyFillContent.GetComponent<Image>().color = Color.green;
        if (timeLeft / timeToClick < 0.5 && timeLeft / timeToClick > 0.25)
            KeyFillContent.GetComponent<Image>().color = Color.yellow;
        if (timeLeft / timeToClick < 0.25)
            KeyFillContent.GetComponent<Image>().color = Color.red;
        if (timeLeft <= 0)
        {
            levelTrigger.QTEStatus(false);
            QTEFail();
            isPressed = true;
        }

        if (gameObject.activeSelf)
        {
            // Generate the next key for guest to press
            if (WaitingForKey == true)
            {
                WaitingForKey = false;
                int upper = twoKeyQTE ? 5 : 3;
                int lower = twoKeyQTE ? 3 : 0;
                QTEGen = Random.Range(lower, upper);
                //start coroutine
                if (QTEGen == 0)
                {
                    displayBox.GetComponent<TextMeshProUGUI>().text = "Q";
                }
                if (QTEGen == 1)
                {
                    displayBox.GetComponent<TextMeshProUGUI>().text = "W";
                }
                if (QTEGen == 2)
                {
                    displayBox.GetComponent<TextMeshProUGUI>().text = "E";
                }
                if (QTEGen == 3)
                {
                    displayBox.GetComponent<TextMeshProUGUI>().text = "Q+W";
                }
                if (QTEGen == 4)
                {
                    displayBox.GetComponent<TextMeshProUGUI>().text = "W+E";
                }
            }

            // If the key is Q
            if (QTEGen == 0)
            {
                if (Input.anyKeyDown)
                {
                    if (Input.GetKey(KeyCode.Q))
                    {
                        isCorrect = true;
                        KeyPressing();
                    }
                    else if (Input.GetMouseButtonDown(0))
                    {
                        return;
                    }
                    else
                    {
                        isCorrect = false;
                        KeyPressing();
                    }
                }
            }
            // If the key is W
            else if (QTEGen == 1)
            {
                if (Input.anyKeyDown)
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        isCorrect = true;
                        KeyPressing();
                    }
                    else if (Input.GetMouseButtonDown(0))
                    {
                        return;
                    }
                    else
                    {
                        isCorrect = false;
                        KeyPressing();
                    }
                }
            }
            // If the key is E
            else if (QTEGen == 2)
            {
                if (Input.anyKeyDown)
                {
                    if (Input.GetKey(KeyCode.E))
                    {
                        isCorrect = true;
                        KeyPressing();
                    }
                    else if (Input.GetMouseButtonDown(0))
                    {
                        return;
                    }
                    else
                    {
                        isCorrect = false;
                        KeyPressing();
                    }
                }
            }
            // If the key is Q W
            else if (QTEGen == 3)
            {
                if (Input.anyKeyDown)
                {
                    if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.W))
                    {
                        isCorrect = true;
                        KeyPressing();
                    }
                    else if (Input.GetMouseButtonDown(0))
                    {
                        return;
                    }
                    else
                    {
                        isCorrect = false;
                        KeyPressing();
                    }
                }
            }
            // If the key is W E
            else if (QTEGen == 4)
            {
                if (Input.anyKeyDown)
                {
                    if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.E))
                    {
                        isCorrect = true;
                        KeyPressing();
                    }
                    else if (Input.GetMouseButtonDown(0))
                    {
                        return;
                    }
                    else
                    {
                        isCorrect = false;
                        KeyPressing();
                    }
                }
            }

            if (isPressed)
            {
                DeactivateQTE();
            }
        }
    }

    void KeyPressing()
    {
        Debug.Log("key pressing");
        if(isCorrect)
        {
            // Tell guest the input is correct
            QTEPass();
            levelTrigger.QTEStatus(true);
            isPressed = true;
            if (forTutorial)
            {
                DisplayPreQTETutorial();
            }
        }
        //else
        //{
        //    // Tell guest the input is incorrect
        //    levelTrigger.QTEStatus(false);
        //    isPressed = true;
        //}
    }

    public void DeactivateQTE()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        KeyFillContent.GetComponent<Image>().fillAmount = 1;
        KeyFillBack.SetActive(false);
        KeyFillContent.SetActive(false);
        displayBox.GetComponent<TextMeshProUGUI>().text = "";
        if (!forTutorial) Destroy(gameObject);
    }

    public void DisplayPreQTETutorial()
    {
        Time.timeScale = 0;
        StartCoroutine(ResetTimeScale());
        tutorialAfterQTE.GetComponent<PlayableDirector>().Play();
    }

    public void QTEPass()
    {
        UISound.clip = pass;
        UISound.volume = 0.4f;
        UISound.Play();
        if (!passEffect.activeSelf) passEffect.SetActive(true);
        else
        {
            passEffect.GetComponent<ParticleSystem>().Stop();
            passEffect.GetComponent<ParticleSystem>().Play();
        }
    }

    public void QTEFail()
    {
        UISound.clip = fail;
        UISound.volume = 0.4f;
        UISound.Play();
        if (!failEffect.activeSelf) failEffect.SetActive(true);
        else
        {
            failEffect.GetComponent<ParticleSystem>().Stop();
            failEffect.GetComponent<ParticleSystem>().Play();
        }
    }

    IEnumerator ResetTimeScale()
    {
        yield return new WaitForSecondsRealtime(freezeTime);
        Time.timeScale = levelTrigger.effectTimeScale;
        Debug.Log("Timescale changed");
        Destroy(gameObject);
    }
}