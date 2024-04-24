using System.Globalization;
using System.Resources;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public abstract class PlayerBaseState : State
{
	// ������ �б��������� ����
	protected readonly PlayerStateMachine stateMachine;

	protected PlayerBaseState(PlayerStateMachine stateMachine)
	{
		this.stateMachine = stateMachine;
	}

	/// <summary>
	/// ī�޶� ������ �������
	/// Player�� �̵������� ���Ѵ�.
	/// </summary>
	protected void CalculateMoveDirection()
	{
		// ������Ʈ �ӽſ� ����ִ� ī�޶� ������ �������
		// ī�޶��� ����, �¿� ���͸� �����Ѵ�.
		Vector3 cameraForward = new(stateMachine.MainCamera.forward.x, 0, stateMachine.MainCamera.forward.z);
		Vector3 cameraRight = new(stateMachine.MainCamera.right.x, 0, stateMachine.MainCamera.right.z);

		// �̵����ͻ���,
		// ī�޶��� ���溤�Ϳ� ��ǲ�� move.y ��ġ�� ���Ѵ�,
		// ī�޶��� �¿캤�Ϳ� ��ǲ�� movecomposite.x�� ���Ѵ�.
		Vector3 moveDirection	= cameraForward.normalized * stateMachine.InputReader.moveComposite.y	// ����
								+ cameraRight.normalized * stateMachine.InputReader.moveComposite.x;	// �Ĺ�

		// ���¸ӽ��� �ӵ��� �̵����Ϳ� �ӷ��� ���̴�.
		stateMachine.Velocity.x = moveDirection.x * stateMachine.MovementSpeed;
		stateMachine.Velocity.z = moveDirection.z * stateMachine.MovementSpeed;
	}

	/// <summary>
	/// �÷��̾ �̵��������� ȸ����Ų��.
	/// </summary>
	protected void FaceMoveDirection()
	{

		Vector3 faceDirection = new(stateMachine.Velocity.x, 0f, stateMachine.Velocity.z);

		// �̵��ӵ��� ���ٸ�
		if (faceDirection == Vector3.zero)
		{
			// �ƹ��͵� ���� �ʰڴ�.
			return;
		}
		// �÷��̾��� ȸ���� ���� ���������� ���·� �̷������. 
		stateMachine.transform.rotation = Quaternion.Slerp(stateMachine.transform.rotation, Quaternion.LookRotation(faceDirection), stateMachine.LookRotationDampFactor * Time.deltaTime);
	}

	/// <summary>
	/// �߷��� �����Ѵ�.
	/// �� �� �𸣰���
	/// </summary>
	protected void ApplyGravity()
	{
		// �÷��̾��� Y������ ���ϴ� ����
		// �߷º��� ���� ��
		if (stateMachine.Velocity.y > Physics.gravity.y)
		{
			// �÷��̾ ���߷��� �����Ѵ�.
			stateMachine.Velocity.y += Physics.gravity.y * Time.deltaTime;
		}
	}

	/// <summary>
	/// Player�� ���� �ִ� �����͸� �����ؼ�
	/// Player�� �̵���Ų��
	/// </summary>
	protected void Move()
	{
		// CharacterController������Ʈ�� �̿��ؼ� ĳ���͸� �̵���Ų��.
		stateMachine.Controller.Move(stateMachine.Velocity * Time.deltaTime);

	}


}