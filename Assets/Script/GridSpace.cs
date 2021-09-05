using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour
{
    public Button button;
    public Text buttontext;
    public string playerSide;

    private GameController gameController;

    public void setGameControllerReference (GameController controller)
    {
        gameController = controller;
    }
    public void SetSpace()
    {
        buttontext.text = playerSide;
        button.interactable = false;
    }
}
