public struct ProjectileData
{
    public SkillType Type;
    public bool CanPenetration;
    public float Damage;
    public float Speed;

    public ProjectileData(SkillType type, bool canPenetration, float damage, float speed)
    {
        Type = type;
        CanPenetration = canPenetration;
        Damage = damage;
        Speed = speed;
    }
}
