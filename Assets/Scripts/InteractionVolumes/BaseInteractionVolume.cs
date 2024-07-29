using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public abstract class BaseInteractionVolume : MonoBehaviour
{
    private bool _waitingInteraction = true;

    public void ResetInteraction()
    {
        _waitingInteraction = true; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_waitingInteraction && other.CompareTag("Player"))
        {
            _waitingInteraction = false;
            OnInteraction();
        }
    }

    protected abstract void OnInteraction();
}
