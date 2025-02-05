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

    public void Use(int id, Vector3 shootPos, Vector3 forward)
    {
        var pro = ProjectileGenerator.Instance
            .Ready(this, id)
            .SetShootingTransform(shootPos, forward)
            .Generator();
    }
}
