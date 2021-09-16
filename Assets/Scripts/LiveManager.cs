using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Обеспечивает управление количеством жизней игрока.
/// </summary>
public class LiveManager : MonoBehaviour
{
    [SerializeField] private Text liveText;

    /// <summary>
    /// Уменьшает количество жизней игрока на 1.
    /// </summary>
    public void MinusHeart()
    {
        liveText.text = Convert.ToString(Convert.ToInt32(liveText.text)-1);
    }

    /// <summary>
    /// Увеличивает количество жизней игрока на 1.
    /// </summary>
    public void PlusHeart()
    {
        liveText.text = Convert.ToString(Convert.ToInt32(liveText.text) + 1);
    }

    /// <summary>
    /// Возвращает текущее количество жизней у игрока.
    /// </summary>
    /// <returns>Количество жизней.</returns>
    public int GetHeartValue()
    {
        return Convert.ToInt32(liveText.text);
    }
}