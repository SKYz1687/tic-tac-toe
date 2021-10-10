using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    [SerializeField] private GameController gameController;

    [SerializeField] private GridSpace[] gridspaces;
    [SerializeField] private int playerSide;

    [SerializeField] private int phi = 1000;
    [SerializeField] private float thinkTime = 1f;
    [SerializeField] private float alpha = 0.5f;

    private float currentTime = 0f;

    private Dictionary<int, float> qtable;
        
    int GetHashCode(int[] values)
    {
        int result = 0;
        int shift = 0;
        for (int i = 0; i < values.Length; i++)
        {
            shift = (shift + 11) % 21;
            result ^= (values[i] + 1024) << shift;
        }
        return result;
    }


    private int[] GetBlankSpace()
    {
        List<int> blankspaces = new List<int>();

        for (int i = 0; i < 9; i++)
        {
            if(gameController.Map[i] == 0)
            {
                blankspaces.Add(i);
            }
        }

        return blankspaces.ToArray();
    }
    

    int[] GetNewMapAt( int slot )
    {
        int[] mymap = new int[9];
        gameController.Map.CopyTo(mymap, 0);
        mymap[slot] = playerSide;

        return mymap;
    }


    private bool IsWin(int[] map)
    {
        return gameController.IsWin(map, playerSide) == 1;
    }


    private int GetMaxIndex(float[] values)
    {
        float max = Mathf.NegativeInfinity;
        int maxIndex = 0;
        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] > max)
            {
                max = values[i];
                maxIndex = i;
            }
        }

        return maxIndex;
    }


    private int Exploit(int[] blankSpaces)
    {

        // get possible qvalues for all next stages
        float[] nextmoveQvalues = new float[blankSpaces.Length];

        for (int i = 0; i < blankSpaces.Length; i++)
        {
            int[] tempMap = GetNewMapAt(blankSpaces[i]);
            int hash = GetHashCode(tempMap);
            float nextQvalue = qtable.ContainsKey(hash)?qtable[hash]:0f;    

            nextmoveQvalues[i] = nextQvalue;
        }

        // Find maximum qvalue
        int maxIndex = GetMaxIndex(nextmoveQvalues);


        // Give a reward (100) if the player win
        int[] mapInNextMove = GetNewMapAt(blankSpaces[maxIndex]);
        if (IsWin(mapInNextMove))
        {
            qtable[GetHashCode(mapInNextMove)] = 100;
        }

        //todo how about losing?
        //???

        // Update qtable
        float currentQValue = 0f;
        int currentHash = GetHashCode(gameController.Map);
        currentQValue = qtable.ContainsKey(currentHash)? qtable[currentHash]:0 ;
        
        qtable[currentHash] = (1.0f - alpha) * currentQValue + (alpha) * nextmoveQvalues[maxIndex];
        

        return blankSpaces[maxIndex];
    }


    private int Explore(int[] blankSpaces)
    {
        int index = Random.Range(0, blankSpaces.Length);

        return blankSpaces[index];
    }


    private void PickSpot()
    {

        int[] blankSpaces = GetBlankSpace();
        int space = blankSpaces[0];

        int b = Random.Range(0, 100);
        if(b <= phi)
        {
            print("explore " + playerSide.ToString() );
            space = Explore(blankSpaces);

            // there are 2% chance that the AI picks randomly
            phi--;
            phi = phi < 2 ? 2 : phi;
        }
        else
        {
            print("exploit " + playerSide.ToString());
            space = Exploit(blankSpaces);
        }

        gridspaces[space].SetSpace(space);
        
    }


    private void Awake()
    {
        qtable = new Dictionary<int, float>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            foreach (var v in qtable)
            {
                print("qtable " + v.ToString());
            }
        }

        currentTime += Time.deltaTime;
        if(currentTime > thinkTime)
        {
            if (gameController.GetPlayerSide() == playerSide)
            {
                PickSpot();
            }

            currentTime = 0;
            
        }

        if(gameController.GetPlayerSide() != playerSide)
        {
            currentTime = 0;
        }
        
    }


}
