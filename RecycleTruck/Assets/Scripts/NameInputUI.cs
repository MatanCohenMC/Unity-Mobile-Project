using TMPro;
using UnityEngine;

public class NameInputUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField m_InputField;
    public TMP_InputField InputField
    {
        get { return m_InputField; }
        private set { m_InputField = value; }
    }

    public void SavePlayerName()
    {
        Debug.Log("save name button pressed");
        string playerName = InputField.text; // Get the text from the input field
        GameManager.Instance.SetPlayerName(playerName); // Call the GameManager's SetPlayerName method
    }

    public void UpdateNameFieldToPlayerName()
    {
        string PlayerName = GameManager.Instance.PlayerName;

        if (PlayerName != InputField.text)
        {
            InputField.text = PlayerName;
        }
    }
}
