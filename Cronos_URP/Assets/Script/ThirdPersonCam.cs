using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
	// ����ħ
	[Header("References")]

	public Transform orientation;	// ����
	public Transform player;		// �÷��̾� �ֻ��� ������Ʈ
	public Transform playerObj;		// ��, �浹
	public Rigidbody rb;

	public float rotationSpeed;

	public Transform combatLookAt;

	public GameObject thirdPersonCam;
	public GameObject combatCam;
	public GameObject topDownCam;

	public CameraStyle currentStyle;

	public enum CameraStyle
	{
		Basic,
		Combat,
		Topdown
	}

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false; // ���콺 �Ⱥ��̰� �ϱ�
		SwitchCameraSytle(currentStyle);
	}

	void Update()
	{
		// switch sytles 
		if (Input.GetKeyDown(KeyCode.Alpha1))		// 1���� ������
		{
			SwitchCameraSytle(CameraStyle.Basic);	// ī�޶� baisc����
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			SwitchCameraSytle(CameraStyle.Combat);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			SwitchCameraSytle(CameraStyle.Topdown);
		}

		//rotate orientation(����)
		Vector3 viewDir =	// ������ ī�޶����忡�� �ٶ󺸴� �÷��̾� �����̴�.
			player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
		orientation.forward = viewDir.normalized; // ĳ������ ������ ������ ����

		// rotate player object(�� ȸ��)
		if(currentStyle == CameraStyle.Basic || currentStyle == CameraStyle.Topdown) 
		{
			float horizontalInput = Input.GetAxisRaw("Horizontal");
			float verticalInput = Input.GetAxis("Vertical");
			
			// �Է��� ���� ������ �����Ѵ�.
			// ���� ĳ���� ���� * ����(+,_) + ���� ĳ���� �¿� * ����(+,-)
			// �� �̿��ؼ� ������ �Է¹޴´�
			Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
			if (inputDir != Vector3.zero)
			{
				// ��ǲ���� ���Ͱ� 0�� �ƴ϶��(�Է��� �޾��� ���)
				// �÷��̾��� ���溤�ʹ� ���� ���溤�Ϳ��� ��ǲ���⺤�͹������� ȸ���ӵ���ŭ �������������� ������ �����δ�.
				player.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
			}
		} // ī�޶���°� Combat������ ���
		else if(currentStyle == CameraStyle.Combat)
		{
			// �������� ���ʹ� ī�޶󿡼� combatLookAt������Ʈ�� �ٶ� �����̴�.
			Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
			// �÷��̾��� ������ combatlookat�� ����� ����.
			orientation.forward = dirToCombatLookAt.normalized;
			// ���� ���溤�͵� combatlookat�� ����� ����.
			playerObj.forward = dirToCombatLookAt.normalized;
		}
	}

	// ī�޶� ��Ÿ�� �ٲٱ�
	void SwitchCameraSytle(CameraStyle newStyle)
	{
		combatCam.SetActive(false);
		thirdPersonCam.SetActive(false);
		topDownCam.SetActive(false);

		if(newStyle == CameraStyle.Basic)
		{
			thirdPersonCam.SetActive(true);
		}
		if(newStyle == CameraStyle.Combat)
		{
			combatCam.SetActive(true);
		}
		if (newStyle == CameraStyle.Topdown)
		{
			topDownCam.SetActive(true);
		}

		currentStyle = newStyle;
	}
}
