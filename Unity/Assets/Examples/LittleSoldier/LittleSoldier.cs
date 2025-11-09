using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZenoSDK;
using ZenoSDK.Renderers;
using ZenoSDK.UI;

public class LittleSoldier : MonoBehaviour
{
    public StaticMeshRenderer staticMeshRenderer;
    public NavMeshRenderer navMeshRenderer;
    
    public Canvas rootCanvas;
    public GameObject gameMenu;
    
    public GameObject soldierPrefab;
    public GameObject obstaclePrefab;

    private List<GameObject> _soldiers = new();
    private List<GameObject> _obstacles = new();

	private void Start()
    {
        if (AnchoredZone.IsAvailable())
        {

			Debug.Log($"Zeno API is available.");
            // Application can listen to some events.
            AnchoredZone.Get().onZoneReady += OnZoneReady;
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
        ClearObjects();
    }

    public void OnMenuFinish()
    {
        // Called by ZenoUI finish dialog.
        Debug.Log($"Scan flow finished.");

        ShowGameMenu();
    }

    public void OnMenuCancel()
    {
        // Called by ZenoUI cancel dialog.
        Debug.Log($"Scan flow cancelled.");

        HideGameMenu();
    }
    
    public void OnZoneReady(StatusOr<AnchoredZoneInfo> zoneInfo)
    {
        // Called by Zone flowwhen a zone has processed.
        if (zoneInfo.HasValue)
        {
            Debug.Log($"Example scanned zone is ready to use. {zoneInfo.Value.status}");
        }
        else
        {
            Debug.Log($"Example scanned zone failed to process.");
        }
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
        gameMenu.SetActive(false);
    }


    public void NewZone()
    {
        // Clear all
        ClearObjects();

        HideAllMenu();

        ZenoUI.Instance.RestartToCreate();
    }

    public void SpawnSoldier()
    {
        var pose = AnchoredZone.Get().GetCurrentCursorPose();
        if (pose.HasValue)
        {
            Vector3 orientation = pose.Value.rotation.eulerAngles;
            orientation.x = 0;
            orientation.z = 0;
            var soldier = Instantiate(soldierPrefab, pose.Value.position, Quaternion.Euler(orientation));
            _soldiers.Add(soldier);
        }
    }

    public void SpawnObstacle()
    {
        var pose = AnchoredZone.Get().GetCurrentCursorPose();
        if (pose.HasValue)
        {
            Vector3 orientation = pose.Value.rotation.eulerAngles;
            orientation.x = 0;
            orientation.z = 0;
            var obstacle = Instantiate(obstaclePrefab, pose.Value.position, Quaternion.Euler(orientation));
            _obstacles.Add(obstacle);
        }
    }

    public void ClearObjects()
    {
        foreach (GameObject soldier in _soldiers)
        {
            Destroy(soldier);
        }
        _soldiers.Clear();
        foreach (GameObject obstacle in _obstacles)
        {
            Destroy(obstacle);
        }
        _obstacles.Clear();
    }

    public void ToggleMesh(Toggle change)
    {
        print("ToggleMesh:" + change.isOn);
        staticMeshRenderer.options.withOcclusion = !change.isOn;
        staticMeshRenderer.options.receivesShadow = !change.isOn;
    }
    
    public void ToggleNavMesh(Toggle change)
    {
        print("ToggleNavMesh:" + change.isOn);
        navMeshRenderer.options.visible = change.isOn;
    }

    private void Update()
    {
        var pose = AnchoredZone.Get().GetCurrentCursorPose();
        if (pose.HasValue)
        {
            foreach (var bot in _soldiers)
            {
                var navigationBot = bot.GetComponent<NavigationBot>();
                if (navigationBot != null)
                {
                    navigationBot.SetDestination(pose.Value.position);
                }
            }
        }
    }
}
