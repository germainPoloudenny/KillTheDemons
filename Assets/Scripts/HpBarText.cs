using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarText : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player;
    void OnEnable()
    {
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText()
    {
        gameObject.GetComponent<Text>().text = player.hp + " / " + player.maxHp;
    }
}
