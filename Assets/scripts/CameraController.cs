using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject target;
    public float cameraPosX = 0f;
    public float cameraPosY = 20f;
    public float cameraPosZ = -40f;
    public float cameraRotX = 25f;
    public float cameraRotY = 0f;
    public float cameraRotZ = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 cameraPosition = target.transform.position;
        cameraPosition.x += cameraPosX;
        cameraPosition.y += cameraPosY;
        cameraPosition.z += cameraPosZ;

        this.transform.position = cameraPosition;
        this.transform.rotation = Quaternion.Euler(cameraRotX, cameraRotY, cameraRotZ);
	}
}
