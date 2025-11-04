using UnityEngine;

namespace ZenoSDK.Options
{
    [CreateAssetMenu(fileName = "ScanLineRendererOptions", menuName = "ZenoSDK/Scan Line Renderer Options")]
    public class ScanLineRendererOptions : ScriptableObject
    {
        // Whether to enable scanline effect.
        public bool enabled = false;

        // Whether to loop the effect forever.
        public bool loop = true;

        // Only work with material with ScanLine shader.
        public Material material;
    }
}
