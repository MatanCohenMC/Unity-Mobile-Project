using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameInputUI : MonoBehaviour
{
    //private GameManager _gameManager;

    private Button saveName;
    //public TMP_InputField InputField { get; set; }
    [SerializeField] private TMP_InputField m_InputField;
    public TMP_InputField InputField
    {
        get { return m_InputField; }
        private set { m_InputField = value; }
    }




    //private void Start()
    //{
    //}

    //private void getMembersComponents()
    //{
    //    //InputField = transform.Find("NameInputField").GetComponent<TMP_InputField>();
    //}

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
