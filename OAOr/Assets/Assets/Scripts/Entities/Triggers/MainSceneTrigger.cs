using UnityEngine;

public class MainSceneTrigger : MonoBehaviour
{
    private void Awake()
    {
        InstantiateManager();
        InstantiateObject();
        InitManager();
        InitObject();
        StartGame();
    }

    private void InstantiateManager()
    {

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
    }
}
