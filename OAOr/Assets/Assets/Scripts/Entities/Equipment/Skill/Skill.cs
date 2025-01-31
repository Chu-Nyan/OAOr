using UnityEngine;

public class Skill
{
    private SkillData _skillData;

    public SkillData SkillData
    {
        get => _skillData;
    }

    public Skill(SkillData skillData)
    {
        _skillData = skillData;
    }

    public void Use(Vector3 shootPos, Vector3 forward)
    {
        var pro = ProjectileGenerator.Instance
            .Ready(this)
            .SetShootingTransform(shootPos, forward)
            .Generator();
    }
}
