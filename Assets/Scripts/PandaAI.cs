using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaAI : MonoBehaviour
{
    public int move_delay;	// ���� �̵������� ������ �ð�
    public int move_time;	// �̵� �ð�

    float speed = 0.15f;

    private Vector2 direction;

    bool isWandering;
    bool isWalking;

    SpriteRenderer sprite;

    // ���� �ִϸ��̼� ��� ���� �ڵ�� ��� �ּ� ó�� !!
    // Animator anim;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        // anim = GetComponent<Animator>();

        isWandering = false;
        isWalking = false;
    }

    void FixedUpdate()
    {
        if (!isWandering)
            StartCoroutine(Wander());	// �ڷ�ƾ ����

        
        if (isWalking)
        {
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
        
    }
  
    void MoveDirection()
    {
        float rand_x, rand_y;
        rand_x = Random.Range(-1.0f, 1.0f);
        rand_y = Random.Range(-1.0f, 1.0f);
        Debug.Log(rand_x);
        Debug.Log(rand_y);

        while (rand_x == 0 && rand_y == 0)
        {
            rand_x = Random.Range(-1.0f, 1.0f);
            rand_y = Random.Range(-1.0f, 1.0f);
        }

        direction = new Vector2(rand_x, rand_y).normalized;
        Debug.Log(direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Get the normal vector of the surface the object collided with
        Vector2 surfaceNormal = collision.contacts[0].normal;

        // Calculate the reflected direction vector
        direction = Vector2.Reflect(direction, surfaceNormal);

    }

    IEnumerator Wander()
    {
        move_delay = 4;
        move_time = 10;

        MoveDirection(); // �����̴� ���� ���� ����

        isWandering = true;

        yield return new WaitForSeconds(move_delay);

        isWalking = true;
        // anim.SetBool("isWalk", true);	// �̵� �ִϸ��̼� ����

        yield return new WaitForSeconds(move_time);

        isWalking = false;
        // anim.SetBool("isWalk", false);	// �̵� �ִϸ��̼� ����

        isWandering = false;
    }

}