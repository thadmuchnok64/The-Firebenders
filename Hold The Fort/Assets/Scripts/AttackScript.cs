using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer LR;
    public bool canShoot,canMelee;
    private Animator anim;
    public GameObject bat;
    private SpriteRenderer batSR;
    public LayerMask layerEntities;
    private GameObject hotbar;
    //change this in future
    public int range;
    private Vector2 mousePos;
    private Entity entity;
    private Player PS;
    private float weaponCooldown;
    private AudioSource AS;
    public AudioClip[] batAttackSounds;
    public AudioClip swingSound;

    void Awake()
    {
        entity = GetComponent<Entity>();
        LR = GetComponent<LineRenderer>();
        PS = GetComponent<Player>();
        hotbar = PS.hotbar;
        AS = GetComponent<AudioSource>();
        //anim = bat.GetComponent<Animator>();
    }

    public void UpdateItem()
    {
        MeleeWeapon a;
        if (hotbar.GetComponent<MeleeWeapon>() != null)
        {
            a = hotbar.GetComponent<MeleeWeapon>();
                if(bat==null)
            bat = Instantiate(a.weaponObject,transform);
           
            batSR = bat.GetComponent<SpriteRenderer>();
            anim = bat.GetComponent<Animator>();
            canMelee = true;
        } else
        {
            canMelee = false;
                Destroy(bat.gameObject);

            }
    }
    // Update is called once per frame
    void Update()
    {
        weaponCooldown += Time.deltaTime;

        if (!PauseMenu.isPaused)
        {


            if (canShoot&& Input.GetMouseButtonDown(0))
            {
                Shoot();
                weaponCooldown = 0;
            }
            else if (canMelee && Input.GetMouseButtonDown(0) && weaponCooldown > hotbar.GetComponent<MeleeWeapon>().attackRate)
            {
                StartCoroutine(StartSwing());
                weaponCooldown = 0;
            }

            mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y);

            if (canMelee)
            {
                bat.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(mousePos.y, mousePos.x));
                bat.transform.position = new Vector3(transform.position.x + 5 * Mathf.Cos(Mathf.Atan2(mousePos.y, mousePos.x)), transform.position.y - 1 + 5 * Mathf.Sin(Mathf.Atan2(mousePos.y, mousePos.x)), transform.position.z);
                if (Mathf.Atan2(mousePos.y, mousePos.x) > .6f)
                {
                    batSR.sortingOrder = entity.sr.sortingOrder - 1;
                }
                else
                {
                    batSR.sortingOrder = entity.sr.sortingOrder + 1;
                }
                if (bat.transform.position.x < transform.position.x)
                    batSR.flipY = true;
                else
                    batSR.flipY = false;


            }



        }
    }
 
    public void Swing()
    {
        StartCoroutine(SizeSwingShift());
        Entity e;
        Enemy f;
        anim.Play("BatSwing");
        bool hit = false;
        Collider2D[] entitiesHit = Physics2D.OverlapCircleAll(bat.transform.position, range, layerEntities);

        foreach (Collider2D entities in entitiesHit)
        {
            if (!entities.isTrigger)
            {

                e = entities.GetComponent<Entity>();
              // f = entities.GetComponent<Enemy>();
                if (e != null && !e.isFriendly)
                {
                    e.TakeDamage(5); //dont hard code this in the future;
                    e.TakeRecoil(transform, 70);
                    hit = true;
                }
            }
        }
        if (hit&&SoundMaster.soundOn)
            AS.PlayOneShot(batAttackSounds[Random.Range(0, batAttackSounds.Length)]);
    }
    public IEnumerator StartSwing()
    {
        if(SoundMaster.soundOn)
        AS.PlayOneShot(swingSound);
        StartCoroutine(SizeSwingShift());
        yield return new WaitForSeconds(hotbar.GetComponent<MeleeWeapon>().attackDelay);
        Swing();
    }
    public IEnumerator SizeSwingShift()
    {
        bat.transform.localScale = new Vector3(1, 1, 1);
        for (int x = 0; x < 10; x++)
        {
            bat.transform.localScale = new Vector3(bat.transform.localScale.x + .07f, bat.transform.localScale.y + .05f, bat.transform.localScale.z);
            yield return new WaitForSeconds(.01f);
        }
        for (int x = 0; x < 10; x++)
        {
            bat.transform.localScale = new Vector3(bat.transform.localScale.x - .07f, bat.transform.localScale.y - .05f, bat.transform.localScale.z);
            yield return new WaitForSeconds(.005f);
        }
        bat.transform.localScale = new Vector3(1, 1, 1);
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
