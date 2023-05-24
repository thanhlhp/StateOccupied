using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public bool zoomActive;
    public Vector3[] target;
    public Camera cam;
    public float speed;
 
    private void Start()
    {
        target[1] = LevelScript.instance.gameArea.position;
        target[1].z = -10;
        target[0] = new Vector3(0, 0, -10);
        cam = Camera.main;
    
    }
    private void Update()
    {
        if(zoomActive)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5, speed * Time.deltaTime);
            cam.transform.position = Vector3.Lerp(cam.transform.position, target[1], speed * Time.deltaTime);
        }
        else
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 15, speed * Time.deltaTime);
            cam.transform.position = Vector3.Lerp(cam.transform.position, target[0], speed * Time.deltaTime);
        }
    }
}
