namespace RPS.Classes
{
    public class Chat
    {
        private Thread chatThread;
        private volatile bool isActive = true;
        public List<ChatMessage> messages = new List<ChatMessage>();
        public Chat() {
            chatThread = new Thread(() =>
            {
                while (isActive)
                {
                    Thread.Sleep(25);
                    if (messages.Count == 0)
                        continue;

                    for (int i = 0; i < messages.Count; i++)
                    {
                        if (i == 0 || messages[i - 1].y < 50)
                        {
                            messages[i].y -= 2;
                            messages[i].alpha = Math.Max(messages[i].alpha - 4, 0);
                            if (messages[i].y <= -20)
                            {
                                messages.RemoveAt(i);
                                i--;
                            }
                        }
                    }

                    if (Global.CurrentGame.Game.InvokeRequired && Global.CurrentGame.Game != null)
                        try {
                            Global.CurrentGame.Game.Invoke(new Action(() =>
                            {
                                Global.CurrentGame.Game.Invalidate(new Rectangle(400, 0, 700, 110), false);
                            }));
                        } catch { }
                        
                }
            });
            chatThread.IsBackground = true;
            chatThread.Start();
        }
        public void SendMessage(string Text, Color Color)
        {
            Size textSize = TextRenderer.MeasureText(Text, Global.CurrentGame.messageFont);
            messages.Add(new ChatMessage(Text, Color, 766 - textSize.Width/2 + 25, 75));
        }
        public void deleteChat()
        {
            isActive = false;
        }

    }
}