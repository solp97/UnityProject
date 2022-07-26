using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T instance
    {
        get
        {
            if(_instance==null)
            {
                _instance = FindObjectOfType<T>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance !=null)
        {
            if(_instance!=this)
            {
                Destroy(_instance);
            }
            return;
        }
        _instance = GetComponent<T>();
        DontDestroyOnLoad(gameObject);
    }
}
