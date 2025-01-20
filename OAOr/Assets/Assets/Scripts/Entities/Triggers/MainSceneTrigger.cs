using System.Collections.Generic;
using UnityEngine;

public class MainSceneTrigger : MonoBehaviour
{
    private BuffTimer _buffTimer;
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
    }

    private void InstantiateObject()
    {
        _buffTimer = gameObject.AddComponent<BuffTimer>();
        _playerController = Instantiate(DataManager.Instance.LoadAsset<PlayerController>(Const.PlayerPrefab));
    }

    private void InitManager()
    {

    }

    private void InitObject()
    {
        _playerController.Init(DataManager.Instance.GetPlayerData());
    }

    private void StartGame()
    {
        _camera.SetTrackingTarget(_playerController.CameraArm);
        gameObject.name = "GameController";
        InputManager.Instance.SetActive(true);
    }
}

