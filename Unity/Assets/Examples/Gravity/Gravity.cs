using System.Collections.Generic;
using UnityEngine;
using ZenoSDK;
using ZenoSDK.UI;

public class Gravity : MonoBehaviour
{
    public Canvas rootCanvas;
    public GameObject gameButtons;
    public Material objMaterial;
    
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

        ResetGameUI();
    }

    private void OnDestroy()
    {
        ClearObjects();
    }

    private void OnAnchoredZoneReady(StatusOr<AnchoredZoneInfo> info)
    {
        // Called by Zeno when a scene has processed.
        if (info.HasValue)
        {
            Debug.Log($"Example scanned zone is ready to use. {info.Value.status}");
        }
        else
        {
            Debug.Log($"Example scanned zone failed to process.");
        }
    }

    private void OnMenuFinish()
    {
        // Called by Zeno flow finished.
        Debug.Log($"Example Zeno menu finished.");
        // Update UI.
        ShowGameUI();
    }

    private void OnMenuCancel()
    {
        // Called by Zeno flow cancelled.
        Debug.Log($"Example Zeno menu cancelled.");
        ResetGameUI();
    }

    public void CreateZone()
    {
        Debug.Log($"Start to create zone.");
        ClearObjects();
        HideGameUI();

        ZenoUI.Instance.RestartToCreate();
    }

    public void ExitZone()
    {
        Debug.Log($"Exit zone and reset.");
        ClearObjects();
        ResetGameUI();

        ZenoUI.Instance.ExitScene();
    }

    public void ShowGameUI()
    {
        rootCanvas.gameObject.SetActive(true);
        gameButtons.SetActive(true);
    }

    public void ResetGameUI()
    {
        rootCanvas.gameObject.SetActive(true);
        gameButtons.SetActive(false);
    }
    public void HideGameUI()
    {
        rootCanvas.gameObject.SetActive(false);
        gameButtons.SetActive(false);
    }

    public void DropObjects()
    {
        if (!AnchoredZone.IsAvailable() || !AnchoredZone.Get().GetCurrentCursorPose().HasValue)
        {
            Debug.Log($"No cursor.");
            return;
        }
        Vector3 pos = AnchoredZone.Get().GetCurrentCursorPose().Value.position;
        
        // Just to preserve collider classes.
        Collider _; _ = new SphereCollider(); _ = new BoxCollider();

        for (int i = 0; i < 30; i++)
        {
            PrimitiveType primitiveType = (PrimitiveType)(i % 4);
            GameObject obj = GameObject.CreatePrimitive(primitiveType);
            _objs.Add(obj);
            obj.transform.SetPositionAndRotation(
                new Vector3(Random.Range(pos.x - 0.2f, pos.x + 0.2f), Random.Range(2.0f, 4.0f), Random.Range(pos.z - 0.2f, pos.z + 0.2f)),
                Random.rotation);
            float size = Random.Range(0.01f, 0.1f);
            obj.transform.localScale = new Vector3(size, size, size);
            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
            renderer.material = objMaterial;
            Rigidbody rigid = obj.AddComponent<Rigidbody>();
            rigid.useGravity = true;
            rigid.isKinematic = false;
        }
    }

    public void ClearObjects()
    {
        foreach (GameObject obj in _objs)
        {
            Destroy(obj);
        }
        _objs.Clear();
    }
}
