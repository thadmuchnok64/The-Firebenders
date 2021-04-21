using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField] InventorySlot[] slots;
    [SerializeField] SoundMaster SM;
    [SerializeField] AudioClip click;

    public bool insertItem(Item item)
    {
        bool foundMin = false;
        InventorySlot a = null;
        foreach (InventorySlot slot in slots)
        {
            Item x = slot.GetComponent<Item>();
            if (x == null && !foundMin)
            {
                foundMin = true;
                a = slot;
            }
            else if (x!=null&&x.itemID == item.itemID && x.isStackable)
            {
                slot.addItem(item);
                if (SoundMaster.soundOn)
                    SM.PlaySound(click);
                return true;
            }

        }

        if (foundMin)
        {
            a.InsertItem(item);
            if (SoundMaster.soundOn)
                SM.PlaySound(click);
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public bool CheckIfContainsID(int ID)
    {
        foreach(InventorySlot slot in slots)
        {
            Item x = slot.GetComponent<Item>();
            if (x!=null&&x.itemID == ID)
            {
                return true;
            }
        }
        return false;
    }
}
