using System.Collections.Generic;
using UnityEngine;

public class MainSceneTrigger : MonoBehaviour
{
    private BuffTimer _buffTimer;
    [SerializeField]
    private PlayerController _playerController;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        InstantiateManager();
        InstantiateObject();
        InitManager();
        InitObject();
        StartGame();

        var buff = new List<Buff>()
        {
            BuffGenerator.Instance.Generate(SkillType.FireBall, StatType.HP,ProcessType.Single, ModificationType.Plus, 10, 2, true),
            BuffGenerator.Instance.Generate(SkillType.IceBolt, StatType.HP,ProcessType.Single, ModificationType.Multiply, 0.7f, 2, true),
            BuffGenerator.Instance.Generate(SkillType.Lightning, StatType.HP,ProcessType.Single , ModificationType.Multiply, 0.7f, 2, true),
            BuffGenerator.Instance.Generate(SkillType.SquidInk, StatType.HP,ProcessType.Single, ModificationType.Plus, -10, 2, true),
        };
        var data = new UnitStatus(_buffTimer);

        foreach (var item in buff)
        {
            data.ApplyBuff(item);
        }

    }

    private void InstantiateManager()
    {
        new InputManager();
    }

    private void InstantiateObject()
    {
        _buffTimer = gameObject.AddComponent<BuffTimer>();
        _playerController = Instantiate(_playerController);
    }

    private void InitManager()
    {

    }

    private void InitObject()
    {
        _playerController.Init(new UnitStatus(_buffTimer));
    }

    private void StartGame()
    {
        gameObject.name = "GameController";
        InputManager.Instance.SetActive(true);
    }
}

