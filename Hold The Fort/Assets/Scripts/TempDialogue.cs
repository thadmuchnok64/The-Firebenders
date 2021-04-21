using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempDialogue : MonoBehaviour
{

    [SerializeField] string[] greetingDialogue;
    [SerializeField] string[] hitDialogue;
    [SerializeField] string victoryDialogue;
    [SerializeField] string failureDialogue;
    [SerializeField] Text text;
    [SerializeField] RectTransform textBox;
    [SerializeField] Inventory inventory;
    [SerializeField] Player player;
    private Coroutine c;
    private bool introComplete;
    // Start is called before the first frame update
    void Start()
    {
       c = StartCoroutine(DialogueLoop(greetingDialogue));
        introComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DialogueLoop(string[] dialogue)
    {
        yield return new WaitForSeconds(2);
        foreach(string s in dialogue){
            PlugDialogue(s);
            yield return new WaitForSeconds(5);
        }
        introComplete = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (introComplete&&collision.gameObject.tag == "Player")
        {
            if (CheckIfGameBeat())
            {
                PlugDialogue(victoryDialogue);
                StartCoroutine(player.StartEndGame());
                //add here
            }
            else
            {
                PlugDialogue(failureDialogue);
            }
        }
    }
    private bool CheckIfGameBeat()
    {
        if (inventory.CheckIfContainsID(2) && inventory.CheckIfContainsID(4) && inventory.CheckIfContainsID(5) && inventory.CheckIfContainsID(6) && inventory.CheckIfContainsID(7) && inventory.CheckIfContainsID(8))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Hurt()
    {
        if (c != null)
            c = null;
        PlugDialogue(hitDialogue[Random.Range(0, hitDialogue.Length)]);
    }
    private void PlugDialogue(string s)
    {
        text.text = s;
        RectTransform rectText = text.GetComponent<RectTransform>();
        rectText.sizeDelta= new Vector2(rectText.sizeDelta.x, text.preferredHeight);
        textBox.sizeDelta = new Vector2(textBox.sizeDelta.x,2100+text.preferredHeight*10);
    }
}
