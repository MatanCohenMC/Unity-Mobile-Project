using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public const int k_DefaultTotalHealthAmount = 3;
    public int m_HealthAmountRemain = k_DefaultTotalHealthAmount;
    public Image[] m_Hearts;
    public Sprite m_FullHeart;
    public Sprite m_EmptyHeart;

    private void Awake()
    {
        m_HealthAmountRemain = k_DefaultTotalHealthAmount;
    }

    void Start()
    {
        updateHeartAmount();
    }

    //void Update()
    //{
    //    updateHeartAmount();
    //}


    public void DecreaseOrIncreaseHeartAmount(bool i_ToIncrease)
    {
        m_HealthAmountRemain += i_ToIncrease ? 1 : -1;
        updateHeartAmount();
    }

    private void updateHeartAmount()
    {
        foreach (Image img in m_Hearts)
        {
            img.sprite = m_EmptyHeart;
        }

        for (int i = 0; i < m_HealthAmountRemain; i++)
        {
            m_Hearts[i].sprite = m_FullHeart;
        }
    }

    public void ResetLives()
    {
        m_HealthAmountRemain = k_DefaultTotalHealthAmount;
    }
}
