using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    private float speed = 5.0f;
    private Vector3 moveVector = Vector3.zero;
    
    private float gravity = 8.0f;
    private float jumpVelocity= 4f;
    public bool groundedPlayer;

    private float startTime;
   

    private float animationDuration = 2.0f;
    private bool isDead = false;


    


    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        CharacterController controller = GetComponent<CharacterController>();

        if (isDead)
        {
            return;
        }

        if (Time.time - startTime < animationDuration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        animator.SetBool("dead", false);

        groundedPlayer = controller.isGrounded;

        if (groundedPlayer)
        {
            animator.SetBool("fall", false);

            //x - left and right

            moveVector.x = Input.GetAxisRaw("Horizontal") * speed;

            if (Input.GetMouseButton(0))
            {

                if(Input.mousePosition.x > Screen.width / 2)
                {
                    moveVector.x = speed;
                }
                else
                {
                    moveVector.x = -speed;
                }
            }

            //y - up and down

            


            if (Input.GetButton("Jump"))
            {
                moveVector.y = jumpVelocity;
            }

            

            //z - forward and backward
            moveVector.z = speed;


            controller.Move(moveVector * Time.deltaTime);
        }
        else
        {
            animator.SetBool("fall", true);
        }
        
        moveVector.y -= gravity * Time.deltaTime;
        controller.Move(moveVector * Time.deltaTime);

        if(controller.transform.position.y < -2)
        {
            Death();
        }


    }
    public void SetSpeed(float modifier)
    {
        speed = 3.0f + modifier;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag=="Obstacle")
        {
            animator.SetBool("dead", true);
            Death();
        }
    }
    

    private void Death()
    {
       
        isDead = true;

        GetComponent<Score>().OnDeath();
    }

}
