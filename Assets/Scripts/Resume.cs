using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Resume : Button
{

    [SerializeField] private GameObject pause;
    // Start is called before the first frame update
    void Update()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    // Update is called once per frame

    void OnPointerDown()
    {
       
     
        
    }

    public void OnMouseDown()
    {
        Time.timeScale = 1;
        pause.SetActive(false);
    }
}
