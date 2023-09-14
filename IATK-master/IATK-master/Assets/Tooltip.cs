using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI tooltipText;
    public GameObject tooltipContainer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowTooltip(Vector3 position, string text)
    {
        tooltipContainer.SetActive(true);
        tooltipContainer.transform.position = position;
        tooltipText.text = text;
    }

    public void HideToolTip()
    {
        tooltipContainer.SetActive(false);
    }
}
