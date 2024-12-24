using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneration : MonoBehaviour
{
    [SerializeField] int width,height;
    [SerializeField] int repeatNum;
    [SerializeField] int minRepeatNum, maxRepeatNum;
    [SerializeField] int minHeight,maxHeight;
    [SerializeField] GameObject dirt, grass;
    [SerializeField] GameObject enemy;
    // public int randomEnemy = ;


    

    // Start is called before the first frame update
    void Start()
    {
        Generation();
    }

    // Update is called once per frame
    void Generation()
    {
        int down = 15;
        int repeatValue = 0;
        // int randomEnemy = 0;
        for (int x=0; x<width;x++){
            if (repeatValue == 0){
                height = Random.Range(minHeight, maxHeight);
                GenerateFlatPlatform(x, down);
                repeatValue = Random.Range(minRepeatNum, maxRepeatNum);
            }else{
                GenerateFlatPlatform(x, down);
                repeatValue--;

            }
                       
        }
        int randomEnemy = Random.Range(10, 21);
        for (int x=10; x<130;){
            Instantiate(enemy, new Vector2(x, height+5), Quaternion.identity);
            x += randomEnemy;
        }
        
        
    }
    void GenerateFlatPlatform(int x, int down){
        for (int y=0; y<height;y++){ 
            SpawnObject(dirt, x, y-down);
        }
        SpawnObject(grass, x, height-down); 
    }
    void SpawnObject(GameObject gameObject, int width, int height){
        gameObject = Instantiate(gameObject, new Vector2(width, height), Quaternion.identity);
        gameObject.transform.parent = this.transform;
    }
}
