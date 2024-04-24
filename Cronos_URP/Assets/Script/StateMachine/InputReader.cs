using System;
using UnityEditor.Build;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

/// <summary>
/// �� �ڵ�� Unity ���� ������ ����Ͽ� �÷��̾� �Է��� �����ϴ� Ŭ������ �����մϴ�.
/// InputReader Ŭ������ Unity�� ���ο� InputSystem�� Ȱ���Ͽ� ���콺�� Ű���� �Է��� ó���մϴ�.
/// 
/// Player�� ��ǲ �׼��� ���� Ʈ���Ÿ� ���� �մϴ�.
/// </summary>
public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
	public Vector2 mouseDelta; // ���콺 �̵������� �޾ƿ´�
	public Vector2 moveComposite;

	public Action onMove;
	public Action onJumpPerformed;		// ������ ���� �׼��� ��� ���� ����
	public Action onLAttackPerformed;	// ������ ���� �׼��� ��� ���� ����
	public Action onRAttackPerformed;	// ������ ���� �׼��� ��� ���� ����
	
	private Controls controls;

	private void OnEnable()
	{
		if(controls != null)
		{
			return;
		}
		controls = new Controls();
		controls.Player.SetCallbacks(this); // InputReader�� IPlayerActions�� ��ӹ޾Ҵ�.
											// Actions�� �����Ѵ�.
		controls.Player.Enable();		// ��밡���� ���·� �����.
	}

	public void OnDisable()
	{
		// �÷��̾��� disable �Լ��� ȣ���Ѵ�.
		controls.Player.Disable();
	}

	// ī�޶� �̵��� ����ϴ�! 
	public void OnLook(InputAction.CallbackContext context)
	{
		mouseDelta = context.ReadValue<Vector2>();
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		moveComposite = context.ReadValue<Vector2>();
		onMove?.Invoke(); // �̵� �߻����θ� �����Ѵ�.
	}

	public void OnJump(InputAction.CallbackContext context)
	{
		if(!context.performed)
		{
			return;
		}

		onJumpPerformed?.Invoke();// onJump�� null �� �ƴ϶�� �����Ѵ�.
	}
	public void OnLAttack(InputAction.CallbackContext context)
	{
		onLAttackPerformed?.Invoke();
	}
	public void OnRAttack(InputAction.CallbackContext context)
	{
		onRAttackPerformed?.Invoke();
	}


}
