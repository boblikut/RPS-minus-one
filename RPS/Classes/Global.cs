namespace RPS.Classes{
    public class GameForm : Form
    {
        public Player player1;
        public Player player2;
        public GameForm Game;
        public Chat chat = new Chat();
        public Wheel wheel;
        public Panel slidingPanel;
        public Font messageFont = new Font("Comic Sans MS", 14);
        public virtual void MoveHand(int id, dirs dir)
        {

        }
    }
    public class Global
    {
        static public Random rand = new Random();
        static public GameForm CurrentGame;
        static public Form Menu;
        static public Icon Icon = new Icon("Images/logo.ico");
    }
}