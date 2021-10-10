using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    int[] A = { 2, 1, 15, 7, 9, 50 };
    int H = 0;
    int In = 0;
    
    private void Start()
    {
        FineMax();    
    }

    void FineMax()
    {
        for(int i = 0; i < A.Length; i++)
        {
            if (A[i] > H)
            {
                H = A[i] ;
            }
            if(A[i] == H)
            {
                In = i;
            }
        }
        Debug.Log(H);
        Debug.Log(In);
    }
    void Dir()
    {
        
    }
    
}
