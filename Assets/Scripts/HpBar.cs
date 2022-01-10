using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : Bar
{
    
    private new void Awake()
    {
        base.Awake();
        scaleCoef = 1.5f;
  
    }

}
