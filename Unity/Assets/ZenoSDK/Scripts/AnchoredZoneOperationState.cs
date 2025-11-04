namespace ZenoSDK
{
    // Operation states for a given AnchoredZone session.
    public enum AnchoredZoneOperationState
    {
        // Inactive state
        Uninitialized,                        // The system is not yet initialized.
        Idle,                                 // The system is inactive without an active zone.

        // Zone preparing states
        Standby,                              // System has an active zone but not running any operation.
        Detecting,                            // System is detecting markers nearby to activate a zone.
        Streaming,                            // System is streaming image and renderable for the current zone.
        Processing,                           // System is processing the captured active zone in the cloud.
        Downloading,                          // System is downloading an active zone which is processed.

        // Post zone preparation states
        Ready,                                // System has prepared the active zone and is ready for localiztion.
        Localizing                            // System is localizing with active zone info.
    }
}
