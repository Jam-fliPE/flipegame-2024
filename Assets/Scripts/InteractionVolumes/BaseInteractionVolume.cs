using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public abstract class BaseInteractionVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnInteraction();
        }
    }

    protected abstract void OnInteraction();
}
