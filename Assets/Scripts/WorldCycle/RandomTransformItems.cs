using UnityEngine;

public class RandomTransformItems : MonoBehaviour
{
    [SerializeField]
    private Transform[] _items;
    [SerializeField]
    private float _positionRange = 2.0f;

    private Vector3[] _initialPositions;
    private Transform _transform;

    private void Start()
    {
        SetupInitialPositions();
    }

    [ContextMenu("Randomize")]
    public void Randomize()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            Transform itemTransform = _items[i];
            Vector3 position = Vector3.zero;

            position.z = Random.Range(-_positionRange, _positionRange);
            itemTransform.position =  _transform.position + _initialPositions[i] + position;
        }
    }

    private void SetupInitialPositions()
    {
        _transform = transform;

        _initialPositions = new Vector3[_items.Length];
        for (int i = 0; i < _items.Length; i++)
        {
            _initialPositions[i] = _items[i].localPosition;
        }
    }
}
