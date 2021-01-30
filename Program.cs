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
            Console.WriteLine("Who would you like to play as? X/O");
            string player = Console.ReadLine();
            while(!gameOver)
            {
                state.Print();
                string turn = state.GetPlayer();              
                
                
                if (turn != player)
                {
                    Thread.Sleep(250);
                    Action action = State.NextMove(state);
                    if (action == null) break;
                    state = state.ApplyAction(action);
                    
                }
                else
                {
                    string input = Console.ReadLine();
                    state = state.ApplyAction(new Action(int.Parse(input), state.GetPlayer()));
                }
                
                               
                
            }          
            
            
            
        }
    }
}

