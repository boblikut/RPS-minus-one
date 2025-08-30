namespace RPS.Classes
{
    public class ChatMessage
    {
        public Color Color { get; set; }
        public string Text { get; set; }
        public int x;
        public int y;
        public int alpha = 255;
        public ChatMessage(string Text, Color Color, int x, int y)
        {
            this.Color = Color;
            this.Text = Text;
            this.x = x;
            this.y = y;
        }
    }
}