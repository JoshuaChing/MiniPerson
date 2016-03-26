using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    public float speed = 10.0F;

    Animator animator;
    Vector3 movement;
    float camRayLength = 100f;

    bool attackTriggered;
    public GameObject sword;

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

    // Handle input
    void HandleInput()
    {
        // left click
        if (Input.GetMouseButtonDown(0))
        {
            attackTriggered = true;
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
        HandleSwordPosition();

        if (attackTriggered)
        {
            animator.SetTrigger("Attack");
            attackTriggered = false;
        }
        else {
            animator.SetBool("isWalking", (horizontal != 0 || vertical != 0) ? true : false);
        }
    }

    // Handle sword position
    void HandleSwordPosition()
    {
        Quaternion newRotation;
        Vector3 newPosition;

        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            newRotation = Quaternion.Euler(-5.232f, 268.37f, 326.67f);
            newPosition = new Vector3(-0.00075f, -0.01963f, -0.01194f);
        } else
        {
            newRotation = Quaternion.Euler(331.4513f, 214.6951f, 71.69279f);
            newPosition = new Vector3(-0.007f, -0.01508f, -0.00483f);
        }

        sword.transform.localRotation = newRotation;
        sword.transform.localPosition = newPosition;
    }

}
