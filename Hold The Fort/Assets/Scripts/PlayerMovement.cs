using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public float playerSpeed;
    private float angle;
    private float mag;

    private Vector2 mousePos;
 
    void Start()
    {
   
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
       mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x-transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y-transform.position.y);
        transform.rotation = Quaternion.Euler(0,0, 90 + Mathf.Rad2Deg * Mathf.Atan2(mousePos.y, mousePos.x));
 
        mag = Mathf.Sqrt(Mathf.Pow(Input.GetAxis("Horizontal"), 2) + Mathf.Pow(Input.GetAxis("Vertical"), 2));
        angle = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        rb.velocity = new Vector2(mag*playerSpeed * Mathf.Cos(angle), mag*playerSpeed * Mathf.Sin(angle));
        

    }

  
}
