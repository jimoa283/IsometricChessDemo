using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TipRange : MonoBehaviour
{
    private UnityAction enterAction;
    private UnityAction exitAction;



   public void Init(UnityAction enterAction,UnityAction exitAction)
    {
        this.enterAction = enterAction;
        this.exitAction = exitAction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {            
            enterAction?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            exitAction?.Invoke();
        }
    }
}
