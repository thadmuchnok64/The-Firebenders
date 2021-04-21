using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectCoolEffect : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(CoolItemEffectStuffz());
    }

    private IEnumerator CoolItemEffectStuffz()
    {
        Vector3 pos1 = transform.position;
        Vector3 pos2 = new Vector3(pos1.x + .15f, pos1.y + .35f, pos1.z);
        float rot1 = 0;
        float rot2 = 30;

        while (true)
        {
            for (int x = 0; x < 125; x++)
            {
                transform.position = new Vector3((transform.position.x * 99f + pos2.x) / 100f, (transform.position.y * 99f + pos2.y) / 100f, pos1.z);
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
