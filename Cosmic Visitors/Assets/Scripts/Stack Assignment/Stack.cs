
using System;
using Unity.VisualScripting;

/// <summary>
/// Maurix stack class
/// </summary>
/// <typeparam name="T"></typeparam>
public class Stack<T>
{
    private T[] items;
    private int count;
    public int Count{ get { return count; } }

    /// <summary>
    /// Maurix Stack Class
    /// </summary>
    public Stack() 
    {
        count = 0;
        items = new T[0];
    }


    public void Push(T item)
    {
        Array.Resize(ref items, count+1);
        items[count] = item;
        count++;
    }

    public T Pop()
    {
        if(count > 0)
        {
            T item = items[count - 1];
            Array.Resize(ref items, count - 1);
            count--;
            return item;
        }
        else throw new ArgumentException("Array Count must be greater than 0");
    }
    
    public T Peek()
    {
        if (count > 0)
        {
            return items[count - 1];
        }
        else throw new ArgumentException("Array Count must be greater than 0");
    }
}
