using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Input : MonoBehaviour
{
    private GameManager _gameManager;

    private Button saveName;
    public TMP_InputField InputField { get; private set; }

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        saveName = transform.Find("SaveNameButton").GetComponent<Button>();
        InputField = transform.Find("NameInputField").GetComponent<TMP_InputField>();
        saveName.onClick.AddListener(SaveName);
    }

    private void SaveName()
    {
        Debug.Log("save name button pressed");
        string playerName = InputField.text; // Get the text from the input field
        _gameManager.SetPlayerName(playerName); // Call the GameManager's SetPlayerName method
    }
}
