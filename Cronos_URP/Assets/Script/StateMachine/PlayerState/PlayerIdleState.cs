using System.Runtime.InteropServices;
using UnityEngine;


// �⺻����
// �ִϸ��̼� : idle
// ��
public class PlayerIdleState : PlayerBaseState
{
	private readonly int idleHash = Animator.StringToHash("Idle");
	private readonly float duration = 0.3f;
	private bool isMove = false;

	public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }
	
	public override void Enter()
	{
		// 1. Idle �ִϸ��̼��� ����Ұ�
		stateMachine.Animator.CrossFadeInFixedTime(idleHash, duration);

		stateMachine.InputReader.onMove += IsMove;

    }
	public override void Tick()
	{
		// playerComponent�������� ���� ������� �ʴٸ�
		if (!stateMachine.Controller.isGrounded)
		{
			stateMachine.SwitchState(new PlayerFallState(stateMachine)); // ���¸� �����ؼ� �����Ѵ�.
		}
		// �����̸� == �̵�Ű�Է��� ������
		if (isMove)
		{
			// �̵����·� �ٲ��
			SwitchToMoveState();
		}
	}
	public override void Exit()
	{
		stateMachine.InputReader.onMove -= IsMove;
	}

	private void IsMove()
	{
		isMove = true;
	}

	private void SwitchToMoveState()
	{
		stateMachine.SwitchState(new PlayerMoveState(stateMachine));
	}
}
