using UnityEngine;

public class Combat
{
    private IAttacker attacker;
    private IDefender defender;
    public Vector2 contactPoint;
    public Vector2 attackDir;

    public Combat(IAttacker attacker, IDefender defender, Vector2 attackDir, Vector2 contactPoint)
    {
        this.attacker = attacker;
        this.defender = defender;
        this.contactPoint = contactPoint;
        this.attackDir = attackDir;
    }

    public void Fight()
    {
        defender.Defend(attacker, attackDir, contactPoint);
    }
}
