using System;
using System.Threading;

namespace TicTacToe
{
    class Program
    {
        static void Main()
        {            
            State state = new State();            
            
            bool gameOver = false;
            while(!gameOver)
            {
                Action action = State.NextMove(state);                
                if (action == null) break;

                Console.WriteLine(action);
                state = state.ApplyAction(action);
                
                state.Print();
                Thread.Sleep(1000);
                
            }          
            
            
            
        }
    }
}

