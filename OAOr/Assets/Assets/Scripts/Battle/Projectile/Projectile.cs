using System;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private static float RemainTime = 5f;

    public ProjectileData Data;
    private readonly List<BuffType> _buffs = new();
    private float _time;

    private event Action<Projectile> Destroyed;

    private void Update()
    {
        _time += Time.deltaTime;
        transform.position += transform.forward * Data.Speed * Time.deltaTime;
        if (_time >= RemainTime)
        {
            Destory();
        }
    }

    public void AddBuff(BuffType type)
    {
        _buffs.Add(type);
    }

    public void SetForward(Vector3 forward)
    {
        var qu = Quaternion.LookRotation(forward);
        transform.rotation = qu;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플래그 체크
        if (other.TryGetComponent<IStatProvider>(out var provider) == true && provider.Status.ID != Data.OwnerID)
        {
            Debug.Log(other.gameObject.layer);
            provider.Status.Hit(Data.Damage, _buffs);
            if (Data.CanPenetration == false)
            {
                Destory();
            }
        }
    }

    public void Destory()
    {
        _time = 0;
        Destroyed?.Invoke(this);
        Destroyed = null;
        gameObject.SetActive(false);
    }

    public void RegisterDestroyed(Action<Projectile> action)
    {
        Destroyed += action;
    }
}
