using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    private int countdownValue = 3;

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
        GameManager.Instance.changeGameStateToPlaying();
    }
}