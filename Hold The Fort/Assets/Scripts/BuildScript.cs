using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildScript : MonoBehaviour
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
    private GameObject hotbar;
    private BuildingBlock block;
    private float mineCoolDown;
    private float mineTime;
    private AudioSource AS;
    public AudioClip blockHit, blockBreak, blockPlace;
    void Start()
    {
        //gridDiameter = 5.0f; //Obsolete
        mineTime = .3f;
        mineCoolDown = mineTime;
        playerObject = GameObject.Find("player");
        hotbar = playerObject.GetComponent<Player>().hotbar;
        gridDiameter=GameObject.Find("Grid").GetComponent<Grid>().cellSize.x;
        sr = GetComponent<SpriteRenderer>();
        tm = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        AS = GetComponent<AudioSource>();
    }

    private void PlaySound(AudioClip x)
    {

        if (SoundMaster.soundOn)
            AS.PlayOneShot(x);

    }
    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z / 1.2f);
        mineCoolDown += Time.deltaTime;
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
            if (Input.GetMouseButton(1))
            {
                if (hotbar.GetComponent<BuildingBlock>() != null &&hotbar.GetComponent<Hotbar>().slotItem!=null)
                {
                    block = hotbar.GetComponent<BuildingBlock>();
                    if (block.quantity > 0 && tm.GetTile(tm.WorldToCell(transform.position)) == null)
                    {

                        tm.SetTile(tm.WorldToCell(transform.position), block.blockTilePrefab);
                        PlaySound(blockPlace);
                        block.quantity--;
                        
                //        if (hotbar.GetComponent<Hotbar>().slotItem is BuildingBlock)
                    //    {
                            int a = hotbar.GetComponent<Hotbar>().slotItem.GetComponent<BuildingBlock>().quantity--;
                            if (a > block.quantity)
                            {
                                block.quantity = a;
                            }
                            hotbar.GetComponent<Hotbar>().UpdateSlotText();
                    //    }
                    }
                }
                else
                {
                    block = null;
                }
                //also change item in slot here!! IMPORTANT!!!!!!!
            } else if (Input.GetMouseButton(0) && mineCoolDown >= mineTime)
            {
                if (tm.GetTile(tm.WorldToCell(transform.position)) != null)
                {
                    TileData a = tm.GetInstantiatedObject(tm.WorldToCell(transform.position)).GetComponent<TileData>();
                    if (!a.isUnbreakable)
                    {
                            a.durability -= 20;
                            mineCoolDown = 0;
                            transform.eulerAngles = new Vector3(0, 0, Random.Range(30,45));
                        if (a.durability <= 0) { 
                            Instantiate(a.buildingBlock, transform.position, transform.rotation);
                            tm.SetTile(tm.WorldToCell(transform.position), null);
                            PlaySound(blockBreak);
                        }
                        else
                        {
                            PlaySound(blockHit);
                        }
                    }
                }
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
