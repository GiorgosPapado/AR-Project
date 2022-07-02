using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARAnchorManager))]
public class ReferencePointManagerWithCameraDistance : MonoBehaviour
{
    [SerializeField]
    private Text debugLog;

    [SerializeField]
    private Text referencePointCount;

    [SerializeField]
    private Button clearReferencePointsButton;

    [SerializeField]
    private Camera arCamera;

    private ARAnchorManager arReferencePointManager;

    private List<ARAnchor> referencePoints = new List<ARAnchor>();

    private ARPlane plane;
    
    void Awake() 
    {
        plane = GetComponent<ARPlane>();
        arReferencePointManager = GetComponent<ARAnchorManager>();
        clearReferencePointsButton.onClick.AddListener(ClearReferencePoints);

        debugLog.gameObject.SetActive(false);
    }

    void Update()
    {
        if(Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if(touch.phase != TouchPhase.Began)
            return;

        Vector3 newPos = arCamera.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0.3f));

        ARAnchor referencePoint = arReferencePointManager.AttachAnchor(plane, new Pose(newPos, Quaternion.identity));
    }

    private void ClearReferencePoints()
    {
        foreach(ARAnchor referencePoint in referencePoints)
        {
            Destroy(referencePoint);
        }
        referencePoints.Clear();
        referencePointCount.text = $"Reference Point Count: {referencePoints.Count}";
    }
}
