using UnityEngine;

namespace ZenoSDK
{
    // Represents results from localization system.
    public struct LocalizationResult
    {
        public long timestamp;
        
        // A pose that localized to local or anchored zone origin, or Space with an offset to fit in device's XR session.
        public Pose localizedPose;

        // Optional Earth geodetic coordinate pose if localized in a global zone with a configured absolute offset.
        // Note: Unity cannot directly interact with this pose because it's geodetic based and usually
        // with very large values.
        public EcefPose? ecefPose;
    }
}
