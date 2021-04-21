using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    // Start is called before the first frame update

    [SerializeField] GameObject healthBarMask;
    private Coroutine regen;
    [SerializeField] Inventory inventory;
    [SerializeField] PauseMenu PM;
    public GameObject hotbar;
    private AudioSource AS;
    [SerializeField] AudioClip[] hitsound;
    [SerializeField] Text gameOverText;


    protected override void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    protected override void Update()
    {
        healthBarMask.transform.localPosition = new Vector3(((health/maxHealth)*17f)-17f, 0, 0);
        if (health < 0)
        {
            PM.DeathScreen();
        }
    }

    public IEnumerator StartEndGame()
    {
        gameOverText.text = "You Won!";
        yield return new WaitForSeconds(6);
        PM.DeathScreen();
        

    }
    public override void TakeDamage(float dam)
    {
        if(SoundMaster.soundOn)
        AS.PlayOneShot(hitsound[Random.Range(0, hitsound.Length)]);
        health = health - (dam / armor);
        if(regen!=null)
        StopCoroutine(regen);
        regen = StartCoroutine(RegenerateHealth());
    }

    private IEnumerator RegenerateHealth()
    {
        yield return new WaitForSeconds(3);
        while (health < maxHealth)
        {
            Heal(.05f);
            yield return new WaitForSeconds(.05f);
        }
    }

    public bool CollectItem(Item item)
    {
       return inventory.insertItem(item);
    }
}
