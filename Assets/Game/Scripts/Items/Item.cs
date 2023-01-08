using UnityEditor.Animations;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite sprite;
    public AnimatorController controller;
    [Range(0f, 1f)]
    public float dropChance;
    public int damage;
    public bool useFlip = true;

    public virtual void OnAttack(Vector2 attackPosition, Vector2 attackDirection)
    {

    }

}
