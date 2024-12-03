using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackKursinis
{
    public interface ITurnStrategy
    {
        void ExecuteTurn(Participant participant, Deck deck, InputOutput io, ref double playerBet);
    }
}
