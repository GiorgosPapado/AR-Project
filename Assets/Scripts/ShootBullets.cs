using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullets : MonoBehaviour
{
    public GameObject StartingCanvas;
    public Transform arCamera;
    public GameObject projectile;


    private float shootForce = 700f;
    private void Update()
    {
        if (StartingCanvas.activeSelf)
            return;
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            GameObject bullet = Instantiate(projectile, arCamera.position, arCamera.rotation) as GameObject;
            bullet.GetComponent<Rigidbody>().AddForce(arCamera.forward * shootForce); 
        }
    }

}
