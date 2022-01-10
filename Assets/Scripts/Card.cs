using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Fight fight;
    public int cardIdx;
    public float bonusValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        fight.cardIsSelected = true;
        fight.SetBonus(cardIdx,bonusValue);
    }
}
