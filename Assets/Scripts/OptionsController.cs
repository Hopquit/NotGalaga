using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public Slider slider;
    void OnDisable()
    {
        PlayerPrefs.SetFloat("volume", slider.value);
    }
    void OnEnable()
    {
        slider.value = PlayerPrefs.GetFloat("volume");
    }
}