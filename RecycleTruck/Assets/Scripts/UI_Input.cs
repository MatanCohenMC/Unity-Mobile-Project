using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Input : MonoBehaviour
{
    //private GameManager _gameManager;

    private Button saveName;
    public TMP_InputField InputField { get; private set; }

    private void Awake()
    {
        //_gameManager = FindObjectOfType<GameManager>();

       
        //saveName.onClick.AddListener(SavePlayerName);
    }

    public void SavePlayerName()
    {
        Debug.Log("save name button pressed");
        //saveName = transform.Find("SaveNameButton").GetComponent<Button>();
        InputField = transform.Find("NameInputField").GetComponent<TMP_InputField>();
        string playerName = InputField.text; // Get the text from the input field
        GameManager.Instance.SetPlayerName(playerName); // Call the GameManager's SetPlayerName method
    }
}
