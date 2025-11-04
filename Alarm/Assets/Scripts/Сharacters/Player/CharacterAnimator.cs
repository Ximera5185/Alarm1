using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private Animator _animator;

    private readonly int Jumping = Animator.StringToHash(nameof(Jumping));
    private readonly int Speed = Animator.StringToHash(nameof(Speed));
    private readonly int HorizontalMove = Animator.StringToHash(nameof(HorizontalMove));
    private readonly int VerticalMove = Animator.StringToHash(nameof(VerticalMove));

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Jump(bool isJumping) 
    {
        _animator.SetBool(Jumping,isJumping);
    }

    public void Move(float deltaX,float deltaZ,float currentSpeed)
    {
        _animator.SetFloat(Speed, currentSpeed);
        _animator.SetFloat(HorizontalMove, deltaX);
        _animator.SetFloat(VerticalMove, deltaZ);
    }
}
