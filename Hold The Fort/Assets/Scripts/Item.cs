using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public Sprite sprite;
    public int itemID;
    private bool isTouchingPlayer;
    private Player player;
    public bool isStackable;
    public int quantity;


    private void Update()
    {
        /*
        if (isTouchingPlayer && Input.GetKeyDown(KeyCode.E))
        {
            bool a;
            a = player.GetComponent<Player>().CollectItem(this);
            if(a)
            Destroy(gameObject);
        }
        */
      
    }

    private IEnumerator PlayerCollect()
    {
        Vector3 x = player.transform.position;
        for (int y = 0; y < 10; y++)
        {
            transform.position = new Vector3((transform.position.x * 9f + x.x) / 10f, (transform.position.y * 9f + x.y) / 10f, transform.position.z);
            yield return new WaitForSeconds(.01f);
        }
        bool a;
        if (player != null)
        {
            a = player.GetComponent<Player>().CollectItem(this);
            if (a)
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            isTouchingPlayer = true;
            player = collision.GetComponent<Player>();
            StartCoroutine(PlayerCollect());
        }/*
        else if(isStackable && collision.tag == "Quant")
        {
            Item x = collision.GetComponent<Item>();
            quantity += x.quantity;
            Destroy(collision.gameObject);
        }*/
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isTouchingPlayer = false;
            player = null;
        }
    }

    //this courintine fucks with items when they're in the inventory. The effect isn't that great anyway though. Fix this eventually.
    private IEnumerator CoolItemEffectStuffz()
    {
        Vector3 pos1 = transform.position;
        Vector3 pos2 = new Vector3(pos1.x + .15f, pos1.y + .35f, pos1.z);
        float rot1 = 0;
        float rot2 = 30;
        
        while (true){
            for(int x = 0; x < 125; x++)
            {
                transform.position = new Vector3((transform.position.x*99f+pos2.x)/100f, (transform.position.y * 99f + pos2.y) / 100f,pos1.z);
                transform.eulerAngles = new Vector3(0, 0, (rot2 + transform.eulerAngles.z * 99) / 100f);
                yield return new WaitForEndOfFrame();
            }
            for (int x = 0; x < 125; x++)
            {
                transform.position = new Vector3((transform.position.x * 99f + pos1.x) / 100f, (transform.position.y * 99f + pos1.y) / 100f, pos1.z);
                transform.eulerAngles = new Vector3(0, 0, (rot1 + transform.eulerAngles.z * 99) / 100f);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}

