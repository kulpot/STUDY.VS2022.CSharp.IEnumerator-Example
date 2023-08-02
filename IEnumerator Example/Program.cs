using System;
using System.Collections;
//using System.Collections.Generic;

//ref link:https://www.youtube.com/watch?v=wZLunlEoR9k&list=PLRwVmtr-pp07QlmssL4igw1rnrttJXerL&index=14
//ctrl+shift+space --- check target details 
// list -- are dynamic, can grow and shrink
// list -- manage array underneath
// all link function rely on IEnumerator

class MeList<T>
{
    T[] items = new T[5];
    int count;
    public void Add(T item)
    {
        if (count == items.Length)
            Array.Resize(ref items, items.Length * 2);  // resize the underlying containers --- add slots by x2 of previous slot
        items[count++] = item;
    }

    public IEnumerator<T> GetEnumerator()
    {
        //for (int i = 0; i < count; i++)
        //    yield return items[i];      // requires yield return knowledge
        return new MeEnumerator(this);
    }
    class MeEnumerator : IEnumerator<T>
    {
        int index = -1;
        MeList<T> theList;
        public MeEnumerator(MeList<T> theList)
        {
            this.theList = theList;
        }
        public bool MoveNext()
        {
            //index++;
            return ++index < theList.count;
        }

        public T Current
        {
            get
            {
                if (index < 0 || theList.count <= index)
                    return default(T);
                return theList.items[index];
            }
        }

        public void Dispose() { }

        object System.Collections.IEnumerator.Current   // none generic current // requires knowledge in interface
        {
            get { return Current; }
        }

        public void Reset()
        {
            //throw new NotSupportedException();  // built-in yield statement
            index = -1; // force reset to sentenel value
        }
    }
}

static class MainClass
{
    static void Main()
    {
        //int[] ints = new int[] { 25, 34, 32 };
        //ArrayList myPartyAges = new ArrayList();    // none generic -- the good old days
        //myPartyAges.Add(25);
        //myPartyAges.Add(34);
        //myPartyAges.Add("Billy");
        //myPartyAges.Add(32);
        //myPartyAges.Add(99);
        //List<int> myPartyAges = new List<int>();   //generic list
        MeList<int> myPartyAges = new MeList<int>();
        myPartyAges.Add(25);
        myPartyAges.Add(34);
        myPartyAges.Add(32);
        //IEnumerator<int> rator = myPartyAges.GetEnumerator();
        //rator.MoveNext();
        //rator.Reset();
        //IEnumerator rator = myPartyAges.GetEnumerator();    // none generic IEnumerator
        //rator.MoveNext();
        //rator.MoveNext();
        //rator.MoveNext();
        //rator.MoveNext();   // error --- finish arraylist content
        //Console.WriteLine(rator.Current);
        foreach (int i in myPartyAges)
            Console.WriteLine(i);
        //IEnumerator<int> rator = myPartyAges2.GetEnumerator();  //IEnumerator(sentinel value) -- allow you to trace another sequence
        //IEnumerator<int> billy = myPartyAges2.GetEnumerator();
        //IEnumerator<int> johnny = myPartyAges2.GetEnumerator();
        //Console.WriteLine(billy.Current); // invalid current call(will defualt to 0) -- when called befored MoveNext
        //billy.MoveNext();
        //johnny.MoveNext();
        //billy.MoveNext();
        //billy.MoveNext();

        //Console.WriteLine(billy.Current);
        //Console.WriteLine(johnny.Current);

        //while (rator.MoveNext())
        //    Console.WriteLine(rator.Current);
    }
}