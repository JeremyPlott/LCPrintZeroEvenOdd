using System;
using System.Threading;


//first time testing out semaphore use
//pretty handy

public class ZeroEvenOdd
{
    private int n;

    public ZeroEvenOdd(int n)
    {
        this.n = n;
    }

    private Semaphore zeroSem = new Semaphore(1, 1);
    private Semaphore evenSem = new Semaphore(0, 1);
    private Semaphore oddSem  = new Semaphore(0, 1);

    // printNumber(x) outputs "x", where x is an integer.
    public void Zero(Action<int> printNumber)
    {

        for (int i = 1; i <= n; i++)
        {

            zeroSem.WaitOne();

            printNumber(0);

            if (i % 2 == 0)
            {
                evenSem.Release();
            }
            else
            {
                oddSem.Release();
            }
        }
    }

    public void Even(Action<int> printNumber)
    {

        for (int i = 2; i <= n; i += 2)
        {
            evenSem.WaitOne();
            printNumber(i);
            zeroSem.Release();
        }
    }

    public void Odd(Action<int> printNumber)
    {

        for (int i = 1; i <= n; i += 2)
        {
            oddSem.WaitOne();
            printNumber(i);
            zeroSem.Release();
        }
    }
}