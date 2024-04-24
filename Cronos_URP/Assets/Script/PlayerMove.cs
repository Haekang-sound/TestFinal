using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	// �ʿ��� ����
	public float speed;
	float hAxis;
	float vAxis;
	bool walking;
	bool wRun;
	Transform PlayerCAMRoot;    // ī�޶��Ʈ�� ���� ? 
	Transform Player;   // ī�޶��Ʈ�� ���� ? 
	Animator m_animator;        // �ִϸ����������� �����ͼ� �ݿ�����

	Vector3 moveVec;
	Animator anim;

	float dTime;

//     /// ����Ʈ�� ���� �׽�Ʈ�� �ڵ�
//     void OnSlashEvent()
//     {
// 		EffectManager effectManager = GameObject.Find("EffectManager").GetComponent<EffectManager>();
// 		effectManager.PlayerSlash();
//     }

    void Start()
	{
		anim = GetComponentInChildren<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		dTime = Time.deltaTime;
		hAxis = Input.GetAxisRaw("Horizontal"); // �̵������� �����´�.
		vAxis = Input.GetAxisRaw("Vertical");
		
		// �̵���ư�� ���ȴٸ�
		if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
		{
			anim.SetBool("isWalking", true);
			walking = true;
		}
		else
		{
			anim.SetBool("isWalking", false);
			walking = false;
		}

		// ������
		if (Input.GetButtonDown("Jump") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Dodge"))
		{
			anim.SetTrigger("Dodge");
		}
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Dodge"))
		{
			transform.position += transform.forward.normalized * speed * 1.5f * dTime;
		}

		// ���ݸ��
		if (Input.GetButtonDown("Punch") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
		{
			anim.SetTrigger("Punch");
		}

		if (Input.GetButtonDown("Hook") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Hook"))
		{
			anim.SetTrigger("Hook");
		}

		// �޸��� ��ư�� ���ȴٸ�
		if (Input.GetButton("Run"))
		{
			anim.SetBool("isRunning", true);
			wRun = true;
		}
		else
		{
			anim.SetBool("isRunning", false);
			wRun =false;
		}

		// �̵����̶�� 
		if (walking) 
		{
			Debug.Log("������");
			transform.position += transform.forward.normalized * (wRun == true ? speed * 3f : speed) * dTime; // ����
		}
	}
}

