using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaAI : MonoBehaviour
{
    public int move_delay;	// 다음 이동까지의 딜레이 시간
    public int move_time;	// 이동 시간

    float speed = 0.2f;

    private Vector2 direction;

    bool isWandering;
    bool isWalking;

    SpriteRenderer sprite;

    // 아직 애니메이션 없어서 관련 코드는 모두 주석 처리 !!
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
            StartCoroutine(Wander());	// 코루틴 실행

        
        if (isWalking)
        {
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
        
    }
  
    // 움직이는 방향 결정
    void MoveDirection()
    {
        float rand_x, rand_y;
        rand_x = Random.Range(-1.0f, 1.0f);
        rand_y = Random.Range(-1.0f, 1.0f);
        Debug.Log(rand_x);
        Debug.Log(rand_y);

        // 움직이는 방향이 (0,0)이면 움직이지 않으니 제외
        // 추후 움직이지 않고 서있는 애니메이션 넣을지...?
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

        MoveDirection(); // 움직이는 랜덤 방향 설정

        isWandering = true;

        yield return new WaitForSeconds(move_delay);

        isWalking = true;
        // anim.SetBool("isWalk", true);	// 이동 애니메이션 실행

        yield return new WaitForSeconds(move_time);

        isWalking = false;
        // anim.SetBool("isWalk", false);	// 이동 애니메이션 종료

        isWandering = false;
    }

}