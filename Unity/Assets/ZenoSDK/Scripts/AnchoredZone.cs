using UnityEngine;

namespace ZenoSDK
{
    // Static helper class for common access to the AnchoredZone.
    public static class AnchoredZone
    {
        private static IAnchoredZone _instance;

        // Registers an API implementation insatnce to this helper.
        // Usually this is self registered by implementation itself at Awaken step.
        public static void Register(IAnchoredZone instance) {
            if (_instance == null)
            {
                Debug.Log("AnchoredZone singleton has been registered.");
                _instance = instance;
            }
            else
            {
                Debug.LogWarning("Cannot instantiate multiple AnchoredZone implementations.");
            }
        }

        // Unregisters the existing API implemetation.
        // Usually this is self unregistered by implmentation itself at OnDestroy step.
        public static void Unregister(IAnchoredZone instance)
        {
            if (_instance == instance)
            {
                Debug.Log("AnchoredZone singleton has been unregistered.");
                _instance = null;
            }
        }

        // Returns the AnchoredZone singleton reference,
        // or null if the game object is not loaded in the scene thus the API is not available.
        public static IAnchoredZone Get()
        {
            if (_instance != null)
            {
                return _instance;
            }
            else
            {
                Debug.Log("AnchoredZone implementation is not available.");
                return null;
            }
        }

        // Whether AnchoredZone API is available.
        public static bool IsAvailable()
        {
            return Get() != null;
        }
    }
}
