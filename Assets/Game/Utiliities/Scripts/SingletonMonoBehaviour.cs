using UnityEngine;

namespace Utilities
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                
                var objectsOfType = FindObjectsOfType<T>(true);

                if (objectsOfType.Length != 1) return null;

                _instance = objectsOfType[0];
                
                return _instance;
            }
        }
    }
}
