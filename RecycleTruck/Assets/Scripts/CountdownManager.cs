using System.Collections;
using TMPro;
using UnityEngine;

public class CountdownManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_CountdownText;

    private const int k_CountdownValue = 3;

    // This method initiates the countdown.
    public void StartCountdown()
    {
        StartCoroutine(CountdownCoroutine());
    }

    // this method is responsible on activating the countDown
    private IEnumerator CountdownCoroutine()
    {
        m_CountdownText.gameObject.SetActive(true); // Activate the countdown text

        for (int i = k_CountdownValue; i >= 1; i--)
        {
            m_CountdownText.text = i.ToString(); // Update the countdown text
            yield return new WaitForSeconds(1);
        }

        m_CountdownText.text = "Go!";
        yield return new WaitForSeconds(1);
        m_CountdownText.gameObject.SetActive(false); // Deactivate the countdown text
        GameManager.Instance.changeGameStateToPlaying();
    }
}