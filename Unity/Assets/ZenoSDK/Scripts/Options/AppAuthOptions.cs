using UnityEngine;

namespace ZenoSDK.Options
{
    [CreateAssetMenu(fileName = "AppAuthOptions", menuName = "ZenoSDK/App Auth Options")]
    public class AppAuthOptions : ScriptableObject
    {
        // API key
        public string appKey;

        // API secret
        public string appSecret;
    }
}
