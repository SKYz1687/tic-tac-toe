using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    [SerializeField] UIController uIController;

    private int moveCount;
    private int playerSide = 1;
    private int[] map;

    public int[] Map
    {
        get {
            return map;
        }
    }


    private void Awake ()
    {
        map = new int[9];
        StartGame();
    }


    public void StartGame()
    {
        uIController.ResetButton();
        //playerSide = 1;
        moveCount = 0;

        //clear
        for (int i = 0; i < 9; i++)
        {
            map[i] = 0;
        }
    }


    public bool SetMap(int loc)
    {
        if (loc > 8) return false;
        if (loc < 0) return false;
        if (map[loc] != 0) return false;

        map[loc] = playerSide;

        return true;
    }


    public int GetPlayerSide ()
    {
        return playerSide;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns>
    /// returns 1 if win
    /// retrun 0 if draw
    /// retrun -1 if not finished
    /// </returns>
    public int IsWin(int[] map, int side)
    {
        if (map[0] == side && map[1] == side && map[2] == side) return 1;
        if (map[3] == side && map[4] == side && map[5] == side) return 1;
        if (map[6] == side && map[7] == side && map[8] == side) return 1;
        if (map[0] == side && map[3] == side && map[6] == side) return 1;
        if (map[1] == side && map[4] == side && map[7] == side) return 1;
        if (map[2] == side && map[5] == side && map[8] == side) return 1;
        if (map[0] == side && map[4] == side && map[8] == side) return 1;
        if (map[2] == side && map[4] == side && map[6] == side) return 1;

        if (moveCount >= 9) return 0;

        return -1;
    }


    public void EndTurn () 
    {
        moveCount++;
        if(IsWin(map, playerSide) == 1)
        {
            print(playerSide + " Wins!");
            StartGame();
        }
        else if (IsWin(map, playerSide) == 0)
        {
            Debug.Log("Draw");
            StartGame();
        }
        else
        {
            ChangeSides();
        }
    }


    void ChangeSides ()
    {
        playerSide = (playerSide == 1) ? 2 : 1;
    }


}