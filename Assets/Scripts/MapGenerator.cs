using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public  class MapGenerator : MonoBehaviour
{
    public Grass grass;
    public  Water water;
    public  GameObject  chunks;

    public int seed;
    public  float scale;

    
    public  float[,] GenerateNoiseMap(Vector2 coord,GameObject chunk, int size){
    
        var  noiseMap = new float[size, size];
        var prng = new System.Random();
  
        float offsetX= prng.Next(-100000, 100000) ;
        float offsetY= prng.Next(-100000, 100000);
        
        if (scale <=0)
        {
            scale = 0.001f;
        }
        for (var y = 0; y < size; y++)
        {
            for (var x = 0; x < size ; x++)
            {
       
                var sampleX =  (x+coord.x) /scale + offsetX ;
                var sampleY = (y+coord.y) /scale  + offsetY;
                var  perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                
                noiseMap[x,y] = perlinValue;
                
                
                Generate(perlinValue,coord.x+x,coord.y+y,chunk);
               
            }
        }     
        chunk.transform.SetParent(chunks.transform);
        return noiseMap;

    }

    private void Generate(float value,float x, float y, GameObject chunk)
    {
        MonoBehaviour instanced;
        if(value <0.6)
        {
             instanced= Instantiate(grass,new Vector3(x,y,0),grass.transform.rotation);
           
        
        }
        else {
              instanced=   Instantiate(water,new Vector3(x,y,0),water.transform.rotation);
            
        }
        instanced.transform.SetParent(chunk.transform);

        chunk.GetComponent<Chunk>().AddBlock(instanced);
    }

    public void Start()
    {

    }



}