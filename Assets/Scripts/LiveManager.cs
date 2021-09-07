using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LiveManager : MonoBehaviour
{
    [SerializeField] private Text liveText;

    public void MinusHeart()
    {
        liveText.text = Convert.ToString(Convert.ToInt32(liveText.text)-1);
    }

    public void PlusHeart()
    {
        liveText.text = Convert.ToString(Convert.ToInt32(liveText.text) + 1);
    }

    public int GetHeartValue()
    {
        return Convert.ToInt32(liveText.text);
    }
}