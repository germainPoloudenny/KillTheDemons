using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Demon : Fighter
{
    private readonly Dictionary<int, bool> _vision = new Dictionary<int, bool>();
    public Vector2 futurPosition;
    // Start is called before the first frame update
    new void OnEnable()
    {
    
        transform.position = new Vector3(futurPosition.x,futurPosition.y,-1);
        Move();
    }

    void Awake()
    {
        var position = transform.position;
        futurPosition = new Vector2(position.x, position.y);
        for (var i = 0; i < 4; i++) _vision[i] = true;
    }
    // Update is called once per frame
    void Update()
    {
      
    }

    void Move()
    {
        var position = transform.position;
        int i = 0;
        while (i++<100)
        {
            var direction = Random.Range(0, 4);

            switch (direction)
            {
                case 0 :
                    if (!_vision[0])
                    {
                        
                        continue;
                    }
                    
                    futurPosition = new Vector2(futurPosition.x, futurPosition.y + 1);
                    StartCoroutine(MoveCoroutine(new Vector2(0, 1)));
                    break;
                case 1 :
                    if (!_vision[1]) continue;
                    futurPosition = new Vector2(futurPosition.x+1, futurPosition.y );
                    StartCoroutine(MoveCoroutine(new Vector2(1, 0)));
                    break;
                case 2 :
                    if (!_vision[2]) continue;
                    futurPosition = new Vector2(futurPosition.x, futurPosition.y - 1);
                    StartCoroutine(MoveCoroutine(new Vector2(0, -1)));
                    break;
                case 3 :
                    if (!_vision[3]) continue;
                    futurPosition = new Vector2(futurPosition.x-1, futurPosition.y );
                    StartCoroutine(MoveCoroutine(new Vector2(-1, 0)));
                    break;
                case 4 :
                    futurPosition = new Vector2(futurPosition.x, futurPosition.y );
                    StartCoroutine(MoveCoroutine(new Vector2(0, 0)));
                    break;
            }

            break;
        }

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        Vector3 otherPosition=other.transform.position;

        bool isBorrowable=true;
        if (other.CompareTag("Water"))
        {
         
            isBorrowable = false;
        }
        else if (other.CompareTag("Monster"))
        {
           
            otherPosition = other.GetComponent<Demon>().futurPosition;
            isBorrowable = false;
        }
       
        
        var vec2 = new Vector2(otherPosition.x, otherPosition.y);
        var position = transform.position;
        if (futurPosition.x == otherPosition.x && futurPosition.y + 1 == otherPosition.y)
        {

            _vision[0] = isBorrowable;
        }
        else if (futurPosition.x +1 == otherPosition.x && futurPosition.y  == otherPosition.y) _vision[1] = isBorrowable;
        else if (futurPosition.x == otherPosition.x  && futurPosition.y -1 == otherPosition.y) _vision[2] = isBorrowable;
        else if (futurPosition.x  -1 == otherPosition.x && futurPosition.y  == otherPosition.y) _vision[3] = isBorrowable;
    }

    private IEnumerator MoveCoroutine(Vector2 vec)
    {
        var position = transform.position;
        var vec3 = new Vector3(position.x+vec.x, position.y+vec.y, -1);
        for (int i = 0; i < 100; i++)
        {
            position= Vector3.MoveTowards(position,vec3,0.01f);
            transform.position = position;
            yield return new WaitForSeconds(0.01f);
        }

        transform.position = new Vector3(futurPosition.x,futurPosition.y,-1);
        
        yield return new WaitForSeconds(0.1f);
        Move();
    }
}
