﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour 
{
    public Button button;
    public Text buttontext;

    private GameController gameController;

    private void Start () {
        GameObject g = GameObject.FindGameObjectWithTag ("GameController");
        gameController = g.GetComponent<GameController> ();
    }

    // public void SetGameControllerReference (GameController controller) {
    //     gameController = controller;
    // }

    public void SetSpace (int slot_number) {
        
        bool result = gameController.SetMap(slot_number);
        if(result)
        {
            int playerSide = gameController.GetPlayerSide();
            buttontext.text = (playerSide == 1) ? "X" : "O";
            button.interactable = false;
            gameController.EndTurn();
        }
        else
        {
            Debug.Log("Setting slot is false");
        }
    }
}