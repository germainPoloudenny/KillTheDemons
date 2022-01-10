using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Fighter
{   
    public EndlessTerrain endlessTerrain;


    public float velocity = 0.05f;
    public int xp = 0, wisdom=0, regen=1;
    private int _lastMove;
    // Start is called before the first frame update
    private new void Start()
    {
        
        endlessTerrain.UpdateVisibleChunks(transform.position);
        SetStats();
        hpBarText.UpdateText();
        textHp.text = "Lvl " + level;
        
    }

    private void OnEnable()
    {
        StartCoroutine(Regenerate());
    }

    private void FixedUpdate()
    {
        
        if (Input.GetKey("z"))
        {
        
                transform.Translate(0,velocity,0);
            
        }
        if (Input.GetKey("d"))
        {   
       
                transform.Translate(velocity,0,0);
          
        }
        if (Input.GetKey("s"))
        {
            
                transform.Translate(0,-velocity,0);
            
        }
        if (Input.GetKey("q"))
        {
            
                transform.Translate(-velocity,0,0);
            
        }
        if ( Input.GetKey("z") ||  (Input.GetKey("d")) ||    (Input.GetKey("s")) ||    (Input.GetKey("q"))                     )
            endlessTerrain.UpdateVisibleChunks(transform.position);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (!(collision.gameObject.CompareTag($"Monster")&& collision.gameObject.GetComponents<BoxCollider2D>()[0]==collision)) return;

        GameObject.Find("SceneController").GetComponent<SceneController>().StartFight(collision.gameObject);
        
        
        


    }



    private new void SetStats()
    {
        level = 1;
        maxHp = 30;
        hp = 30;
        attack = 10;
        critRate = 0.25f;
    }

    public HpBarText hpBarText;
    public IEnumerator Regenerate()
    {   
   
        while (hp != maxHp)
        {
            SetHp(hp+regen);
            hpBarText.UpdateText();
            yield return new WaitForSeconds(1f);
        }
    }
    
}
