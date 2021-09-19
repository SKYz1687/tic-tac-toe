using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    GameObject[] buttons;
    
    private void Awake()
    {
        buttons = GameObject.FindGameObjectsWithTag("Gridcell");
    }

    public void ResetButton()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInChildren<Text>().text = "";
            buttons[i].GetComponent<Button>().interactable = true;
        }
    }
}
