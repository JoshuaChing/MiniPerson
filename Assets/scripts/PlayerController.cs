using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    public float speed = 10.0F;

    Animator animator;
    Vector3 movement;
    float camRayLength = 100f;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
    }
	
    void FixedUpdate()
    {
        float horizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        float vertical = CrossPlatformInputManager.GetAxisRaw("Vertical");

        RotateWithMouse();
        Move(horizontal, vertical);
        Animate(horizontal, vertical);
    }

	// Update is called once per frame
	void Update () {

    }

    // Rotate character with mouse
    void RotateWithMouse()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            GetComponent<Rigidbody>().MoveRotation(newRotation);
        }
    }

    // Move character with keyboard
    void Move (float horizontal, float vertical)
    {
        movement.Set(horizontal, 0f, vertical);
        movement = movement.normalized * speed * Time.deltaTime;
        GetComponent<Rigidbody>().MovePosition(transform.position + movement);
    }

    // Animate character
    void Animate(float horizontal, float vertical)
    {
        animator.SetBool("isWalking", (horizontal != 0 || vertical != 0) ? true : false);
    }

}
