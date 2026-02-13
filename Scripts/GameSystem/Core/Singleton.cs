using UnityEngine;

namespace MyGameSystem.Core
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        public static T instance;

        protected virtual void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this as T;
        }
    }
}
