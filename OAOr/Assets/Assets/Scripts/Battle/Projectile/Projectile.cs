using System;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileData Data;
    private readonly List<BuffType> _buffs = new();

    private event Action<Projectile> Destroyed;

    private void Update()
    {
        Debug.DrawLine(transform.position, transform.forward,Color.white);
        Debug.DrawLine(transform.position, transform.up,Color.red);
        transform.position += transform.forward * Data.Speed * Time.deltaTime;
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

    private void Hit(IStatProvider provider)
    {
        foreach (var type in _buffs)
        {
            var buff = BuffGenerator.Instance.Generate(type);
            provider.Status.ApplyBuff(buff);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Hit(other.GetComponent<IStatProvider>());
    }

    public void Destory()
    {
        Destroyed?.Invoke(this);
        Destroyed = null;
        gameObject.SetActive(false);
    }

    public void RegisterDestroyed(Action<Projectile> action)
    {
        Destroyed += action;
    }
}
