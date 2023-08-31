using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    private GameManager gameManager;

    private int countdownValue = 3;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void StartCountdown()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        countdownText.gameObject.SetActive(true); // Activate the countdown text

        while (countdownValue > 0)
        {
            countdownText.text = countdownValue.ToString(); // Update the countdown text
            yield return new WaitForSeconds(1);
            countdownValue--;
        }

        countdownText.text = "Go!";
        yield return new WaitForSeconds(1);
        countdownText.gameObject.SetActive(false); // Deactivate the countdown text
        gameManager.StartGame(); // Call the StartGame method
    }
}