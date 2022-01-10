using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    // Start is called before the first frame update
    private Slider slider1;
    public Slider slider2;
    void Start()
    {
        slider1 = GetComponent<Slider>();
        slider1.onValueChanged.AddListener(delegate
        {

            GameObject.Find(("SceneController")).GetComponent<SceneController>().SetVolume(slider1.value);
            slider2.value = slider1.value;
        });
    }

    // Update is called once per frame

}
