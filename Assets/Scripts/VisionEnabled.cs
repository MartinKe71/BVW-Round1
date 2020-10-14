using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[System.Serializable]
public class FreeLookParams
{
    public float midRigRadius;
    public float midRigHeight;
    public float xValue;
    public Vector2 xRange;
    public float yValue;
    public string yAxisName;
    public string xAxisName;
    public float fieldOfView;
}

public class VisionEnabled : MonoBehaviour
{
    public CinemachineFreeLook freeLook;
    public float effectTimeScale = 0.2f;
    public float curTime = 0f;

    [Header("Audio Clip")]

    [Header("Tweaking the camera parameters in ghost effect")]
    public float cameraTransitionTime;
    public float timeElapsed = 0f;
    public FreeLookParams cameraInit;
    public FreeLookParams cameraGoal;


    private Camera cam;
    
    public bool visionChanged = false;
    // set to true for testing purposes, will change back to false later
    public bool interactionSelected = false;
    public bool qteSuccess = true;
    public bool qteFinish = false;

    // ghost gameObject
    public GameObject ghost;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) interactionSelected = true;
    }

    public void InteractiveSelected()
    {
        interactionSelected = true;
    }

    public void ResetQTEStatus()
    {
        qteFinish = false;
        qteSuccess = true;
    }

    public void QTEStatus(bool status)
    {
        qteFinish = true;
        qteSuccess = status;
    }

    public void ChangeVision(float durationTime)
    {
        //ghost pop up
        ghost.SetActive(false);
        //Debug.Log("ghost!!!!!!!!!");
        // Change Camera Renderer
        UnityEngine.Rendering.Universal.UniversalAdditionalCameraData additionalCameraData = cam.transform.GetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>();
        additionalCameraData.SetRenderer(visionChanged ? 0 : 1);
        additionalCameraData.renderPostProcessing = !visionChanged;
        
        // Change time scale
        Time.timeScale = visionChanged ? 1 : effectTimeScale;

        // Start end vision coroutine count down
        if (durationTime>0) StartCoroutine(EndVision(durationTime));

        // Change camera position
        visionChanged = !visionChanged;
        StartCoroutine(ChangeCamera());
        //Debug.Log("Vision Changed!");
        //Debug.Log("interactiveSelected: " + interactionSelected);
        //Debug.Log("QTEFinish: " + qteFinish);
        //Debug.Log("QTEStatus: " + qteSuccess);
        //Debug.Log("in vision?: " + visionChanged);
    }

    IEnumerator EndVision(float durationTime)
    {
        //ghost pop up
        ghost.SetActive(true);
        curTime = 0f;
        // The time countdown will keep going if the qte succeeded
        // it will stop if the qte failed
        //while (!interactionSelected && curTime < durationTime)
        //Debug.Log("End Vision----------");
        //Debug.Log("qteSuccess: " + qteSuccess);
        //Debug.Log("interactionSelected: " + interactionSelected);
        //Debug.Log("curtime < durationTime: " + (curTime < durationTime));
        while (qteSuccess && !interactionSelected && curTime < durationTime)
        {
            yield return new WaitForEndOfFrame();
            curTime += Time.deltaTime;
        }
        ChangeVision(0);
        interactionSelected = false;
    }

    //public void PostFadeAnimation()
    //{
    //    if (!visionChanged)
    //    {
    //        Time.timeScale = effectTimeScale;
    //    }
    //    visionChanged = !visionChanged;
    //}

    IEnumerator ChangeCamera()
    {
        //Debug.Log("start change camera ------------------------");
        //Debug.Log("visionchanged: " + visionChanged);
        FreeLookParams goal = visionChanged ? cameraGoal : cameraInit;
        timeElapsed = 0f;
        freeLook.m_XAxis.m_InputAxisName = goal.xAxisName;
        freeLook.m_YAxis.m_InputAxisName = goal.yAxisName;
        while (timeElapsed < cameraTransitionTime)
        {
            freeLook.m_Lens.FieldOfView = Mathf.Lerp(freeLook.m_Lens.FieldOfView, goal.fieldOfView, timeElapsed/cameraTransitionTime);
            freeLook.m_XAxis.Value = Mathf.Lerp(freeLook.m_XAxis.Value, goal.xValue, timeElapsed/cameraTransitionTime);
            freeLook.m_XAxis.m_MinValue = Mathf.Lerp(freeLook.m_XAxis.m_MinValue, goal.xRange.x, timeElapsed / cameraTransitionTime);
            freeLook.m_XAxis.m_MaxValue = Mathf.Lerp(freeLook.m_XAxis.m_MaxValue, goal.xRange.y, timeElapsed / cameraTransitionTime);
            freeLook.m_YAxis.Value = Mathf.Lerp(freeLook.m_YAxis.Value, goal.yValue, timeElapsed / cameraTransitionTime);

            yield return new WaitForSecondsRealtime(Time.deltaTime);
            timeElapsed += Time.deltaTime/Time.timeScale;
        }
    }
}
