using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    // general camera variables
    public GameObject target;
    public float cameraPosX = 0f;
    public float cameraPosY = 20f;
    public float cameraPosZ = -40f;
    public float cameraRotX = 25f;
    public float cameraRotY = 0f;
    public float cameraRotZ = 0f;
    public bool followCameraActive = true;

    // mouse aim camera variables
    public float rotateSpeed = 5;
    Vector3 offset;

    // follow camera settings
    // setting 1: 0,20,-40,25,0,0
    // setting 2: 0,30,-60,25,0,0
    // setting 3: 0,40,-80,25,0,0

    // mouse aim settings
    // setting 1: 0,20,-80,0,0,0

    void Start()
    {
        offset = target.transform.position - transform.position;
    }

    void FixedUpdate()
    {
        HandleInput();
    }

    void LateUpdate()
    {
        HandleCameraType();
    }

    void HandleCameraType()
    {
        if (followCameraActive)
        {
            FollowCamera();
        }
        else
        {
            MouseAimCamera();
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            if (followCameraActive)
            {
                // set mouse aim settings
                SetCameraVaraibles(0f, 20f, -80f, 0f, 0f, 0f);
                followCameraActive = false;
            }
            else
            {
                // set camera follow settings
                SetCameraVaraibles(0f, 40f, -80f, 25f, 0f, 0f);
                followCameraActive = true;
            }
        }
    }

    void SetCameraVaraibles(float px, float py, float pz, float rx, float ry, float rz)
    {
        cameraPosX = px;
        cameraPosY = py;
        cameraPosZ = pz;
        cameraRotX = rx;
        cameraRotY = ry;
        cameraRotZ = rz;
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

    void MouseAimCamera()
    {
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.transform.Rotate(0, horizontal, 0);

        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position - (rotation * offset);

        transform.LookAt(target.transform);
    }
}
