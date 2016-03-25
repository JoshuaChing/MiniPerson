using UnityEngine;
using System.Collections;

public class MinionMove : MonoBehaviour {

    public Transform target;
    public float speed = 3f;
    public float rotationSpeed = 3f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        MoveToTarget();
        RotateToTarget();
    }
    
    void MoveToTarget()
    {
        float movement = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, movement);
    }

    void RotateToTarget()
    {
        Vector3 lookPosition = target.position - transform.position;
        lookPosition.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
