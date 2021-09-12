using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public Text[] buttonList;
    public GameObject gameOverPanel;
    public Text gameOverText;

    private int moveCount;

    private int playerSide;

    int[] map;

    void Awake () {
        //SetGameControllerReferenceOnButtons();
        playerSide = 1;

        gameOverPanel.SetActive (false);
        moveCount = 0;

        map = new int[9];
        for (int i = 0; i < 9; i++) {
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

    public int GetPlayerSide () {
        return playerSide;
    }

    public void EndTurn () {
        moveCount++;
        if (map[0] == playerSide && map[1] == playerSide && map[2] == playerSide) {
            GameOver ();
        }

        if (map[3] == playerSide && map[4] == playerSide && map[5] == playerSide) {
            GameOver ();
        }

        if (map[6] == playerSide && map[7] == playerSide && map[8] == playerSide) {
            GameOver ();
        }

        if (map[0] == playerSide && map[3] == playerSide && map[6] == playerSide) {
            GameOver ();
        }

        if (map[1] == playerSide && map[4] == playerSide && map[7] == playerSide) {
            GameOver ();
        }

        if (map[2] == playerSide && map[5] == playerSide && map[8] == playerSide) {
            GameOver ();
        }

        if (map[0] == playerSide && map[4] == playerSide && map[8] == playerSide) {
            GameOver ();
        }

        if (map[2] == playerSide && map[4] == playerSide && map[6] == playerSide) {
            GameOver ();
        }

        if (moveCount >= 9) {
            SetGameOverText ("It's a draw!");
        }

        ChangeSides ();
    }

    void ChangeSides () {
        playerSide = (playerSide == 1) ? 2 : 1;
    }

    void GameOver () {
        print (playerSide + " Wins!");

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