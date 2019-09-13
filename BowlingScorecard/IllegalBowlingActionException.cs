using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingGame
{
    public class IllegalBowlingActionException : ApplicationException
    {
        public IllegalBowlingActionException(string messgage): base(messgage)
        { }
    }
}
