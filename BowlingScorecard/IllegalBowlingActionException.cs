using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingGame
{
    /// <summary>
    /// An exception to resemble a unique Bowling Game exception.
    /// </summary>
    public class IllegalBowlingActionException : ApplicationException
    {
        public IllegalBowlingActionException(string messgage): base(messgage)
        { }
    }
}
