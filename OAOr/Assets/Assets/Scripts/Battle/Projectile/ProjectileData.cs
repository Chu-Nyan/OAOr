public struct ProjectileData
{
    public SkillType Type;
    public int OwnerID;
    public bool CanPenetration;
    public float Damage;
    public float Speed;

    public ProjectileData(SkillType type, int owner, bool canPenetration, float damage, float speed)
    {
        Type = type;
        OwnerID = owner;
        CanPenetration = canPenetration;
        Damage = damage;
        Speed = speed;
    }
}
