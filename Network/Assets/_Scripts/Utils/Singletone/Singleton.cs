using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                T[] objects = Resources.FindObjectsOfTypeAll<T>();
                if (objects.Length > 1)
                {
                    string str = "";
                    for (int i = 0; i < objects.Length; i++)
                    {
                        str += objects[i].name + "\n";
                    }
                    Debug.LogError($"More than 1 intance of {typeof(T)}\n" +
                                   $"{str}");
                }

                instance = Resources.FindObjectsOfTypeAll<T>()[0];

                if (instance == null)
                {
                    Debug.LogWarning("No such singleton instance in scene: " + typeof(T));
                    instance = new GameObject().AddComponent<T>();
                    instance.name = typeof(T).ToString();
                }
            }
            return instance;
        }
    }
}