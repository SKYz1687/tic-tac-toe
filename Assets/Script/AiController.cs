using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour
{
    [SerializeField] private GridSpace[] gridSpaces;
    [SerializeField] private int Playerside;
    [SerializeField] private GameController gameController;

    private float Curnutime = 0;
    private float Curnutime1 = 0;
    private int trun = 0;
    private void Start()
    {

    }

    int[] GetBlankSpace()
    {
        List<int> blackspaces = new List<int>();

        for(int i = 0; i < 9; i++)
        {
            if(gameController.Map[i] == 0)
            {
                blackspaces.Add(i);
            }
        }

        return blackspaces.ToArray();
    }


    void PickSpot()
    {
            
            if (gameController.GetPlayerSide() == Playerside)
            {
            int[] blackspaces = GetBlankSpace();

            int trun = blackspaces[Random.Range(0, blackspaces.Length)];

            gridSpaces[trun].SetSpace(trun);
            }
           
    }
    private void Update()
    {
        if (gameController.GetPlayerSide() == Playerside)
        {
            Curnutime += Time.deltaTime;
            if (Curnutime > 1)
            {
                PickSpot();
                Curnutime = 0;
            }
        }
    }
}
