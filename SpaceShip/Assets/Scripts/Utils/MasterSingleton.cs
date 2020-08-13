using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

/// <summary>
/// Inherit from this component to easily create a singleton gameobject.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class MasterSingleton<T> : MonoBehaviour where T : MasterSingleton<T>
{
    private static T _instance;

    /// <summary>
    /// Get the instance of this singleton.
    /// </summary
    public static T Instance
    {
        [DebuggerStepThrough]
        get
        {
            // Instance requiered for the first time, we look for it
            if (_instance == null)
            {
                //Log.Warning("No instance of {0} present in scene", typeof(T).Name);
                //throw new InvalidOperationException(string.Format("No instance of {0} present in scene",typeof(T).Name));
            }
            return _instance;
        }
    }

    public static bool HasInstance
    {
        [DebuggerStepThrough]
        get { return _instance != null; }
    }

    private void Register()
    {
        if (_instance != null)
        {
            Debug.Log(string.Format("More than one singleton object of type {0} exists.", typeof(T).Name));

            // Check if gameobject only contains Transform and this component
            if (GetComponents<Component>().Length == 2)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(this);
            }

            return;
        }

        _instance = (T)this;
    }

    // If no other monobehaviour request the instance in an awake function
    // executing before this one, no need to search the object.
    protected virtual void Awake()
    {
        Register();
    }

    protected virtual void OnEnable()
    {
        // In case of code-reload, this should restore the single instance
        if (_instance == null)
        {
            Register();
        }
    }

}

