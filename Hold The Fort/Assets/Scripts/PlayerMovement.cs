using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private float playerSpeed;
    private float angle;
    private float mag;
    private int orientation; //1 = right, 2 = down, 3 = left, 4 = up
    private int lastOrientation;
    private bool moving;
    private bool wasMoving;
    [SerializeField] GameObject inventory;
    private Entity entity;
    private Animator anim;
    private bool toggled;

   // private Vector2 mousePos;

    void Start()
    {
        toggled = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        entity = GetComponent<Player>();
        playerSpeed = entity.speed;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            toggleInventory();
        }
    }

    private void toggleInventory()
    {
        if (toggled)
        {
            toggled = false;
            inventory.SetActive(false);
            /*      if (shiftPos != null)
                      StopCoroutine(shiftPos);
                  shiftPos = StartCoroutine(ShiftPosition(posIn));    */
        }
        else
        {
            toggled = true;
            inventory.SetActive(true);
            /*      if (shiftPos != null)
                      StopCoroutine(shiftPos);
                  shiftPos = StartCoroutine(ShiftPosition(posVis));   */
        }
    }
    void FixedUpdate()
    {

        entity.sortLayers();
        entity.SoftenRecoil();

        //TEMPORARILY REMOVING THIS FOR A SPRITE ANIMATION SYSTEM
        /*
       mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x-transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y-transform.position.y);
        transform.rotation = Quaternion.Euler(0,0, 90 + Mathf.Rad2Deg * Mathf.Atan2(mousePos.y, mousePos.x));
 */

        //mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y);
      

        mag = Mathf.Sqrt(Mathf.Pow(Input.GetAxis("Horizontal"), 2) + Mathf.Pow(Input.GetAxis("Vertical"), 2));
        angle = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        rb.velocity = new Vector2((mag * playerSpeed * Mathf.Cos(angle)) + entity.recoil.x, (mag * playerSpeed * Mathf.Sin(angle)) + entity.recoil.y) ;
        if (rb.velocity.x > playerSpeed / 2)
        {
            orientation = 1;
        } else if (rb.velocity.x < -playerSpeed / 2)
        {
            orientation = 3;
        }
        else if (rb.velocity.y > playerSpeed / 2)
        {
            orientation = 4;
        }
        else if (rb.velocity.y < -playerSpeed / 2)
        {
            orientation = 2;
        }
        if (Mathf.Abs(rb.velocity.x) > .25f || Mathf.Abs(rb.velocity.y) > .25f)
            moving = true;
        else
            moving = false;

        if (moving != wasMoving || orientation != lastOrientation)
        {
            wasMoving = moving;
            lastOrientation = orientation;
            SetAnim();
        } 


    }
    public void SetAnim()
    {

        if (moving)
        {
            if (orientation == 1)
                anim.Play("Right");
            else if (orientation == 2)
                anim.Play("Down");
            else if (orientation == 3)
                anim.Play("Left");
            else if (orientation == 4)
                anim.Play("Up");
        }
        else
        {
            if (orientation == 1)
                anim.Play("StopRight");
            else if (orientation == 2)
                anim.Play("StopDown");
            else if (orientation == 3)
                anim.Play("StopLeft");
            else if (orientation == 4)
                anim.Play("StopUp");
        }

    }

}
