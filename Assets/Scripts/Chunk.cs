using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour, IEnumerable
{
    public List<MonoBehaviour> cells =new List<MonoBehaviour>();
    private GameObject _demon;

    private int _nbGrass;
    // Start is called before the first frame update
    private void Awake()
    {
        _demon = GameObject.Find("Demon");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBlock(MonoBehaviour block)
    {
        GetComponent<Chunk>().cells.Add(block);
        if (block is Grass)
            _nbGrass++;
    }

 
    
    
    public void Spawn()
    {
        
        var rdm = Random.Range(1, _nbGrass);
        int inc = 0;
        foreach(var cell in cells)
        {
            if (cell is Grass)
            {
                inc++;
                if (inc == rdm)
                {
                    var o = cell.gameObject;
                    var position = o.transform.position;
                    var demon = Instantiate(_demon, new Vector3(position.x,position.y,-1),o.transform.rotation);
                    demon.transform.SetParent(transform);
                    return;
                }
            }
        }
        
    }
    
    
    
    
    
    
    public IEnumerator GetEnumerator()
    {
        throw new System.NotImplementedException();
    }
}
