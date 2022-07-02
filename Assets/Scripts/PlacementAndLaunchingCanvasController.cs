using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

[RequireComponent(typeof(ARRaycastManager))]
public class PlacementAndLaunchingCanvasController : MonoBehaviour
{
    [SerializeField]
    private GameObject welcomePanel;

    [SerializeField]
    private PlacementObject placedObject;

    [SerializeField]
    private Color activeColor = Color.red;

    [SerializeField]
    private Color inactiveColor = Color.gray;

    [SerializeField]
    private Button dismissButton;
    private void Dismiss() => welcomePanel.SetActive(false);

    [SerializeField]
    private Camera arCamera;

    private Vector2 touchPosition = default;

    [SerializeField]
    private GameObject displayCanvas;

    [SerializeField]
    GameObject visualObject;

    ARRaycastManager m_RaycastManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    public PlacementObject spawnedObject { get; private set; }

    void Awake() 
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        dismissButton.onClick.AddListener(Dismiss);
    }

        void Update()
    {
        // do not capture events unless the welcome panel is hidden
        if(welcomePanel.activeSelf)
            return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            touchPosition = touch.position;

            if(touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;

/*                if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    // Raycast hits are sorted by distance, so the first one
                    // will be the closest hit.
                    var hitPose = s_Hits[0].pose;
                    spawnedObject = Instantiate(placedObject, hitPose.position, Quaternion.identity);
                }*/
                // if we got a hit meaning that it was selected
                if (Physics.Raycast(ray, out hitObject))
                {
                    PlacementObject placed = hitObject.transform.GetComponent<PlacementObject>();
                    MeshRenderer placedMeshRenderer = placedObject.GetComponent<MeshRenderer>();
                    if(placed != null && placed.Selected == true)
                    {
                        placedMeshRenderer.material.color = activeColor;
                        
                        if(displayCanvas.activeSelf) 
                        {
                            placed.ToggleCanvas();
                        }
                    }
                } // nothing selected so set the sphere color to inactive
                else
                {
                    PlacementObject placed = placedObject.GetComponent<PlacementObject>();
                    MeshRenderer placedMeshRenderer = placedObject.GetComponent<MeshRenderer>();
                    if(placed != null && placed.Selected == false)
                    {                        
                        placedMeshRenderer.material.color = inactiveColor;

                        if(displayCanvas.activeSelf) 
                        {
                            placed.ToggleCanvas();
                        }
                    }
                }
            }
        }
    }
    public void DisableVisual()
    {
        visualObject.SetActive(false);
    }
}
