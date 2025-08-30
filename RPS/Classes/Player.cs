using RPS.Classes;

public class Player{
    public string Name { get; set; }
    public signs LeftHand { get; set; }
    public signs RightHand { get; set; }
    public (signs, signs) PreviousHands = (signs.EMPTY, signs.EMPTY);
    public bool canChoose = false;
    public hands? droppingHand;
    public signs lastSign = signs.EMPTY;
    public signs playingSign;

    public int HP;

    public Player Enemy;

    public bool canNavigate = false;
    public int markedCard = -1;

    public string ActiveCard;
    public List<Card> cards = new List<Card>();
    public Control slidingPanel;
    public void clearHands()
    {
        this.LeftHand = signs.EMPTY;
        this.RightHand = signs.EMPTY;
        this.playingSign = signs.EMPTY;
        this.droppingHand = null;
    }

    public Player(string name, int hp = 2, Control slidingPanel = null)
    {
        this.Name = name;
        this.HP = hp;
        this.canChoose = false;
        this.clearHands();
        this.slidingPanel = slidingPanel;
    }

    public void dropHand()
    {
        if (droppingHand == null) { return; }
        switch (droppingHand)
        {
            case hands.LEFT:
                {
                    lastSign = LeftHand;
                    LeftHand = signs.EMPTY;
                    playingSign = RightHand;
                    break;
                }
            case hands.RIGHT:
                {
                    lastSign = RightHand;
                    RightHand = signs.EMPTY;
                    playingSign = LeftHand;
                    break;
                }
        }
    }

    public void takeDamage(int amount)
    {
        HP = Math.Max(HP - 1, 0);
    }

    public void getCard(Card card)
    {
        cards.Add(card);
        Global.CurrentGame.chat.SendMessage(this.Name + " получил карту \"" + card.Name + "\"", Color.Gold);
    }
    
    public void useCard(Card card)
    {
        card.use(this);
    }

    public void onGetDamage(shots type)
    {
        if (type == shots.NO)
            return;
        if (ActiveCard == "Ангел")
        {
            Global.CurrentGame.chat.SendMessage("Сработала карта \"Ангел\"", Color.Gold);
            ActiveCard = "";
            return;
        } else if (ActiveCard == "Рекошет")
        {
            Global.CurrentGame.chat.SendMessage("Сработала карта \"Рекошет\"", Color.Gold);
            Enemy.onGetDamage(type);
            ActiveCard = "";
            return;
        }
        switch (type)
        {
            case shots.FULL:
                {
                    Global.CurrentGame.chat.SendMessage(this.Name + " получил урон", Color.Red);
                    takeDamage(1);
                    break;
                }
            case shots.EMPTY:
                {
                    Global.CurrentGame.chat.SendMessage(this.Name + " получил фальшивый урон", Color.Green);
                    break;
                }
        }
    }
}