using ZenoSDK;
using ZenoSDK.UI;
using UnityEngine;

public class ZenoDemo : MonoBehaviour
{
    private ExampleGame _game;

    private void Awake()
    {
        _game = GetComponent<ExampleGame>();
    }

    private void Start()
    {
        if (AnchoredZone.IsAvailable())
        {
            Debug.Log($"Zeno API is available.");
            // Application can listen to some events.
            AnchoredZone.Get().onZoneReady += OnAnchoredZoneReady;
            ZenoUI.Instance.onMenuFinish += OnMenuFinish;
            ZenoUI.Instance.onMenuCancel += OnMenuCancel;
        }
        else
        {
            Debug.Log($"Zeno API is not available.");
        }

        _game.ShowMirrorSceneButtons();
    }

    private void OnDestroy()
    {
        ResetArGame();
    }

    public void CreateArGame()
    {
        Debug.Log($"Start Zeno menu to create scene.");

        _game.ClearAll();
        _game.HideButtons();

        ZenoUI.Instance.RestartToCreate();
    }

    public void JoinArGame()
    {
        Debug.Log($"Start Zeno menu to join scene.");

        _game.ClearAll();
        _game.HideButtons();

        ZenoUI.Instance.RestartToJoin();
    }

    public void ResetArGame()
    {
        Debug.Log($"Exit scene and reset all.");
        _game.ClearAll();

        ZenoUI.Instance.ExitScene();
    }

    public void ShowQrCode()
    {
        ZenoUI.Instance.TriggerShowQrCode();
    }

    public void OnAnchoredZoneReady(StatusOr<AnchoredZoneInfo> info)
    {
        // Called by Zeno when a scene has processed.
        if (info.HasValue)
        {
            Debug.Log($"Example scanned zone is ready to use. {info.Value.status}");
        }
        else
        {
            Debug.Log($"Example scanned scene failed to process.");
        }
    }

    public void OnMenuFinish()
    {
        // Called by Zeno flow finished.
        Debug.Log($"Example Zeno menu finished.");
        // Update UI.
        _game.ShowGameButtons();
    }

    public void OnMenuCancel()
    {
        // Called by Zeno flow cancelled.
        Debug.Log($"Example Zeno menu cancelled.");
        _game.ShowMirrorSceneButtons();
    }
}
