using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject transitionImage;
    public GameObject blackScreen;
    public float fadeOutTime = 3f;
    public float holdOnTime = 2f;
    public float curTime = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0;
            StartCoroutine(ChangeScene());
        }
    }

    IEnumerator ChangeScene()
    {

        while (curTime < fadeOutTime)
        {
            audioSource.volume -= 0.01f;
            var tempColor = transitionImage.GetComponent<Image>().color;
            tempColor.a = curTime/fadeOutTime;
            transitionImage.GetComponent<Image>().color = tempColor;
            tempColor = blackScreen.GetComponent<Image>().color;
            tempColor.a = curTime / fadeOutTime;
            blackScreen.GetComponent<Image>().color = tempColor;
            yield return new WaitForSecondsRealtime(0.1f);
            curTime += 0.1f;
        }
        yield return new WaitForSecondsRealtime(holdOnTime);
        curTime = 0f;
        while (curTime < 1f)
        {
            var tempColor = transitionImage.GetComponent<Image>().color;
            tempColor.a = (1-curTime) / fadeOutTime;
            transitionImage.GetComponent<Image>().color = tempColor;
            yield return new WaitForSecondsRealtime(0.1f);
            curTime += 0.1f;
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
