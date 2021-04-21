using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Container
{
    private Vector3 posIn, posVis,mousePos;
    //private Coroutine shiftPos;
    private RectTransform rect;

   // private SpriteRenderer sr;
    public GameObject mouseObject;

    private void Start()
    {
        posIn = transform.position;
        rect = gameObject.GetComponent<RectTransform>();
        posVis = new Vector3(-25f, 15f, rect.position.z);
       // sr = mouseObject.GetComponent<SpriteRenderer>();
    }
    /*
    private void Update()
    {
       
        if (GetComponent<Item>()!=null)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseObject.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
            sr.sprite = GetComponent<Item>().sprite;
        }
        else
        {
            sr.sprite = null;
        }
    }
    */

    /*
    public IEnumerator ShiftPosition(Vector3 newPos)
    {

        for(int x = 0; x<40; x++)
        {
            
            rect.localPosition = new Vector3((5f * newPos.x + rect.localPosition.x) / 6f, (5f * newPos.y + rect.localPosition.y) / 6f, rect.localPosition.z);
            yield return new WaitForSeconds(.02f);
        }

    }
     */

}
