using Library.DesignPattern;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGenerator : Singleton<ProjectileGenerator>
{
    private ObjectPooling<Projectile> _pool;
    private Dictionary<BuffType, BuffData> _buffData;

    private Projectile _new;

    public ProjectileGenerator()
    {
        _pool = new(() =>
        {
            var prefab = DataManager.Instance.LoadAsset<Projectile>(Const.ProjectilePrefab);
            return GameObject.Instantiate<Projectile>(prefab);
        });
    }

    public void Init()
    {
        _buffData = new Dictionary<BuffType, BuffData>();
    }

    public ProjectileGenerator Ready(Skill skill, int owner)
    {
        _new = _pool.Dequeue();
        var skillData = skill.SkillData;
        _new.Data = new(skillData.SkillType, owner, skillData.CanPenetration, skillData.Damage, skillData.ProgectileSpeed);
        foreach (var item in skillData.Buffs)
        {
            _new.AddBuff(item);
        }

        return this;
    }

    public ProjectileGenerator SetShootingTransform(Vector3 pos, Vector3 forward)
    {
        _new.transform.position = pos;
        _new.SetForward(forward);

        return this;
    }

    public Projectile Generator()
    {
        return _new;
    }

}