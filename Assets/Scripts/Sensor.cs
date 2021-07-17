using System;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public event Action GameOverEvent;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.SetActive(false);
        GameOverEvent?.Invoke();
    }
}
