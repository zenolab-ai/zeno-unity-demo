using UnityEngine;

namespace ZenoSDK.Options
{
    [CreateAssetMenu(fileName = "LocalizerOptions", menuName = "ZenoSDK/Localizer Options")]
    public class LocalizerOptions : ScriptableObject
    {
        // Gets camera image for localization every given interval in seconds.
        public float localizationImageInterval = 0.2f;  // 200ms
    }
}
