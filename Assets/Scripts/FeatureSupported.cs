using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class FeatureSupported : MonoBehaviour
{
    [SerializeField]
    private Text features;

    [SerializeField]
    private ARFaceManager arFaceManager;

    [SerializeField]
    private ARHumanBodyManager arHumanBodyManager;

    [SerializeField]
    private ARPointCloudManager arPointCloudManager;

    void Start()
    {
        // Face Support Checks
        bool supportsEyeTracking = arFaceManager.subsystem.subsystemDescriptor.supportsEyeTracking;
        bool supportsFacePose = arFaceManager.subsystem.subsystemDescriptor.supportsFacePose;
        bool supportsFaceMeshVerticesAndIndices = arFaceManager.subsystem.subsystemDescriptor.supportsFaceMeshVerticesAndIndices;

        // Human Body Support Checks
        bool supportsHumanBody2D = arHumanBodyManager.subsystem.subsystemDescriptor.supportsHumanBody2D;
        bool supportsHumanBody3D = arHumanBodyManager.subsystem.subsystemDescriptor.supportsHumanBody3D;
        bool supportsHumanDepthImage = arHumanBodyManager.subsystem.subsystemDescriptor.supportsHumanBody3D;
            
        // Point Cloud Support Checks
        bool supportsConfidence = arPointCloudManager.subsystem.subsystemDescriptor.supportsConfidence;
        bool supportsFeaturePoints = arPointCloudManager.subsystem.subsystemDescriptor.supportsFeaturePoints;
        
        
        features.text = $"supportsEyeTracking : {supportsEyeTracking}\n" +
            $"supportsFacePose : {supportsFacePose}\n" +
            $"supportsFaceMeshVerticesAndIndices : {supportsFaceMeshVerticesAndIndices}\n" +
            $"supportsHumanBody2D : {supportsHumanBody2D}\n" +
            $"supportsHumanBody3D : {supportsHumanBody3D}\n" +
            $"supportsHumanDepthImage : {supportsHumanDepthImage}\n" +
            $"supportsConfidence : {supportsConfidence}\n" +
            $"supportsFeaturePoints : {supportsFeaturePoints}";
    }
}
