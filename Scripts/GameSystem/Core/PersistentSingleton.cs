using UnityEngine;

namespace MyGameSystem.Core
{
    /// <summary>
    /// Only suitable for 只有资产文件的类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        public static T instance { get; private set; }

        protected virtual void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this as T;

            DontDestroyOnLoad(gameObject);
        }
    }
}
