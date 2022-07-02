using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using TMPro;

[RequireComponent(typeof(ARRaycastManager))]
public class PlacementWithManySelectionWithScaleController : MonoBehaviour
{
    [SerializeField]
    private GameObject placedPrefab;

    [SerializeField]
    private GameObject welcomePanel;

    [SerializeField]
    private Button dismissButton;

    [SerializeField]
    private bool applyScalingPerObject = false;

    [SerializeField]
    private Slider scaleSlider;

    [SerializeField]
    private TextMeshPro scaleTextValue;

    [SerializeField]
    private Camera arCamera;

    [SerializeField]
    private Color activeColor = Color.red;

    [SerializeField]
    private Color inactiveColor = Color.gray;

    private Vector2 touchPosition = default;

    private ARRaycastManager arRaycastManager;

    private ARSessionOrigin aRSessionOrigin;

    private bool onTouchHold = false;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private PlacementObject lastSelectedObject;


    void Awake() 
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        aRSessionOrigin = GetComponent<ARSessionOrigin>();
        dismissButton.onClick.AddListener(Dismiss);
        scaleSlider.onValueChanged.AddListener(ScaleChanged);
    }


    private void Dismiss() => welcomePanel.SetActive(false);

    private void ScaleChanged(float newValue)
    {
        if(applyScalingPerObject){
            if(lastSelectedObject != null && lastSelectedObject.Selected)
            {
                lastSelectedObject.transform.localScale = Vector3.one * scaleSlider.value;
            }
        }
        else 
            aRSessionOrigin.transform.localScale = Vector3.one * newValue;

        scaleTextValue.text = $"Scale {newValue}";
    }

    void Update()
    {
        // do not capture events unless the welcome panel is hidden or if UI is selected
        if(welcomePanel.activeSelf)
            return;

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if(EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                return;

            touchPosition = touch.position;

            if(touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if(Physics.Raycast(ray, out hitObject))
                {
                    
                    lastSelectedObject = hitObject.transform.GetComponent<PlacementObject>();
                    
                    if (lastSelectedObject != null)
                    {

                        PlacementObject[] allOtherObjects = FindObjectsOfType<PlacementObject>();
                        foreach(PlacementObject placementObject in allOtherObjects)
                        {
                            MeshRenderer meshRenderer = placementObject.GetComponent<MeshRenderer>();
                            if (placementObject != lastSelectedObject){
                                placementObject.Selected = false;
                                meshRenderer.material.color = inactiveColor;
                            }
                            else
                            {
                                placementObject.Selected = true;
                                meshRenderer.material.color = activeColor;
                            }
                            placementObject.ToggleOverlay();
                        }
                    }
                }
                if(arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;

                    if(lastSelectedObject == null)
                    {
                        lastSelectedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation).GetComponent<PlacementObject>();
                    }
                }
            }  

            if(touch.phase == TouchPhase.Moved)
            {
                if(arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;

                    if(lastSelectedObject != null && lastSelectedObject.Selected)
                    {
                        lastSelectedObject.transform.parent.position = hitPose.position;
                        lastSelectedObject.transform.parent.rotation = hitPose.rotation;
                    }
                }
            }
        }
    }
}
