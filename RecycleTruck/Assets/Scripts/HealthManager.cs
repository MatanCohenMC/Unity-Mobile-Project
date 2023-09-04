using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class HealthManager : MonoBehaviour
{
    public const int k_DefaultTotalHealthAmount = 3;
    public int HealthAmountRemain { get; private set; }
    [SerializeField] private Image[] m_Hearts;
    public Image[] Hearts
    {
        get { return m_Hearts; }
        private set { m_Hearts = value; }
    }
    [SerializeField] private Sprite m_FullHeart;
    public Sprite FullHeart
    {
        get { return m_FullHeart; }
        private set { m_FullHeart = value; }
    }
    [SerializeField] private Sprite m_EmptyHeart;
    public Sprite EmptyHeart
    {
        get { return m_EmptyHeart; }
        private set { m_EmptyHeart = value; }
    }

    private void Awake()
    {
        GameManager.Instance.OnGameSetup += SetupLives;
    }

    void Start()
    {
        updateHeartAmount();
    }

    public void DecreaseOrIncreaseHeartAmount(bool i_ToIncrease)
    {
        HealthAmountRemain += i_ToIncrease ? 1 : -1;
        updateHeartAmount();
    }

    private void updateHeartAmount()
    {
        foreach (Image img in Hearts)
        {
            img.sprite = EmptyHeart;
        }

        for (int i = 0; i < HealthAmountRemain; i++)
        {
            Hearts[i].sprite = FullHeart;
        }
    }

    // this method setup player lives to its initial value
    public void SetupLives()
    {
        HealthAmountRemain = k_DefaultTotalHealthAmount;
        updateHeartAmount();
    }
}
