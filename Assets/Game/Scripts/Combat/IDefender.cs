using UnityEngine;

public interface IDefender
{
    int Health { get; set; }

    void Defend(IAttacker attacker, Vector2 attackDir, Vector2 contactPoint);

}
