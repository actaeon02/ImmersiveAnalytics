using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectVisibility : MonoBehaviour
{
    public Transform cameraTransform;
    public float visibilityDistance = 5f;
    public GameObject tooltip;

    // Update is called once per frame
    void Update()
    {
        float distanceToCamera = Vector3.Distance(transform.position, cameraTransform.position);

        // Toggle the object's visibility
        if (distanceToCamera <= visibilityDistance)
        {
            tooltip.SetActive(true);
        }
        else
        {
            tooltip.SetActive(false);
        }
    }
}
