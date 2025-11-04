namespace ZenoSDK
{
    // Enum of zone mesh type.
    public enum AnchoredZoneMeshType
    {
        // Uncompressed obj format.
        Obj,
        // Draco compressed obj format
        DracoObj,
        // Uncompressed obj format with textured and bundled as a zip file.
        TexturedObjZip,
        // Use Nerf for reconstruction.
        Nerf
    }

    // Enum of zone status processed in the cloud.
    public enum AnchoredZoneStatus
    {
        // Zone is just created and not yet been used.
        Empty,
        // Zone is currently scanning and capturing data. 
        Capturing,
        // Zone scanning is finished and is processing in the cloud.
        Processing,
        // Zone processing has been completed and ready to use.
        Completed,
        // Zone processing has failed.
        Failed
    }

    // Represents a specific zone session.
    public struct AnchoredZoneInfo
    {
        // ID of the zone. It represents a scanning of an area. Host and guests can share the same zone with the zone ID.
        public string zoneId;

        // Current status of the zone. Only completed zone can be used for localization.
        public AnchoredZoneStatus status;

        // Type of the mesh that the zone is attached. The mesh matches the physical surroundings after localization.
        public AnchoredZoneMeshType meshType;

        // Timestamp (milliseconds since epoch) when this zone was last updated.
        public long updateTimestamp;

        // Timestamp (milliseconds since epoch) that this zone will be expired.
        public long expireTimestamp;

        // (Experimental) Detected objects inside the physical surrounding that this zone covered.
        public ObjectDetectionResult detectionResult;
    }
}
