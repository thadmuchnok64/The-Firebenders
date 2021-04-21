using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] SpriteRenderer sr;
    public Item containedItem;
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject textObject;
    [SerializeField] GameObject itemDummy;

    // Update is called once per frame
   
    public void Update()
    {
        if(GetComponent<Item>() is BuildingBlock)
        {
            textObject.SetActive(true);
            UpdateText();
        }
        else
        {
            textObject.SetActive(false);
        }
    }
    public void UpdateText()
    {
        if (GetComponent<BuildingBlock>().quantity != 0)
            textObject.GetComponent<Text>().text = GetComponent<BuildingBlock>().quantity + "";
        else
        {
            textObject.GetComponent<Text>().text = "";
            sr.sprite = null;
            Destroy(GetComponent<Item>());
        }

    }
    public void addItem(Item item)
    {
        GetComponent<Item>().quantity += item.quantity;
    }
    public void InsertItem(Item item)
    {
        sr.sprite = item.sprite;
        if (gameObject.GetComponent<Item>() == null)
        {
            gameObject.AddComponent(item.GetType());
            System.Reflection.FieldInfo[] fields = item.GetType().GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
            {
                field.SetValue(gameObject.GetComponent(item.GetType()), field.GetValue(item));
            }
        }
    }

    public void PushItemToPointer(Item item)
    {
        if (inventory.gameObject.GetComponent<Item>() == null)
        {
            inventory.gameObject.AddComponent(item.GetType());
            System.Reflection.FieldInfo[] fields = item.GetType().GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
            {
                field.SetValue(inventory.gameObject.GetComponent(item.GetType()), field.GetValue(item));
            }
        }
    }

    public IEnumerator SwapItems(Item item1,Item item2)
    {
        GameObject par = itemDummy;
        if (par.gameObject.GetComponent<Item>() == null)
        {
            par.AddComponent(item1.GetType());
            System.Reflection.FieldInfo[] fields = item1.GetType().GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
            {
                field.SetValue(par.GetComponent(item1.GetType()), field.GetValue(item1));
            }
        }
        Destroy(item1);
        yield return new WaitForSeconds(.01f);
        InsertItem(item2);
        Destroy(item2);
        yield return new WaitForSeconds(.01f);
        PushItemToPointer(par.GetComponent<Item>());
        Destroy(par.GetComponent<Item>());

    }

    public Item RemoveItem()
    {
        Item x = containedItem;
        if (gameObject.GetComponent<Item>() != null)
            Destroy(GetComponent<Item>());
        sr.sprite = null;
        return x;
    }

    public void onPress()
    {

        if (inventory.GetComponent<Item>() == null && GetComponent<Item>() != null)
        {
            //happens when you have an object occupied in the slot, but not the mouse
            PushItemToPointer(GetComponent<Item>());
            Destroy(GetComponent<Item>());
            sr.sprite = null;

        }
        else if (inventory.GetComponent<Item>() != null && GetComponent<Item>() == null)
        {
            //this happens if you have an item occupied by the mouse, but not the slot
            InsertItem(inventory.GetComponent<Item>());
            Destroy(inventory.GetComponent<Item>());
            sr.sprite = GetComponent<Item>().sprite;
            inventory.mouseObject.GetComponent<SpriteRenderer>().sprite = null;
        }
        else if(inventory.GetComponent<Item>() != null && GetComponent<Item>() != null)
        {
            StartCoroutine(SwapItems(GetComponent<Item>(), inventory.GetComponent<Item>()));
            sr.sprite = GetComponent<Item>().sprite;

        }
    }

    public void ShiftScale(Vector3 newSize)
    {
        StartCoroutine(SizeShift(newSize));
    }
    private IEnumerator SizeShift(Vector3 newSize)
    {
        RectTransform x = GetComponent<RectTransform>();
        for (int a = 0; a<10; a++)
        {
            x.localScale = new Vector3((x.localScale.x * 3f + newSize.x) / 4f, (x.localScale.y * 3f + newSize.y) / 4f, x.localScale.z);
            yield return new WaitForSeconds(.0075f);
        }
        x.localScale = newSize;
    }
    

}
