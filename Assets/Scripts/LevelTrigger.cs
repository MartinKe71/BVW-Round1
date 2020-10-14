using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    [SerializeField] Camera Cam;
    public float effectDuration = 5f;

    [Header("QTE Object and Parameters")]
    public GameObject qtePrefab;
    public GameObject displayBox;
    public GameObject KeyFillBack;
    public GameObject KeyFillContent;

    [Header("For Tutorial Only")]
    public bool forTutorial = false;
    public GameObject tutorialAfterQTE;
    public float freezeTime;

    private GameObject qteSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Cam.GetComponent<VisionEnabled>().ResetQTEStatus();
            Cam.GetComponent<VisionEnabled>().ChangeVision(effectDuration);          
            if (qtePrefab != null)
            {
                qteSystem = Instantiate(qtePrefab);
                QTE qte = qteSystem.GetComponent<QTE>();
                qte.displayBox = displayBox;
                qte.KeyFillContent = KeyFillContent;
                qte.KeyFillBack = KeyFillBack;
                qte.timeToClick = 0.5f;
                // Settings for tutorial only
                qte.forTutorial = forTutorial;
                qte.tutorialAfterQTE = tutorialAfterQTE;
                qte.freezeTime = freezeTime;
                Debug.Log("Activate QTE");
            }
        }
        //else if (other.transform.CompareTag("Enemy"))
        //{
        //    other.transform.GetComponent<Animator>().SetTrigger("Attack");
        //}

    }

    // Has been replaced by an internal counter in QTE System
    //private void Update()
    //{
    //    if (timeDone == 1)
    //    {
    //        qteObject.SetActive(false);
    //        Debug.Log("Deactivated QTE System");
    //    }
    //}
}
