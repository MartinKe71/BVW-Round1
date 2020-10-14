using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ChangeCamera : MonoBehaviour
{
    [Header("States")]
    public float countDownTime = 10f;
    public float effectTimeScale = 0.2f;
    public bool inVision;
    public float curTime = 0f;
    public bool interactiveSelected;

    [Header("Cameras")]
    public Camera bunnyCam;
    public Camera ghostCam;
    public GameObject ghostTimelineObject;

    [Header("UI Visuals")]
    public TextMeshProUGUI countDownText;
    public GameObject countDownImage;
    public GameObject postProcessing;
    public float vignetteHoldTime;
    private Vignette vignette;

    [Header("QTE System and States")]
    public GameObject qtePrefab;
    public GameObject displayBox;
    public GameObject KeyFillBack;
    public GameObject KeyFillContent;
    public GameObject passEffect;
    public GameObject failEffect;
    public GameObject JumpQTE;
    public float timeToClick = 3f;
    public bool qteSuccess = true;
    public bool qteFinish = false;
    public bool qteTwoKey = false;

    // The Ghost gameobject
    private GameObject ghost;
    // The mesh renderer of the ghost
    public MeshRenderer ghostRender;
    // The qte system that will be instantiated
    private GameObject qteSystem;
    // The timeline controlling ghost movement
    private PlayableDirector ghostTimeline;

    // sound
    public AudioSource UISound;
    public AudioClip pass;
    public AudioClip fail;
    public AudioClip timeChange;

    void Start()
    {
        ghost = ghostCam.transform.parent.gameObject;
        ghostRender = ghost.GetComponentInChildren<MeshRenderer>();
        ghostTimeline = ghostTimelineObject.GetComponent<PlayableDirector>();
        postProcessing.GetComponent<Volume>().profile.TryGet(out vignette);
        UISound = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("Player entered");
            UISound.clip = timeChange;
            UISound.volume = 1f;
            UISound.Play();
            ChangeTimeScaleToNormal(false);
            ChangeCameraToGhost(true);
            
            ghostTimeline.Play();

            if (qtePrefab != null)
            {
                qteSystem = Instantiate(qtePrefab);
                QTE qte = qteSystem.GetComponent<QTE>();
                qte.levelTrigger = gameObject.GetComponent<ChangeCamera>();
                qte.displayBox = displayBox;
                qte.KeyFillContent = KeyFillContent;
                qte.KeyFillBack = KeyFillBack;
                qte.timeToClick = timeToClick;
                qte.passEffect = passEffect;
                qte.failEffect = failEffect;
                qte.twoKeyQTE = qteTwoKey;

                qte.UISound = UISound;
                qte.pass = pass;
                qte.fail = fail;
            }
        }
    }

    public void ChangeTimeScaleToNormal(bool change)
    {
        Time.timeScale = change? 1f : effectTimeScale;
    }

    public void ChangeCameraToGhost(bool change)
    {
        Debug.Log("Change camera");
        inVision = true;
        bunnyCam.gameObject.SetActive(!change);
        ghostCam.gameObject.SetActive(change);
        ghost.GetComponent<GhostController>().enabled = change;
        ghost.GetComponent<GhostController>().levelTrigger = gameObject.GetComponent<ChangeCamera>();
    }

    IEnumerator EndEffectCountDown()
    {
        // Reset states
        curTime = 0f;
        if (this.tag == "Mushroom") JumpQTE.SetActive(true);
        while (qteSuccess && !interactiveSelected && curTime < countDownTime)
        {
            yield return new WaitForSecondsRealtime(Time.deltaTime);
            curTime += Time.deltaTime / Time.timeScale;
            float remainTime = Mathf.Floor(10 * (countDownTime - curTime)) / 10;
            vignette.intensity.value = curTime / countDownTime;
            // Update remaining time
            countDownImage.GetComponent<Image>().fillAmount = remainTime / countDownTime;
            countDownText.text = "Time Remaining: " + remainTime.ToString();
            countDownText.gameObject.SetActive(true);
        }
        ghostTimeline.Pause();
        inVision = false;
        vignette.intensity.value = 0f;
        countDownText.gameObject.SetActive(false);
        ChangeTimeScaleToNormal(true);
        ChangeCameraToGhost(false);
        InteractiveSelected(false);
        countDownImage.SetActive(false);
        JumpQTE.SetActive(false);
    }

    public void InteractiveSelected(bool state)
    {
        interactiveSelected = state;
    }

    public void QTEStatus(bool status)
    {
        qteFinish = true;
        qteSuccess = status;
        StartCoroutine(EndEffectCountDown());
    }

}
