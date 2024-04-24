using UnityEngine;

public class PlayerPunchState : PlayerBaseState
{
	private readonly int AttackHash = Animator.StringToHash("Hook");
	private const float CrossFadeDuration = 0.1f;

	public PlayerPunchState(PlayerStateMachine stateMachine) : base(stateMachine) { }
	public override void Enter()
	{
		stateMachine.Animator.Rebind();
		stateMachine.Animator.CrossFadeInFixedTime(AttackHash, CrossFadeDuration);
	}
	public override void Tick()
	{
		// ���� �ִϸ��̼� ������ �޾ƿ´�
		AnimatorStateInfo stateInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

		// �ִϸ��̼��� �����ٸ�
		if (stateInfo.IsName("Hook") && stateInfo.normalizedTime >= 1.0f && stateInfo.normalizedTime <= 1.1f)
		{
			stateMachine.SwitchState(new PlayerMoveState(stateMachine));
		}
	}
	public override void Exit()
	{
	}

}
