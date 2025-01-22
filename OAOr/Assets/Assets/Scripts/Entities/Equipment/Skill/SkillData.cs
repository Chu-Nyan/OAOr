using System;
using System.Collections.Generic;

[Serializable]
public class SkillData
{
    public SkillType SkillType;
    public float Damage;
    public float ProgectileSpeed;
    public List<BuffType> Buffs;
}
