using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ��� stateMachine�� ����̵Ǵ� SateMachine Ŭ����
public abstract class StateMachine : MonoBehaviour
{
	private State currentState; // ����
    
	// ���¸� �����ϴ� �Լ�
	public void SwitchState(State state)
	{
		currentState?.Exit();	// ���� ���¸� Ż���մϴ�.
		currentState = state;	// ���ο� ���·� �����մϴ�.
		currentState.Enter();	// ���ο� ���·� �����մϴ�.
	}

    // Update is called once per frame
    private void Update()
    {
		// ���� ���¸� �����Ѵ�.
        currentState?.Tick();
    }
}
