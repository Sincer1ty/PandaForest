using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaAI : MonoBehaviour
{
    public int move_delay;	// ���� �̵������� ������ �ð�
    public int move_time;	// �̵� �ð�

    float speed_x;	// x�� ���� �̵� �ӵ�
    float speed_y;	// y�� ���� �̵� �ӵ�
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
            Move();
    }

    void Move()
    {
        if (speed_x != 0)
        {
            sprite.flipX = speed_x < 0; // x�� �ӵ��� ���� Spite �̹����� ������
        }
            

        transform.Translate(speed_x, speed_y, speed_y);	// ���� �̵�
    }

    IEnumerator Wander()
    {
        // ���� ���� : ����� ��ȭ�� ������ �����ϱ� ���� �����̴� ª�� ������ ���
        move_delay = 1;
        move_time = 6;

        // Translate�� �̵��� �� Object�� �ڷ���Ʈ �ϴ� ���� �����ϱ� ���� Time.deltaTime�� ������
        speed_x = Random.Range(-0.8f, 0.8f) * Time.deltaTime;
        speed_y = Random.Range(-0.8f, 0.8f) * Time.deltaTime;

        isWandering = true;

        yield return new WaitForSeconds(move_delay);

        isWalking = true;
        // anim.SetBool("isWalk", true);	// �̵� �ִϸ��̼� ����

        yield return new WaitForSeconds(move_time);

        isWalking = false;
        // anim.SetBool("isWalk", false);	// �̵� �ִϸ��̼� ����

        isWandering = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Bottom_CollisionTiles") || collision.gameObject.name.Contains("Top_CollisionTiles"))
            speed_y = -speed_y;
        else if (collision.gameObject.name.Contains("Left_CollisionTiles") || collision.gameObject.name.Contains("Right_CollisionTiles"))
            speed_x = -speed_x;
    }
}
