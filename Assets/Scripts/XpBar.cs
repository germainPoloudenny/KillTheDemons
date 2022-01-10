using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpBar : Bar
{

    private void Start()
    {
        scaleCoef = 7f/0.65f;
    }

    public IEnumerator setXpAnimating(int xp)
    {

        int oldXp =  (int)(transform.localScale.x/_initScale*100);
        int diff = xp - oldXp;
        for (int i = 0; i < 10; i++)
        {
            SetScale((oldXp+diff*(i+1)/10f)/100);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.3f);
        if(xp==100) SetScale(0);
        
    }
}
