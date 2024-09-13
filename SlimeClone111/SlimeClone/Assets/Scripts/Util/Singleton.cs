using UnityEngine;

namespace SingletonPattern
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static readonly object _lock = new object();
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance = (T)FindObjectOfType(typeof(T));

                        if (_instance == null)
                        {
                            GameObject obj = new GameObject();
                            _instance = obj.AddComponent(typeof(T)) as T;
                            obj.name = typeof(T).ToString();

                            //��� Scene���� ��� �־�� �� �� Ȱ��ȭ.
                            DontDestroyOnLoad(obj);
                        }
                    }
                }
                return _instance;
            }
        }


    }
}