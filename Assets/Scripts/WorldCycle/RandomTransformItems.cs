using System.Collections.Generic;
using UnityEngine;

public class RandomTransformItems : MonoBehaviour
{
    [SerializeField]
    private Transform[] _itemsToSwitchLocations;
    [SerializeField]
    private Transform[] _itemsToRotate;
    [SerializeField]
    private float _positionRange = 2.0f;

    private float[] _initialPositions;
    private Transform _transform;

    private void Start()
    {
        SetupInitialPositions();
    }

    [ContextMenu("Randomize")]
    public void Randomize()
    {
        RandomizeLocations();
        RandomizeRotations();
    }

    [ContextMenu("RandomizeLocations")]
    private void RandomizeLocations()
    {
        List<Transform> randomItems = new List<Transform>(_itemsToSwitchLocations);
        
        for (int i = 0; i < _initialPositions.Length; i++)
        {
            Transform item = GetRandomItem(ref randomItems);
            Vector3 position = item.localPosition;;
            if (Random.value > 0.5f)
            {
                position.z = _initialPositions[i] + Random.Range(-_positionRange, _positionRange);
            }

            item.localPosition = position;
        }
    }

    [ContextMenu("RandomizeRotations")]
    private void RandomizeRotations()
    {
        foreach (Transform item in _itemsToRotate)
        {
            float newAngle = Random.Range(-180.0f, 180.0f);
            item.rotation = Quaternion.AngleAxis(newAngle, Vector3.up);
        }
    }

    private void SetupInitialPositions()
    {
        _transform = transform;

        _initialPositions = new float[_itemsToSwitchLocations.Length];
        for (int i = 0; i < _itemsToSwitchLocations.Length; i++)
        {
            _initialPositions[i] = _itemsToSwitchLocations[i].localPosition.z;
        }

        Randomize();
    }

    private Transform GetRandomItem(ref List<Transform> items)
    {
        int index = Random.Range(0, items.Count);
        Transform result = items[index];
        items.RemoveAt(index);

        return result;
    }
}
