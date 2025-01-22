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

    public void Use(Vector3 shootPos, Vector3 shootDir)
    {
        // TODO : 발사 하기
        // 투사체를 Data를 이용하여 만들기
    }
}
