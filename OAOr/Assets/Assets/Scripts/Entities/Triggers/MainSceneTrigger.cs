using UnityEngine;

public class MainSceneTrigger : MonoBehaviour
{
    private PlayerController _playerController;
    [SerializeField]
    private CameraController _camera;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        InstantiateManager();
        InstantiateObject();
        InitManager();
        InitObject();
        StartGame();
    }

    private void InstantiateManager()
    {
        new DataManager();
        new InputManager();
        new ProjectileGenerator();
        new BuffGenerator();
        new NPCGenerator();

        gameObject.AddComponent<BuffTimer>();
    }

    private void InstantiateObject()
    {
        _playerController = Instantiate(DataManager.Instance.LoadAsset<PlayerController>(Const.PlayerPrefab));
        var npc = NPCGenerator.Instance.Ready(UnitType.Enemy).Generate();
    }

    private void InitManager()
    {
        ProjectileGenerator.Instance.Init();
        BuffGenerator.Instance.Init();
    }

    private void InitObject()
    {
        _playerController.Init(DataManager.Instance.GetPlayerData(), _camera);
    }

    private void StartGame()
    {
        _camera.SetTrackingTarget(_playerController.CameraArm);
        gameObject.name = "GameController";
        InputManager.Instance.SetActive(true);
    }
}

