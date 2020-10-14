using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public float MoveSpeed = 4;
    public float CamSensitivity = 0.5f;
    public Vector3 initialPosition;
    public Quaternion initialRotation;
    public Vector3 effectPosition;
    public Quaternion effectRotation;

    [Header("Raycastings")]
    public static string selectedObject;
    public int layerMask;
    public Camera cam;
    public ChangeCamera levelTrigger;



    // Start is called before the first frame update
    void OnAwake()
    {
        // Set initial values
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    private void OnEnable()
    {
        cam = GetComponentInChildren<Camera>();
        layerMask = ~Physics.IgnoreRaycastLayer & ~(1 << 13);
        transform.localPosition = effectPosition;
        transform.localRotation = effectRotation;
    }

    private void OnDisable()
    {
        transform.localPosition = initialPosition;
        transform.localRotation = initialRotation;
    }
    // Update is called once per frame
    void Update()
    {
        // do raycast
        DoRaycastOnMouse();

        // Get User Input
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        float mouseX = Input.GetAxisRaw("Mouse X");

        //this.transform.position += (transform.forward * vertical + transform.right * horizontal) * MoveSpeed * Time.deltaTime / Time.timeScale;
        //this.transform.Rotate(Vector3.up, horizontal * CamSensitivity);
    }

    public void DoRaycastOnMouse()
    {
        if (levelTrigger.qteFinish && levelTrigger.qteSuccess && !levelTrigger.interactiveSelected && levelTrigger.inVision)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                Transform objectHit = hit.transform;
                selectedObject = objectHit.transform.gameObject.name;
                // Do something with the object that was hit by the raycast.

                // Play Tutorial

                if (objectHit.gameObject.layer == 9)
                {

                    if (objectHit.tag == "Object")
                        objectHit.gameObject.GetComponent<ObjectEvent>().CreatePreview();
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (objectHit.gameObject.transform.parent != null)
                        {
                            Debug.Log("Animate in parent");
                            objectHit.gameObject.GetComponentInParent<Animator>().SetBool("Play", true);
                        }
                        else
                        {
                            Debug.Log("Animate in the gameobject");
                            objectHit.gameObject.GetComponent<Animator>().SetBool("Play", true);
                        }
                        objectHit.gameObject.GetComponent<ObjectEvent>().destroyPreview();
                        objectHit.gameObject.GetComponent<Rigidbody>().useGravity = true;
                        objectHit.gameObject.GetComponent<AudioSource>().Play();
                        levelTrigger.InteractiveSelected(true);
                    }

                }
            }
        }
    }
}
