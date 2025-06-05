using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform mCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Change camera x to players x pos, keep y the same
        mCamera.position = new Vector3(transform.position.x, mCamera.position.y, mCamera.position.z);
    }
}
