using UnityEngine;

public class HealthContainer : MonoBehaviour
{
    [SerializeField] private Animator hpContainerAnimator;

    public void Enable()
    {
        hpContainerAnimator.Play("Active");
    }

    public void Disable()
    {
        hpContainerAnimator.Play("Inactive");
    }
}
