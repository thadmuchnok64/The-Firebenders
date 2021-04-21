using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Entity
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private TempDialogue td;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        td = GetComponent<TempDialogue>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(recoil.x, recoil.y);
        SoftenRecoil();
    }

    public override void TakeDamage(float dam)
    {
        td.Hurt();       
    }

}
