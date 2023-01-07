using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IDefender
{
    int Health { get; set; }

    public void Defend(IAttacker attacker);

}
