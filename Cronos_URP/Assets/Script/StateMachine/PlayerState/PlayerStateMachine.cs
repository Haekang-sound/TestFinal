using UnityEngine;
[RequireComponent(typeof(InputReader))]         // ��Ʈ����Ʈ�� ��ӹ��� 
[RequireComponent(typeof(Animator))]            // ������� ��Ʈ����Ʈ RequireComponenet
[RequireComponent(typeof(CharacterController))] // �ش�������Ʈ�� �߰����ش�
public class PlayerStateMachine : StateMachine
{
	public Vector3 Velocity;
	public float MovementSpeed { get; private set; } = 5f;
	public float JumpForce { get; private set; } = 10f;
	public float LookRotationDampFactor { get; private set; } = 10f;

	public Transform MainCamera { get; private set; }
	public InputReader InputReader { get; private set; }
	public Animator Animator { get; private set; }
	public CharacterController Controller { get; private set;}

	public Transform PlayerTransform { get; private set; }

	private void Start()
	{
		MainCamera = Camera.main.transform;

		InputReader = GetComponent<InputReader>();
		Animator = GetComponent<Animator>();
		
		Controller = GetComponent<CharacterController>();
		PlayerTransform = GetComponent<Transform>();

		// ���� ���¸� �����ش�.
		//SwitchState(new PlayerMoveState(this));
		SwitchState(new PlayerIdleState(this));
	}
}
