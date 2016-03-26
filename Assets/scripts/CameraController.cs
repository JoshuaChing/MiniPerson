using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject target;

    // follow camera variables
    public float cameraPosX = 0f;
    public float cameraPosY = 20f;
    public float cameraPosZ = -40f;
    public float cameraRotX = 25f;
    public float cameraRotY = 0f;
    public float cameraRotZ = 0f;

    // setting 1: 0,20,-40,25,0,0
    // setting 2: 0,30,-60,25,0,0
    // setting 3: 0,40,-80,25,0,0

    void LateUpdate()
    {
        FollowCamera();
    }

    void FollowCamera()
    {
        Vector3 cameraPosition = target.transform.position;
        cameraPosition.x += cameraPosX;
        cameraPosition.y += cameraPosY;
        cameraPosition.z += cameraPosZ;

        this.transform.position = cameraPosition;
        this.transform.rotation = Quaternion.Euler(cameraRotX, cameraRotY, cameraRotZ);
    }
}
