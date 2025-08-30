namespace RPS.Classes
{
    public class Card
    {
        public delegate void CardAction(Player user);

        public string Name { get; set; }
        public string Description { get; set; }
        public CardAction action;
        public Image Image { get; set; }
        public Card(string Name, string Description, string image, CardAction action)
        {
            this.Name = Name;
            this.Description = Description;
            this.action = action;
            this.Image = Image.FromFile(image);
        }
        public Card(string Name, string Description, CardAction action) // УБРАТЬ УБРАТЬ УБРАТЬ УБРАТЬ УБРАТЬ УБРАТЬ УБРАТЬ УБРАТЬ
        {
            this.Name = Name;
            this.Description = Description;
            this.action = action;
        }
        public void use(Player user)
        {
            Global.CurrentGame.chat.SendMessage(user.Name + " использовал карту \"" + this.Name + "\"", Color.Gold);
            Global.CurrentGame.Game.Invalidate();
            action(user);
            user.cards.Remove(this);
        }
    }
}