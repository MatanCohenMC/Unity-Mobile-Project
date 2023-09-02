using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownManager : MonoBehaviour
{
    public static CountdownManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI countdownText;
    //private GameManager gameManager;

    private int countdownValue = 3;

    private void Awake()
    {
        Instance = this;
        //gameManager = FindObjectOfType<GameManager>();
    }

    public void StartCountdown()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        countdownText.gameObject.SetActive(true); // Activate the countdown text

        for (int i = countdownValue; i >= 1; i--)
        {
            countdownText.text = i.ToString(); // Update the countdown text
            yield return new WaitForSeconds(1);
        }

        countdownText.text = "Go!";
        yield return new WaitForSeconds(1);
        countdownText.gameObject.SetActive(false); // Deactivate the countdown text
        //gameManager.StartGame(); 
        GameManager.Instance.changeGameStateToPlaying();
    }
}