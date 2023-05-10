using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    public Stack<int> stack = new Stack<int>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"stack test: {stack}");
        Debug.Log($"stack Test - count: {stack.Count}");

        stack.Push(11);
        stack.Push(22);
        stack.Push(33);
        stack.Push(44);

        Debug.Log($"stack Test - after 4 Push count: {stack.Count}");

        int i = stack.Pop();

        Debug.Log($"stack Test - Afetr one Pop... ");
        Debug.Log($"stack Test - count: {stack.Count}");
        Debug.Log($"stack Test - Element Popped: {i}");

        Debug.Log($"stack Test - Stack Peek...");
        Debug.Log($"stack Test - Element in peek: {stack.Peek()}");
        Debug.Log($"stack Test - count: {stack.Count}");


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
