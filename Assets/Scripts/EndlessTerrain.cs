using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class EndlessTerrain : MonoBehaviour
{

    private readonly Dictionary<Vector2, GameObject> _chunkDictionary = new Dictionary<Vector2, GameObject>();
    private readonly Dictionary<Vector2, GameObject> _deathChunkDictionary = new Dictionary<Vector2, GameObject>();
    public int chunkSize;

    private const int ChunkVisibleInViewDst = 10;
    public void UpdateVisibleChunks(Vector2 viewerTransform){
     

        for(var yOffset = - ChunkVisibleInViewDst; yOffset <= ChunkVisibleInViewDst; yOffset+=ChunkVisibleInViewDst)
        {
           
            for(var xOffset = - ChunkVisibleInViewDst; xOffset <= ChunkVisibleInViewDst; xOffset+=ChunkVisibleInViewDst)
            {
               
                var viewedChunkCoord = new Vector2(viewerTransform.x + xOffset, viewerTransform.y + yOffset);
                var newVec =  VecToChunkCoord(viewedChunkCoord);
              
                
                if(_deathChunkDictionary.ContainsKey(newVec))
                {
                 
                    var chunk = _deathChunkDictionary[newVec];
                    _chunkDictionary.Add(newVec,chunk);
                    _deathChunkDictionary.Remove(newVec);
                    chunk.SetActive(true);

                    
                }
                else if (!_chunkDictionary.ContainsKey(newVec)){
                   
                    var chunk = new GameObject();
                    chunk.AddComponent<Chunk>();
                    chunk.transform.position = new Vector3(newVec.x, newVec.y, -1);
                    _chunkDictionary.Add(newVec, chunk );
                    gameObject.GetComponent<MapGenerator>().GenerateNoiseMap(newVec,chunk,chunkSize);
                    chunk.GetComponent<Chunk>().Spawn();
                }
              
            }
        }
       
        var temp = new Dictionary<Vector2, GameObject>(_chunkDictionary) ;
        foreach(var coord in temp.Keys)
        {
            
            if (!(Mathf.Abs(coord.x - viewerTransform.x) > 4 * ChunkVisibleInViewDst) &&
                !(Mathf.Abs(coord.y - viewerTransform.y) > 4 * ChunkVisibleInViewDst)) continue;
            var chunk = _chunkDictionary[coord];
           _deathChunkDictionary.Add(coord,chunk);
           _chunkDictionary.Remove(coord);
           chunk.SetActive(false);
        }
                
                
                
                
               
            
    }


    private Vector2 VecToChunkCoord(Vector2 vec)
    {
        if (vec.x < 0 && vec.x % 10 !=0) vec.x -= chunkSize;
        if (vec.y < 0 && vec.y % 10 !=0) vec.y -= chunkSize;
        var coordX  = (float)    Math.Floor( (double)(int)( vec.x / chunkSize)) * chunkSize;
        var coordY  = (float)   Math.Floor((double)(int)(vec.y /  chunkSize)) * chunkSize;
       
        return new Vector2(coordX, coordY);
    }

    public GameObject CellCordToCell(Vector2 vec)
    {
        foreach (var i in _chunkDictionary)
        {
            if (i.Key == vec) return i.Value;
        }

        return null;
    }

}
