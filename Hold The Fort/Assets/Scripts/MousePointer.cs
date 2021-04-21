using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    private Vector2 mousePos;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
            if (inventory.GetComponent<Item>() != null)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(mousePos.x, mousePos.y, 0);
                sr.sprite = inventory.GetComponent<Item>().sprite;
            }
            else
            {
                sr.sprite = null;
            }
        
    }
}
