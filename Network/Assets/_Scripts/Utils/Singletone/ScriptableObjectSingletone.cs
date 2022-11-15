using UnityEngine;

public class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                T objects = Resources.Load<T>("Settings/" + typeof(T));
                _instance = objects;
            }

            return _instance;
        }
    }

    public void OnEnable()
    {
        if (Instance != null && this != _instance)
        {
            Debug.LogError(typeof(T) + " already exists in \"Resources\\Settings\\\"");
            DestroyImmediate(this);
        }
    }
}