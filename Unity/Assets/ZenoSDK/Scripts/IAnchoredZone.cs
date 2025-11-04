using ZenoSDK.Options;
using UnityEngine;

namespace ZenoSDK
{

    // Zeno anchored zone represents a surrounding area with zone identifier and spatial information
    // for application to interact with physical world.
    public interface IAnchoredZone
    {
        // Initializes the ZenoSDK. This must be called before any further operations.
        // Returns error status if not successful.
        Status Initialize();

        // ====================================================================
        //   Blocking accessors to retrieve data from the active Zone.
        //   These readonly accessors can be called in any threads or coroutines
        //   and return immediately.
        // ====================================================================

        // Returns the current operation state.
        AnchoredZoneOperationState GetOperationState();

        // Returns the current origin mode.
        ZoneOriginMode GetOriginMode();

        // Returns the AnchoredZoneInfo status.
        AnchoredZoneStatus GetStatus();

        // Returns the AnchoredZoneInfo metadata object that encapsulates all needed information of a zone
        // or error status if not prepared or loaded.
        StatusOr<AnchoredZoneInfo> GetInfo();

        // Returns the a detected marker if exists, or a host marker.
        StatusOr<MarkerInfo> GetMarkerInfo();

        // Displays a QR code image for the current zone.
        Status ShowMarker(string extraData = null);

        // Hides the QR code image if shown.
        void HideMarker();

        // Returns the marker options for marker's visual configuration.
        MarkerOptions GetMarkerOptions();

        // Returns the navigation  options for navigation feature configuration.
        NavigationOptions GetNavigationOptions();

        // Returns the localizer options for localization feature configuration.
        LocalizerOptions GetLocalizerOptions();

        // Temporarily skip or unskip the zone streaming. During the skipping period, no frame is captured.
        // Note that a new stream is started with skip set to false by default.
        void SkipCaptureFrames(bool shouldSkip);

        // Returns the localization result if localized or error status if not localized.
        StatusOr<LocalizationResult> GetLocalizationResult();

        // Returns the wrapper game object of the processed mesh of the zone.
        StatusOr<GameObject> GetMeshWrapperObject();

        // Returns the raycast result of the current cursor. Returns null if no raycast result.
        // When zone is not ready, the cursor follows at raycast on detected planes.
        // After zone is ready, the cursor follows at raycast on the processed mesh.
        StatusOr<Pose> GetCurrentCursorPose();

        // ================================================================================
        //   Asynchronous operations to manipulate the active Zone.
        //   Operations could be triggered from any thread or coroutines asynchronously.
        // ================================================================================

        // Creates a new empty zone. Returns error status if not successful.
        // OnZoneStandby is called when the zone has been created in the cloud and is ready to
        // accept images.
        // State change: Idle -> Standby
        Status CreateZone(OnZoneStandby onZoneStandby = null);

        // Joins an existing zone created by others. Returns error status if not successful.
        // OnZoneStandby is called when the existing zone has been fetched from cloud.
        // State change: Idle -> Standby
        // Depends the active joined zone's status, the following call could be:
        //    Empty|Capturing: StartStream then FinishStream to capture the zone.
        //    Completed:       DownloadMesh to download the processed zone mesh.
        Status JoinZone(string zoneId, OnZoneStandby onZoneStandby = null);

        // Starts an async operation for marker detection.
        // OnMarkerDetected is called when an existing zone is detected through the marker.
        // State change: Idle -> Detecting
        Status StartMarkerDetection(OnMarkerDetected onMarkerDetected = null);

        // Stops the marker detection operation.
        // State change: Detecting -> Idle
        Status StopMarkerDetection();

        // Starts an async operation to start stream zone informations until the operation is stopped.
        // OnStreamUpdate is called whenever the stream has an update.
        // OnStreamFinish is called when the stream finishes, triggered by other clients.
        // State change: Standby -> Streaming
        Status StartStream(OnStreamUpdate onStreamUpdate = null, OnStreamFinish onStreamFinish = null);

        // Resets the stream to start over again.
        // State change: Streaming -> Streaming
        Status ResetStream();

        // Finishes this client's stream operation and starts to wait for the zone to be processed and downloaded.
        // OnZoneReady is called when the zone is processed and downloaded and ready for localization.
        // State change: Streaming -> Processing -> Downloading -> Ready
        Status FinishStream(OnZoneReady onZoneReady = null, OnZoneProcessUpdate onZoneProcessUpdate = null);

        // Downloads the terrain mesh for a active completely processed zone.
        // OnZoneReady is called when the zone is downloaded and ready for localization.
        // State change: Standby -> Downloading -> Ready
        Status DownloadMesh(OnZoneReady onZoneReady = null, OnMeshDownloadUpdate onMeshDownloadUpdate = null);

        // Starts an async operation to localize with the loaded or processed zone with camera images.
        // OnLocalizationUpdate is called there is a pose update of the device in the zone.
        // State change: Ready -> Localizing
        Status StartLocalization(OnLocalizationUpdate onLocalizationUpdate = null);

        // Stops the localization operation and back to ready state. This is a resumable state that user can call resume localization later.
        // State change: Localizing -> Ready
        Status StopLocalization();

        // Exits the active zone and back to idle state, whether the active zone is processing or ready.
        // State change: Detecting|Streaming|Processing|Downloading|Localizing|Ready -> Idle.
        void ExitZone();

        // ================================================================================
        //    Delegates and events in ZenoSDK.
        // ================================================================================

        delegate void OnMarkerDetected(StatusOr<MarkerInfo> marker, StatusOr<Pose> markerPose, StatusOr<Pose> localizedPose);
        delegate void OnZoneStandby(StatusOr<AnchoredZoneInfo> info);
        delegate void OnStreamUpdate(StatusOr<FrameSelectionResult> frameSelectionResult, StatusOr<ZoneStreamRenderable> streamRenderable);
        delegate void OnStreamFinish(Status status);
        delegate void OnZoneReady(StatusOr<AnchoredZoneInfo> info);
        delegate void OnLocalizationUpdate(StatusOr<Pose> localizedPose);
        delegate void OnZoneProcessUpdate(float progress);
        delegate void OnMeshDownloadUpdate(float progress);

        event OnMarkerDetected onMarkerDetected;
        event OnZoneStandby onZoneStandby;
        event OnStreamUpdate onStreamUpdate;
        event OnStreamFinish onStreamFinish;
        event OnZoneReady onZoneReady;
        event OnLocalizationUpdate onLocalizationUpdate;
        event OnZoneProcessUpdate onZoneProcessUpdate;
        event OnMeshDownloadUpdate onMeshDownloadUpdate;
    }
}
