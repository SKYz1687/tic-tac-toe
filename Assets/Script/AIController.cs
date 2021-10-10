using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    [SerializeField] private GridSpace[] gridspaces;
    [SerializeField] private int playerSide;
    [SerializeField] private GameController gameController;

    public float thinkTime = 1f;

    private float currentTime = 0f;
    

    Dictionary<int, float> qtable;
        
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


    private void Awake()
    {
        qtable = new Dictionary<int, float>();
    }


    int[] GetBlankSpace()
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

    int Explore(int[] blankSpaces)
    {
        int index = Random.Range(0, blankSpaces.Length);

        return blankSpaces[index];
    }

    public float alpha = 0.5f;

    int[] GetNewMapAt( int slot )
    {
        int[] mymap = new int[9];
        gameController.Map.CopyTo(mymap, 0);
        mymap[slot] = playerSide;

        return mymap;
    }

    int Exploit(int[] blankSpaces)
    {

        float currentQValue = 0f;
        int currentHash = GetHashCode(gameController.Map);
        if( qtable.ContainsKey(currentHash))
        {
            currentQValue = qtable[currentHash];
        }

        float[] nextmoveQvalues = new float[blankSpaces.Length];

        for (int i = 0; i < blankSpaces.Length; i++)
        {
            int[] mymap = new int[9];
            gameController.Map.CopyTo(mymap, 0);

            mymap[blankSpaces[i]] = playerSide;

            int hash = GetHashCode(mymap);
            float nextQvalue = 0;
            
            if( qtable.ContainsKey(hash))
            {
                nextQvalue = qtable[hash];    
            }

            nextmoveQvalues[i] = nextQvalue;
        }


        float max = -1000f;
        int maxIndex = 0;
        for (int i = 0; i < nextmoveQvalues.Length; i++)
        {
            if(nextmoveQvalues[i] > max)
            {
                max = nextmoveQvalues[i];
                maxIndex = i;
            }
        }


        int[] mymap2 = GetNewMapAt(blankSpaces[maxIndex]);
        int hash2 = GetHashCode(mymap2);

        if (gameController.IsWin(mymap2, playerSide) == 1)
        {
            qtable[hash2] = 100;
        }

        float newQvalue = nextmoveQvalues[maxIndex];
        qtable[currentHash] = (1.0f - alpha) * currentQValue + (alpha) * newQvalue;
        

        return blankSpaces[maxIndex];
    }

    //todo
    public int phi = 1000;

    void PickSpot()
    {

        int[] blankSpaces = GetBlankSpace();
        int space = blankSpaces[0];

        int b = Random.Range(0, 100);
        if(b <= phi)
        {
            print("explore " + playerSide.ToString() );
            space = Explore(blankSpaces);
            phi--;
        }
        else
        {
            print("exploit " + playerSide.ToString());
            space = Exploit(blankSpaces);
        }

        gridspaces[space].SetSpace(space);
        
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
