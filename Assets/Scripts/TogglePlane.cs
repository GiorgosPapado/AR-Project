using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

[RequireComponent(typeof(ARPlaneManager))]
public class TogglePlane : MonoBehaviour
{

    private ARPlaneManager manager;

    [SerializeField]
    private TextMeshProUGUI toggleButtonText;

    private void Awake()
    {
        manager = GetComponent<ARPlaneManager>();
        toggleButtonText.text = "Disable";
    }

    public void TogglePlaneDetection()
    {
        manager.enabled = !manager.enabled;
            string toggleButtonMessage = "";

            if (manager.enabled)
        {
            toggleButtonMessage = "Disable";
            SetAllPlanesActive(true);
        }
        else
        {
            toggleButtonMessage = "Enable";
            SetAllPlanesActive(false);
        }
        toggleButtonText.text = toggleButtonMessage;
    }

    private void SetAllPlanesActive(bool value)
    {
        foreach(var plane in manager.trackables)
        {
            plane.gameObject.SetActive(value);
        }
    }
}
