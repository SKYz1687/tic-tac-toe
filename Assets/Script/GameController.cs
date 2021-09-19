using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public Text[] buttonList;
    public GameObject gameOverPanel;
    public Text gameOverText;

    [SerializeField] UIController uIController;

    private int moveCount;

    private int playerSide;

    int[] map;

    void Awake () {
        //SetGameControllerReferenceOnButtons();
        

        //gameOverPanel.SetActive (false);
        

        map = new int[9];
        StartGame();
    }
    public void StartGame()
    {
        uIController.ResetButton();
        playerSide = 1;
        moveCount = 0;

        //clear
        for (int i = 0; i < 9; i++)
        {
            map[i] = 0;
        }
    }


    //void SetGameControllerReferenceOnButtons()
    //{
    //    for (int i = 0; i < buttonList.Length; i++)
    //    {
    //        buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
    //    }
    //}

    public bool SetMap(int loc)
    {
        if (loc > 8) return false;
        if (loc < 0) return false;
        if (map[loc] != 0) return false;

        map[loc] = playerSide;

        return true;
    }

    public int GetPlayerSide () {
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
    public int IsWin()
    {
        if (map[0] == playerSide && map[1] == playerSide && map[2] == playerSide)
        {
            return 1;
        }

        if (map[3] == playerSide && map[4] == playerSide && map[5] == playerSide)
        {
            return 1;
        }

        if (map[6] == playerSide && map[7] == playerSide && map[8] == playerSide)
        {
            return 1;
        }

        if (map[0] == playerSide && map[3] == playerSide && map[6] == playerSide)
        {
            return 1;
        }

        if (map[1] == playerSide && map[4] == playerSide && map[7] == playerSide)
        {
            return 1;
        }

        if (map[2] == playerSide && map[5] == playerSide && map[8] == playerSide)
        {
            return 1;
        }

        if (map[0] == playerSide && map[4] == playerSide && map[8] == playerSide)
        {
            return 1;
        }

        if (map[2] == playerSide && map[4] == playerSide && map[6] == playerSide)
        {
            return 1;
        }

        if (moveCount >= 9)
        {
            return 0;
        }

        return -1;
    }
    public void EndTurn () 
    {
        moveCount++;
        if(IsWin() == 1)
        {
            GameOver();
        }
        if(IsWin() == 0)
        {
            Debug.Log("Draw");
            StartGame();
        }
        IsWin();
        ChangeSides();
    }

    void ChangeSides () {
        playerSide = (playerSide == 1) ? 2 : 1;
    }

    void GameOver () {
        print (playerSide + " Wins!");
        StartGame();
        // for (int i = 0; i < buttonList.Length; i++) {
        //     buttonList[i].GetComponentInParent<Button> ().interactable = false;
        // }
        // SetGameOverText (playerSide + " Wins!");
    }

    void SetGameOverText (string value) {
        // gameOverPanel.SetActive (true);
        // gameOverText.text = value;
    }
}