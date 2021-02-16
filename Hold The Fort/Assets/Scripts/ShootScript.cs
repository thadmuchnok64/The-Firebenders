using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer LR;
    void Start()
    {
        LR = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    public void Shoot()
    {

        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right);

        if (ray)
        {
            LR.SetPosition(0, transform.position);
            LR.SetPosition(1, ray.point);
        }
        else
        {
            LR.SetPosition(0, transform.position);
            LR.SetPosition(1, transform.position + transform.right*100);
        }

        LR.startColor = new Color(LR.startColor.r, LR.startColor.b, LR.startColor.g, 1);
        LR.endColor = new Color(LR.endColor.r, LR.endColor.b, LR.endColor.g, 1);
        StartCoroutine(FadeLine());

    }

    public IEnumerator FadeLine()
    {
        for(int i = 0; i < 30; i++)
        {
            LR.startColor = new Color(LR.startColor.r, LR.startColor.b, LR.startColor.g, LR.startColor.a / 1.25f);
            LR.endColor = new Color(LR.endColor.r, LR.endColor.b, LR.endColor.g, LR.endColor.a / 1.15f);
            yield return new WaitForSeconds(.015f);



        }
        LR.startColor = new Color(LR.startColor.r, LR.startColor.b, LR.startColor.g, 0);
        LR.endColor = new Color(LR.endColor.r, LR.endColor.b, LR.endColor.g, 0);
    }
}
