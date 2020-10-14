using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BouncingButton : MonoBehaviour
{
    public float holdOnSeconds = 3f;
    public Animator animator;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.color = Color.black;
        StartCoroutine(HideButton());
    }

    IEnumerator HideButton()
    {
        yield return new WaitForSecondsRealtime(holdOnSeconds);
        animator.SetBool("Play", true);
        text.color = Color.white;
    }
}
