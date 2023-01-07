using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttacker
{
    public Transform Transform { get; }
    int Damage { get; set;  }
}
