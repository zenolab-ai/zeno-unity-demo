using System.Collections.Generic;
using UnityEngine;
using ZenoSDK;
using ZenoSDK.UI;

public class Garden : MonoBehaviour
{
    public Canvas rootCanvas;
    public GameObject gameMenu;

    private List<GameObject> _objs = new();
    
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

        HideGameMenu();
    }

    private void OnDestroy()
    {
        ClearField();
    }

    private void OnAnchoredZoneReady(StatusOr<AnchoredZoneInfo> info)
    {
        // Called by Zeno when a scene has processed.
        if (info.HasValue)
        {
            Debug.Log($"Scanned zone is ready to use. {info.Value.status}");
        }
        else
        {
            Debug.Log($"Scanned zone failed to process.");
        }
    }

    private void OnMenuFinish()
    {
        // Called by Zeno flow finished.
        Debug.Log($"Zeno menu finished.");
        // Update UI.
        ShowGameMenu();
    }

    private void OnMenuCancel()
    {
        // Called by Zeno flow cancelled.
        Debug.Log($"Zeno menu cancelled.");
        HideGameMenu();
    }

    public void NewZone()
    {
        Debug.Log($"Start to scan a new zone.");
        ClearField();
        HideAllMenu();

        ZenoUI.Instance.RestartToCreate();
    }

    public void ShowGameMenu()
    {
        rootCanvas.gameObject.SetActive(true);
        gameMenu.SetActive(true);
    }

    public void HideGameMenu()
    {
        rootCanvas.gameObject.SetActive(true);
        gameMenu.SetActive(false);
    }
    public void HideAllMenu()
    {
        rootCanvas.gameObject.SetActive(false);
    }

    public void Plant()
    {
        if (!AnchoredZone.IsAvailable() || !AnchoredZone.Get().GetCurrentCursorPose().HasValue)
        {
            Debug.Log($"No cursor.");
            return;
        }
        Vector3 pos = AnchoredZone.Get().GetCurrentCursorPose().Value.position;
        
        // TODO
    }

    public void ClearField()
    {
        foreach (GameObject obj in _objs)
        {
            Destroy(obj);
        }
        _objs.Clear();
    }
}
