using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    // Start is called before the first frame update
    [SerializeField] int aggroRange;
    Animator anim;
    bool isAggrod;
    [SerializeField] GameObject player;

    private float playerX;
    private float playerY;
    private int orientation; //1 = right, 2 = down, 3 = left, 4 = up
    private int lastOrientation;
    private Rigidbody2D rb;
    private float angleToPlayer;
    

    protected override void Start()
    {

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim.Play("Idle");
        isAggrod = false;
        //StartCoroutine(Idle());
    }

    // Update is called once per frame
    protected override void Update()
    {

        if (health <= 0)
            Destroy(gameObject);

        sortLayers();
        
        angleToPlayer = Mathf.Atan2(playerY, playerX);

        if (isAggrod)
        {
            playerX = player.transform.position.x - transform.position.x;
            playerY = player.transform.position.y - transform.position.y;
            if (Mathf.Abs(playerX)> (Mathf.Abs(playerY))){
                if (playerX > 0)
                    orientation = 1;
                else
                    orientation = 3;
            }
            else
            {
                if (playerY > 0)
                    orientation = 4;
                else
                    orientation = 2;
            }

            if (orientation != lastOrientation)
            {
                lastOrientation = orientation;
                SetAnim();
            }
        }

    }
    public void FixedUpdate()
    {
        if(isAggrod)
        rb.velocity = new Vector2((Mathf.Cos(angleToPlayer) * speed)+recoil.x, (Mathf.Sin(angleToPlayer) * speed)+recoil.y);
        SoftenRecoil();
    }

    public void SetAnim()
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
 
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isAggrod = true;
            player = collision.gameObject;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Entity e;
            e = collision.gameObject.GetComponent<Player>();
            e.TakeDamage(damage);
            e.TakeRecoil(transform,50);
        }
    }

    /*
    public IEnumerator Idle()
    {

        playerX = player.transform.position.x - transform.position.x;
        playerY = player.transform.position.y - transform.position.y;

        while (!isAggrod)
        {
            yield return new WaitForSeconds(.2f);



            if (Mathf.Abs(playerX)<=aggroRange&& Mathf.Abs(playerY) <= aggroRange)
            {
                isAggrod = true;
            }
        }
        

}
*/
}
