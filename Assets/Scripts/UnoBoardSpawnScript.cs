using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnoBoardSpawnScript : MonoBehaviour
{
    public GameObject[] cards;
    public Transform[] point;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i<1; i++)
        {
            Instantiate(cards[i],point[i].transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
