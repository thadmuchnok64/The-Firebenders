using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class buildLight : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 mousePos;
    private Vector2 roundedPos;
    public GameObject stamp;
    private float gridDiameter;
    private GameObject playerObject;
    private SpriteRenderer sr;
    private Tilemap tm;
    public RuleTile stoneBlock;
    public Color occColor, empColor;
    void Start()
    {
        //gridDiameter = 5.0f; //Obsolete
        playerObject = GameObject.Find("player");
        gridDiameter=GameObject.Find("Grid").GetComponent<Grid>().cellSize.x;
        sr = GetComponent<SpriteRenderer>();
        tm = GameObject.Find("Tilemap").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        roundedPos = new Vector2((Mathf.Round((mousePos.x-gridDiameter/2) / gridDiameter)) * gridDiameter, (Mathf.Round((mousePos.y-gridDiameter/2)/ gridDiameter)) * gridDiameter);
        transform.position = new Vector3(roundedPos.x+(gridDiameter/2), roundedPos.y+(gridDiameter/2), transform.position.z);
        if (tm.GetTile(tm.WorldToCell(transform.position)) != null)
        {
            sr.color =new Color(occColor.r,occColor.g,occColor.b,sr.color.a);
        }
        else
        {
            sr.color = new Color(empColor.r, empColor.g, empColor.b,sr.color.a);
        }
        if ((Mathf.Abs(transform.position.x - playerObject.transform.position.x)) < 15f && (Mathf.Abs(transform.position.y - playerObject.transform.position.y)) < 15)
        {
            if (Input.GetMouseButtonUp(1))
            {
                tm.SetTile(tm.WorldToCell(transform.position),stoneBlock);
            } else if (Input.GetMouseButtonUp(0))
            {
                tm.SetTile(tm.WorldToCell(transform.position), null);
            }
            if (sr.color.a < 1)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b,(0.001f + sr.color.a*1.05f));
            }
        }
        else
        {
            if (sr.color.a >0)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a/1.1f);
            }
        }

    }
}
