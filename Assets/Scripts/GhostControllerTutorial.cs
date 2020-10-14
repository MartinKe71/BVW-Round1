using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostControllerTutorial : MonoBehaviour
{
    public static string selectedObject;
    public RaycastHit theObject;
    public Camera cam;
    public GameObject tutorialPart3;

    public int layerMask;
    private VisionEnabled visionEffect;

    // Start is called before the first frame update
    void Start()
    {
        visionEffect = GetComponent<VisionEnabled>();
        layerMask = ~Physics.IgnoreRaycastLayer & ~(1 << 13);
    }

    // Update is called once per frame
    void Update()
    {
        // we need to disable raycasting while the qte is going on (qtefinish is false)
        // we must disable raycating if the qte failed (qtesuccess is false)
        // we can use raycasting only if interaction object has not been selected
        //if (!visionEffect.interactionSelected && visionEffect.visionChanged)
        if (visionEffect.qteFinish && visionEffect.qteSuccess && !visionEffect.interactionSelected && visionEffect.visionChanged)
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

                    if(objectHit.tag == "Object")
                        objectHit.gameObject.GetComponent<ObjectEvent>().CreatePreview();
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (objectHit.gameObject.transform.parent != null)
                        {
                            //Debug.Log("Animate in parent");
                            objectHit.gameObject.GetComponentInParent<Animator>().SetBool("Play", true);
                        }
                        else
                        {
                            //Debug.Log("Animate in the gameobject");
                            objectHit.gameObject.GetComponent<Animator>().SetBool("Play", true);
                        }
                        objectHit.gameObject.GetComponent<ObjectEvent>().destroyPreview();
                        objectHit.gameObject.GetComponent<Rigidbody>().useGravity = true;
                        objectHit.gameObject.GetComponent<AudioSource>().Play();
                        cam.GetComponent<VisionEnabled>().InteractiveSelected();
                    }

                }
            }
        }

    }
}
