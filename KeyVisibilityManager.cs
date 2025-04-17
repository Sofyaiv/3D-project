using UnityEngine;

public class KeyVisibilityManager : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject keyPrefab;      // Your key model prefab
    public Transform handTransform;   // The hand bone (e.g. RightHand)

    private GameObject _currentKey;

    void Start()
    {
        // Spawn the key in the hand at start
        _currentKey = Instantiate(keyPrefab, handTransform);
        _currentKey.transform.localPosition = Vector3.zero;
        _currentKey.transform.localRotation = Quaternion.identity;
    }

    void Update()
    {
        if (_currentKey != null && Input.GetKeyDown(KeyCode.E))
        {
            // Toggle visibility by enabling/disabling the whole GameObject
            _currentKey.SetActive(!_currentKey.activeSelf);
        }
    }
}