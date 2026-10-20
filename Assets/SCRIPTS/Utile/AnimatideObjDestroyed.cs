using Unity.VisualScripting;
using UnityEngine;

public class AnimatideObjDestroyed : MonoBehaviour
{
    [SerializeField] private Animator animator;
    void Start()
    {
        Destroy(gameObject, animator.GetCurrentAnimatorClipInfo(0).Length);
    }
}
