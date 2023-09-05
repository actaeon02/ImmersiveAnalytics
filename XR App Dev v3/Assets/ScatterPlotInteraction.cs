using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterplotInteraction : MonoBehaviour
{
    private bool labelVisible = false;
    private GameObject currentPoint;

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch) ||
            OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("DataPoint"))
                {
                    currentPoint = hit.collider.gameObject;
                    ShowLabel(currentPoint.transform.position);
                }
            }
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch) ||
                 OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            HideLabel();
        }
    }

    private void ShowLabel(Vector3 position)
    {
        if (!labelVisible)
        {
            // Show label at the data point's position
            // You can instantiate a label UI element here
            labelVisible = true;
        }
    }

    private void HideLabel()
    {
        if (labelVisible)
        {
            // Hide the label
            // You can destroy the label UI element here
            labelVisible = false;
        }
    }
}