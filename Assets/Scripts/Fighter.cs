using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fighter : MonoBehaviour
{
    // Start is called before the first frame update
    public int hp = 10, maxHp=10, attack = 10, level;
    public float critRate = 0.25f;
    public HpBar hpBar;
    public Text textHp;

    protected void Start()
    {
        
        if (gameObject.name == "Opponent") return;
        SetStats();
        textHp.text = "Lvl " + level;

    }

// Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetHp(int x)
    {

        hp = x;
        if (hp > maxHp)
            hp = maxHp;
        else if (hp < 0)
            hp = 0;
        hpBar.SetScale((float)hp/maxHp);
    }

    public int Attack()
    {
     
        if (Random.Range(0f,1f)  < critRate)
            return 2 * attack;
        return attack;
    }

    public void SetStats()
    {
        level = Random.Range(1,100);
        var skill = level;
        attack = Random.Range(skill/6+1,skill/3+1)*2;
        skill -= attack/2;
        hp = Random.Range(skill/6+1,skill/3+1)*7;
        maxHp = hp;
        skill -= hp/5;
        critRate = (skill+3) * 0.02f;
    }


}
