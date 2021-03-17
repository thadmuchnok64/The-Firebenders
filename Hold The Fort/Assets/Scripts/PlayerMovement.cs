using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public float playerSpeed;
    public Camera cam;
    private float angle;
    private float mag;
    private float charAngle;
    private bool isMoving;

    private Vector2 mousePos;
    private Vector2 mouseDif;
    Animator animator;
 
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x-transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y-transform.position.y);
        // transform.rotation = Quaternion.Euler(0,0, 90 + Mathf.Rad2Deg * Mathf.Atan2(mousePos.y, mousePos.x));

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseDif = mousePos - rb.position;
        charAngle = Mathf.Atan2(mouseDif.x, mouseDif.y);

        mag = Mathf.Sqrt(Mathf.Pow(Input.GetAxis("Horizontal"), 2) + Mathf.Pow(Input.GetAxis("Vertical"), 2));
        angle = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        rb.velocity = new Vector2(mag*playerSpeed * Mathf.Cos(angle), mag*playerSpeed * Mathf.Sin(angle));
        if (rb.velocity == Vector2.zero)
        {
            isMoving = false;
        } else
        {
            isMoving = true;
        }

        Animations();
    }

    void Animations()
    {
        if (isMoving)
        {
            if ((charAngle > Mathf.PI/4)&&(charAngle < (Mathf.PI * 3) / 4))
            {
                animator.Play("AlbertRunRight");
            } else if ((charAngle > -(Mathf.PI * 3) / 4) && (charAngle < -Mathf.PI/4))
            {
                animator.Play("AlbertRunLeft");
            } else if ((charAngle > -Mathf.PI/4) && (charAngle < Mathf.PI/4))
            {
                animator.Play("AlbertRunUp");
            } else
            {
                animator.Play("AlbertRunDown");
            }
        } else
        {
            if ((charAngle > Mathf.PI / 4) && (charAngle < (Mathf.PI * 3) / 4))
            {
                animator.Play("AlbertIdleRight");
            }
            else if ((charAngle > -(Mathf.PI * 3) / 4) && (charAngle < -Mathf.PI / 4))
            {
                animator.Play("AlbertIdleLeft");
            }
            else if ((charAngle > -Mathf.PI / 4) && (charAngle < Mathf.PI / 4))
            {
                animator.Play("AlbertIdleUp");
            }
            else
            {
                animator.Play("AlbertIdleDown");
            }
        }
    }
  
}
