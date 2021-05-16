using static ReverseTicTacToeGame.Enums;

namespace ReverseTicTacToeGame
{

    public struct Player
    ///TODO
    /// Meybe struct
    {
        private int m_NumberOfWins;
        private readonly eSignsOfPlayers r_Sign;
        private readonly bool m_IsComputer;
        // internal string m_Name:
    


        public Player(eSignsOfPlayers i_Sign, bool i_IsComputer)
        {
            r_Sign = i_Sign;
            m_IsComputer = i_IsComputer;
            m_NumberOfWins = 0;
        }

        public int NumberOfWins
        {
            get { return m_NumberOfWins; }
            set { m_NumberOfWins = value;  }
        }

        public eSignsOfPlayers Sign
        {
            get { return r_Sign; }
        }

        public bool IsComputer
        {
            get { return m_IsComputer; }
        }
    }
}