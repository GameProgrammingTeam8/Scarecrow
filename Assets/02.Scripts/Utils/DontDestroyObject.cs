using UnityEngine;

public class DontDestroyObject : MonoBehaviour
{
    private void Awake()
    {
        var obj = FindObjectsByType<DontDestroyObject>(
            FindObjectsSortMode.None
        );
        
        if (obj.Length == 1) DontDestroyOnLoad(gameObject);
        else Destroy(gameObject);
    }
}