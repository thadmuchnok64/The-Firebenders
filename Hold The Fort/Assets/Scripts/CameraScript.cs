using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject playerObject;
    public float scopeDistance;
    void Start()
    {
        playerObject = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(((Mathf.Cos(Mathf.Deg2Rad*(playerObject.transform.eulerAngles.z-90)))*scopeDistance*Input.GetAxis("Shift"))+(playerObject.transform.position.x*5.0f+mPos.x)/6.0f, ((Mathf.Sin(Mathf.Deg2Rad * (playerObject.transform.eulerAngles.z-90))) * scopeDistance* Input.GetAxis("Shift")) +(playerObject.transform.position.y*5.0f+mPos.y)/6.0f,transform.position.z);
        
    }
}
