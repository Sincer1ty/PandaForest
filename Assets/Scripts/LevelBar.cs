using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxLevel(int level)
    {
        slider.maxValue = level;
        slider.value = level;
    }
    public void SetLevel(int level)
    {
        slider.value = level;
    }
}
