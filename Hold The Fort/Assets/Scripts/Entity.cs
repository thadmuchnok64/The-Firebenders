using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;
    public float maxHealth;
    public int speed;
    public SpriteRenderer sr;
    public int damage;
    public float armor;
    public bool isFriendly;
    public Vector2 recoil;
    [SerializeField] Color damageColor;
    private Color normalColor;


    public void Awake()
    {
        if (health == 0)
        {
            health = 10;
        }
        if(speed == 0)
        {
            speed = 5;
        }
        if(damage == 0)
        {
            damage = 15;
        }
        if(armor == 0)
        {
            armor = 1;
        }

        sr = GetComponent<SpriteRenderer>();
        normalColor = sr.color;

    }

    public void sortLayers()
    {
        sr.sortingOrder = Mathf.RoundToInt(-2.0f * transform.position.y);
    }

    protected virtual void Update()
    {
        //Debug.Log("This does nothing, you should replace this script with inherited scripts");
    }
    protected virtual void Start()
    {
        //Debug.Log("This also does nothing");
    }
    public virtual void TakeDamage(float dam)
    {
        health = health - (dam / armor);
    }
    public void Heal(float healAmmount)
    {
        health += healAmmount;
        if (health > maxHealth)
            health = maxHealth;
    }
    public void SoftenRecoil()
    {
        if (Mathf.Abs(recoil.x) > 0 && Mathf.Abs(recoil.y) > 0)
        {
            recoil = new Vector2(recoil.x / 1.2f, recoil.y / 1.2f);
        }
    }
    public void TakeRecoil(Transform t, int mag)
    {
        float x, y;
        x = t.position.x - transform.position.x;
        y = t.position.y - transform.position.y;
        float a = Mathf.Atan2(y, x);
        recoil = new Vector2(Mathf.Cos(a) * -mag, Mathf.Sin(a) * -mag);
        sr.color = damageColor;
        StartCoroutine(ResetColor());
    }

    public IEnumerator ResetColor()
    {
        for (int x = 0; x < 15; x++)
        {
            sr.color = new Color((normalColor.r + sr.color.r) / 2f, (normalColor.g + sr.color.g) / 2f, (normalColor.b + sr.color.b) / 2f, (normalColor.a + sr.color.a) / 2f);
            yield return new WaitForSeconds(.05f);
        }
        sr.color = normalColor;
    }

}
