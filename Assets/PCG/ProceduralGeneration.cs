using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] int width,height;
    [SerializeField] int minStoneHeight, maxStoneHeight;
    [SerializeField] GameObject dirt, grass, stone;

    // Start is called before the first frame update
    void Start()
    {
        Generation();
    }

    // Update is called once per frame
    void Generation()
    {
        for (int x=0; x<width;x++){
            int minHeight = height - 1;
            int maxHeight = height + 2;
            int minStoneSpawnDistance = height - minStoneHeight;
            int maxStoneSpawnDistance = height - maxStoneHeight;
            int totalStoneSpawnDistance = Random.Range(minStoneSpawnDistance, maxStoneSpawnDistance);
            height = Random.Range(minHeight, maxHeight);
            for (int y=0; y<height;y++){ 
                if (y < totalStoneSpawnDistance){
                    SpawnObject(stone, x, y);
                }else{
                    SpawnObject(dirt, x, y);
                }
            }
            if (height == totalStoneSpawnDistance){
                SpawnObject(stone, x, height);
            }else{
                SpawnObject(grass, x, height);
            }
            
        }
        
    }
    void SpawnObject(GameObject gameObject, int width, int height){
        gameObject = Instantiate(gameObject, new Vector2(width, height), Quaternion.identity);
        gameObject.transform.parent = this.transform;
    }
    
}
