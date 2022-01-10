using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cloak : MonoBehaviour
{
    public Text text;
    public bool isFrozen = false;
    public float timeWithoutPlaying;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isFrozen)
        text.text =  FormatTime(Time.timeSinceLevelLoad-timeWithoutPlaying);
    }
    public string FormatTime( float time )
    {
        int minutes = (int) time / 60;
        int seconds = (int) time - 60 * minutes;
        return string.Format("{0:00}:{1:00}",  minutes, seconds );
    }

    
}
