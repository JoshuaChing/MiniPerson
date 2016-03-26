using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    public float speed = 10.0F;

    Animator animator;
    Vector3 movement;
    float camRayLength = 100f;

    bool attackTriggered;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
    }
	
    void FixedUpdate()
    {
        float horizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        float vertical = CrossPlatformInputManager.GetAxisRaw("Vertical");

        HandleInput();
        HandleMovement(horizontal, vertical);
        HandleAnimation(horizontal, vertical);
        RotateWithMouse();
    }

	// Update is called once per frame
	void Update () {

    }

    // Rotate character with mouse
    void RotateWithMouse()
    {
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            return;
        }

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
    void HandleMovement (float horizontal, float vertical)
    {
        movement.Set(horizontal, 0f, vertical);
        movement = movement.normalized * speed * Time.deltaTime;
        GetComponent<Rigidbody>().MovePosition(transform.position + movement);
    }

    // Animate character
    void HandleAnimation(float horizontal, float vertical)
    {
        if (attackTriggered)
        {
            animator.SetTrigger("Attack");
            attackTriggered = false;
        }
        else {
            animator.SetBool("isWalking", (horizontal != 0 || vertical != 0) ? true : false);
        }
    }

    // Handle input
    void HandleInput()
    {
        // left click
        if (Input.GetMouseButtonDown(0))
        {
            attackTriggered = true;
        }
    }

}
