using static ReverseTicTacToeGame.Enums;

namespace ReverseTicTacToeGame
{
    internal struct Player
    {
        private int m_NumberOfWins;
        private readonly eSignsOfPlayers r_Sign;
        private readonly bool r_IsComputer;

        public Player(eSignsOfPlayers i_Sign, bool i_IsComputer)
        {
            r_Sign = i_Sign;
            r_IsComputer = i_IsComputer;
            m_NumberOfWins = 0;
        }

        public int NumberOfWins
        {
            get { return m_NumberOfWins; }
            set { m_NumberOfWins = value; }
        }

        public eSignsOfPlayers Sign
        {
            get { return r_Sign; }
        }

        public bool IsComputer
        {
            get { return r_IsComputer; }
        }
    }
}