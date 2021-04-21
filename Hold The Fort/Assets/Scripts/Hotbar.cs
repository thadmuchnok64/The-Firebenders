using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    [SerializeField] GameObject[] slots;
    [SerializeField] AttackScript attackScript;
    private int activeSlotIndex;
    public float scrollTime;
    private float scrollCooldown;
    public Item slotItem;
    // Start is called before the first frame update
    void Start()
    {
        activeSlotIndex = 0;
        InsertItem(slots[activeSlotIndex].GetComponent<Item>());
    }

    // Update is called once per frame
    void Update()
    {
        scrollCooldown -= Time.deltaTime ;
        if (scrollCooldown <= 0) {
            if (Input.mouseScrollDelta.y == 1)
            {
                /*
                Destroy(GetComponent<Item>());
                slotItem = null;
                scrollCooldown = scrollTime;
                shrinkSlot(slots[activeSlotIndex]);
                activeSlotIndex++;
                
                expandSlot(slots[activeSlotIndex]);
                if (slots[activeSlotIndex].GetComponent<Item>() != null)
                    InsertItem(slots[activeSlotIndex].GetComponent<Item>());
                    */
                StartCoroutine(Adjust(activeSlotIndex + 1));
            }
            else if (Input.mouseScrollDelta.y == -1)
            {
                /*
                Destroy(GetComponent<Item>());
                slotItem = null;
                scrollCooldown = scrollTime;
                shrinkSlot(slots[activeSlotIndex]);
                activeSlotIndex--;
                
                expandSlot(slots[activeSlotIndex]);
                if (slots[activeSlotIndex].GetComponent<Item>() != null)
                    InsertItem(slots[activeSlotIndex].GetComponent<Item>());
                    */
                StartCoroutine(Adjust(activeSlotIndex - 1));

            }
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                StartCoroutine(Adjust(0));
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                StartCoroutine(Adjust(1));
            }
            else if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                StartCoroutine(Adjust(2));
            }
            else if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                StartCoroutine(Adjust(3));
            }
            else if (Input.GetKeyUp(KeyCode.Alpha5))
            {
                StartCoroutine(Adjust(4));
            }
            else if (Input.GetKeyUp(KeyCode.Alpha6))
            {
                StartCoroutine(Adjust(5));
            }
            else if (Input.GetKeyUp(KeyCode.Alpha7))
            {
                StartCoroutine(Adjust(6));
            }
        }
    }
    private IEnumerator Adjust(int x)
    {
       
        Destroy(GetComponent<Item>());
        slotItem = null;
        yield return new WaitForSeconds(.01f);
        shrinkSlot(slots[activeSlotIndex]);
        activeSlotIndex = x;
        if (activeSlotIndex < 0)
            activeSlotIndex = slots.Length - 1;
        else if (activeSlotIndex >= slots.Length)
            activeSlotIndex = 0;
        expandSlot(slots[activeSlotIndex]);
        InsertItem(slots[activeSlotIndex].GetComponent<Item>());
    }
    private void shrinkSlot(GameObject x)
    {
        
        InventorySlot a = x.GetComponent<InventorySlot>();
        a.ShiftScale(new Vector3(1, 1, 1));
    }

    private void expandSlot(GameObject x)
    {
        InventorySlot a = x.GetComponent<InventorySlot>();
        a.ShiftScale(new Vector3(1.2f, 1.2f, 1));
    }
    
    public void UpdateSlotText()
    {
        if (slotItem.quantity<=0)
        Destroy(GetComponent<Item>());
        slots[activeSlotIndex].GetComponent<InventorySlot>().UpdateText();

    }
    public void InsertItem(Item item)
    {
        if (gameObject.GetComponent<Item>() != null)
        {
            Destroy(gameObject.GetComponent<Item>());
        }
        if (item != null)
        {
            gameObject.AddComponent(item.GetType());
            System.Reflection.FieldInfo[] fields = item.GetType().GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
            {
                field.SetValue(gameObject.GetComponent(item.GetType()), field.GetValue(item));
            }
        }
        slotItem = item;

        StartCoroutine(DelayUpdate(3));
        
    }
    private IEnumerator DelayUpdate(int x)
    {
        for (int y = 0; y < x; y++) {
            yield return new WaitForEndOfFrame();
        }
        attackScript.UpdateItem();
    }
}
