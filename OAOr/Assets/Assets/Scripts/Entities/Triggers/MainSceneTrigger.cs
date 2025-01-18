using UnityEngine;

public class MainSceneTrigger : MonoBehaviour
{
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
        new InputManager();
    }

    private void InstantiateObject()
    {

    }

    private void InitManager()
    {

    }

    private void InitObject()
    {

    }

    private void StartGame()
    {
        gameObject.name = "GameController";
        InputManager.Instance.SetActive(true);
    }
}
