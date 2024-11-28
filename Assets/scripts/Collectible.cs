using System;
using UnityEngine;
public class Collectible : MonoBehaviour 
{ 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            other.GetComponent<CollectibleCounter>().IncreaseCounter();
            gameObject.SetActive(false); 
        }
    }
} 