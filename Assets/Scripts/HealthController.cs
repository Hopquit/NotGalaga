using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class HealthController : MonoBehaviour
{
    
    void OnEnable ()
    {
        EventBus.StartListening ("PlayerHealthChanged", PlayerHealthChanged);
    }
    void OnDisable ()
    {
        EventBus.StopListening ("PlayerHealthChanged", PlayerHealthChanged);
    }
    void PlayerHealthChanged()
    {
        GetComponent<TextElement>().text = "Health: " + Time.deltaTime;
    }
    
}
