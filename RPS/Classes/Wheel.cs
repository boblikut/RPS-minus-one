namespace RPS.Classes
{
    public class Wheel
    {
        public readonly shots[] drum;
        public void addShot(shots type)
        {
            for (int i = 0; i < 6; i++)
            {
                if (drum[i] == shots.NO)
                {
                    drum[i] = type;
                    break;
                }
            }
        }
        public Wheel()
        {
            drum = new shots[6];
            for (int i = 0; i < 6; i++)
            {
                drum[i] = shots.NO;
            }
            addShot(shots.FULL);
        }
    }
}