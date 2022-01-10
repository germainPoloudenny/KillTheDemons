using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bar : MonoBehaviour
{
    // Start is called before the first frame update
    protected float _initScale;
    private float _tmpPos1, _tmpPos2;
    protected float scaleCoef;
    protected void Awake()
    {
        var transform1 = transform;
        _initScale = transform1.localScale.x;
        _tmpPos1 = 0;

    }

    // Update is called once per frame
    void Update()
    {
     
    }


    public void SetScale(float x)
    {
        var transform1 = transform;
        var localScale = transform1.localScale;
        localScale = new Vector3(_initScale * x, localScale.y, 0);
        transform1.localScale = localScale;
        var position = transform1.position;
        var positionX = position.x+_tmpPos1;
        _tmpPos1 =(_initScale-localScale.x)/2*scaleCoef;
        position = new Vector3(positionX  - _tmpPos1, position.y, position.z);
        transform1.position = position;
    }
}
