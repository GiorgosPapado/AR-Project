/*using System.Collections;
using System.Collections.Generic;
using Google.XR.ARCoreExtensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

public class UnityEventResolver : UnityEvent<Transform> {}
public class ArCloudAnchorManager : MonoBehaviour
{

    [SerializeField]
    private Camera arCamera;

    [SerializeField]
    private float resolveAnchorPassedTimeout = 5f;

    private ARAnchorManager arAnchorManager;

    private ARAnchor pendingHostAnchor;

    private ARCloudAnchor cloudAnchor;

    private string anchorToResolve;

    private bool anchorUpdateInProgress = false;

    private bool anchorResolveInProgress = false;

    private float safeToResolvePassed = 0;

    private UnityEventResolver resolver;

    private void Awake()
    {
        resolver = new();
        resolver.AddListener((t) => ARPlacementManager.Instance.RecreatePlacement(t));
    }
}
*/