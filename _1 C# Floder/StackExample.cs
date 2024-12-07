using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class StackNode<T>
{
    public T data;
    public StackNode<T> prev;
}

public class StackCustom<T> where T : new()
{
    public StackNode<T> top;

    public void Push(T data)
    {
        // var =  public StackNode<T>
        var stackNode = new StackNode<T>();
        
        stackNode.data = data;

        stackNode.prev = top;
        top = stackNode;
    }

    public T Pop()
    {
        if (top == null)
        {
            return new T();
        }
        
        var result = top.data;   
        top = top.prev;

        return result;
    }

    // 단지 구경만 하는 함수
    public T Peek()
    {
        if (top == null)
        {
            return new T();
        }
        
        return top.data;
    }
}

public class StackExample : MonoBehaviour
{
    // head
    // []* -> []* ->[]* ->[]* ->( 싱글 링크드 표시방법)
    //                    top
    // [] <- *[]<- *[]<- *[]( 스택 표기법)
    
    // Start is called before the first frame update
  
    // public 일지라도 인스펙터에 노출은 안되나
    // c# class 에서는 접근이 가능하다
    [NonSerialized]
    public float Speed = 3.0f;
    
    // private 이지만 인스펙터에 노출이 된다.
    // 다만 다른 c# class에서는 접근이 불가하다
    [SerializeField]
    private float Speed2 = 3.0f;
    
    private Stack<Vector3> positionstack = new Stack<Vector3>();
    
    // Update is called once per frame
    void Update()
    {
        // 위치를 (0, 0, 0) 으로 지정 *현재 바라보고있는 방향*
        Vector3 movePos = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movePos += transform.forward;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            movePos -= transform.forward;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            movePos -= transform.right;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            movePos += transform.right;
        }

        // 움직였던 정보를 기록하기 위해 키를 땔때마다 위치를 기록
        if (Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.D)) 
        {
            movePos = Vector3.zero;
            positionstack.Push(transform.position);
        }
         
        // 왓던 포지션으로 되돌아가는 코드
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (positionstack.Count > 0)
            transform.position = positionstack.Pop();
        }
        
        transform.position += movePos.normalized * Speed2 * Time.deltaTime;
    }

    public class BreaketChecker
    {
        public bool AreBreaketBalanced(string expression)
        {
            Stack<char> stack = new Stack<char>();

            foreach (char c in expression)
            {
                if (c == '('  || c == '[' || c == '{')
                {
                    stack.Push(c);
                }
                else if (c == ')' || c == ']' || c == '}')
                {
                    if (stack.Count == 0)
                        return false;
                    
                    char top = stack.Pop();
                    if ((c == ')' && top == '(') ||
                        (c == ']' && top == '[') ||
                        (c == '}' && top == '{'))
                    {
                        return false;
                    }
                }
            }

            return stack.Count == 0;
        }
    }
}
