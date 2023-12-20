using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //public static PlayerMovement Instance;

    public float dashRange;
    public float dashSpeed;
    public bool isDash;
    public float speed;
    public Vector2 direction;
    private Animator animator;
    public enum FaceDir {DOWN, UP, LEFT, RIGHT}
    public FaceDir faceDir;
    public bool isAttack;
    public GameObject trail;
    public int face;

    public float fadeDuration = 0.5f;

    //public float dashDelay;
    //private float count;

    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(transform.gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    void Start(){
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        TakeInput();
        Move();
    }

    private void Move(){
        if (isDash == false)
        {
            transform.Translate(direction * speed * Time.deltaTime);
            SetAnimatorMovement(direction);
        }
        else
        {
            transform.Translate(direction * dashSpeed * Time.deltaTime);
            SetAnimatorMovement(direction);
        }
    }

    private void TakeInput(){
        direction = Vector2.zero;

        if(Input.GetKey(KeyCode.UpArrow))
        {
            direction += Vector2.up;
            faceDir = FaceDir.UP;
            face = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction += Vector2.left;
            faceDir = FaceDir.LEFT;
            face = 2;
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            direction += Vector2.down;
            faceDir = FaceDir.DOWN;
            face = 3;
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            direction += Vector2.right;
            faceDir = FaceDir.RIGHT;
            face = 4;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isDash = true;
            trail.SetActive(true);
        }
        else
        {
            isDash = false;
            StartCoroutine(DeactivateAfterDelay(trail));
        }

        if (Input.GetKey(KeyCode.RightShift))
        {
            animator.SetBool("Run", true);
        }


        //if( Input.GetKeyDown(KeyCode.Space)&&count<=0) 
        //{ 
        //    Vector2 targetPos = transform.position;
        //    if (faceDir == FaceDir.UP) {
        //        targetPos.y += dashRange;
        //    }
        //    else if (faceDir == FaceDir.DOWN)
        //    {
        //        targetPos.y -= dashRange;
        //    }
        //    else if (faceDir == FaceDir.RIGHT) { 
        //        targetPos.x += dashRange;
        //    }
        //    else if (faceDir == FaceDir.LEFT)
        //    {
        //        targetPos.x -= dashRange;
        //    }
        //    transform.position= targetPos;
        //    count = dashDelay;
        //}
        //else
        //{
        //    count -= Time.deltaTime;
        //}
    }

    private void SetAnimatorMovement(Vector2 direction){
        if(direction != Vector2.zero){
            animator.SetBool("Run", true);
            animator.SetFloat("yDir", direction.y);
            if (direction.x <= 0.1f && direction.x > -0.1f) return;
            animator.SetFloat("xDir", direction.x);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    IEnumerator DeactivateAfterDelay(GameObject trail)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            if (isDash == false)
            {
                elapsedTime += Time.deltaTime;
            } else
            {
                elapsedTime = 0;
            }
            yield return null;
        }
        trail.SetActive(false);
    }

}
