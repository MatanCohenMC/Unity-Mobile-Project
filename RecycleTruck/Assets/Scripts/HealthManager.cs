using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public const int k_DefaultTotalHealthAmount = 3;
    public int m_HealthAmountRemain = k_DefaultTotalHealthAmount;
    public Image[] m_Hearts/* = new Image[k_DefaultTotalHealthAmount]*/;
    public Sprite m_FullHeart;
    public Sprite m_EmptyHeart;

    private void Awake()
    {
        m_HealthAmountRemain = k_DefaultTotalHealthAmount;
    }
    // Update is called once per frame
    void Update()
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
}
