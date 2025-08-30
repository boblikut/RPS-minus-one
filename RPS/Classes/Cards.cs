using yt_DesignUI;
namespace RPS.Classes
{
    public class Cards
    {
        public static List<Card> cards = new List<Card>
        {
            new Card("+1", "Добавляет 1 хлопушечный заряд в барабан", "Images/cards/1.png", new Card.CardAction((Player user) =>
            {
                Global.CurrentGame.wheel.addShot(shots.FULL);
            })),
            new Card("+2", "Добавляет 2 хлопушечных заряда в барабан", "Images/cards/2.png", new Card.CardAction((Player user) =>
            {
                Global.CurrentGame.wheel.addShot(shots.FULL);
                Global.CurrentGame.wheel.addShot(shots.FULL);
            })),
            new Card("Джокер", "Добавляет 1 пустой хлопушечный заряд в барабан", "Images/cards/joker.png", new Card.CardAction((Player user) =>
            {
                Global.CurrentGame.wheel.addShot(shots.EMPTY);
            })),
            new Card("Джок x2", "Добавляет 2 пустых хлопушечных заряда в барабан", "Images/cards/joker_x2.png", new Card.CardAction((Player user) =>
            {
                Global.CurrentGame.wheel.addShot(shots.EMPTY);
                Global.CurrentGame.wheel.addShot(shots.EMPTY);
            })),
            new Card("Реверс", "Заменяет пустые заряды на заполненные, а заполненные на пустые", "Images/cards/reverse.png", new Card.CardAction((Player user) =>
            {

                for(int i = 0; i < Global.CurrentGame.wheel.drum.Length; i++)
                {
                    if (Global.CurrentGame.wheel.drum[i] == shots.EMPTY)
                    {
                        Global.CurrentGame.wheel.drum[i] = shots.FULL;
                    }
                    else if (Global.CurrentGame.wheel.drum[i] == shots.FULL){
                        Global.CurrentGame.wheel.drum[i] = shots.EMPTY;
                    }
                }
            })),
            new Card("Зеро", "Возвращает начальное состояние барабана. 1 зараженный заряд", "Images/cards/zero.png", new Card.CardAction((Player user) =>
            {
                Global.CurrentGame.wheel.drum[0] = shots.FULL;
                for(int i = 1; i < Global.CurrentGame.wheel.drum.Length; i++)
                {
                     Global.CurrentGame.wheel.drum[i] = shots.NO;
                }
            })),
            new Card("Друг", "Вы и соперник получаете по карте", "Images/cards/heart.png", new Card.CardAction((Player user) =>
            {
                Card gettingCard = cards[Global.rand.Next(cards.Count)];
                Global.CurrentGame.player1.getCard(gettingCard);
                if (user.slidingPanel != null) {
                    user.slidingPanel.Controls.Add(new EgoldsCard(gettingCard, user));
                }
                gettingCard = cards[Global.rand.Next(cards.Count)];
                Global.CurrentGame.player2.getCard(gettingCard);
                if (user.Enemy.slidingPanel != null) {
                    user.Enemy.slidingPanel.Controls.Add(new EgoldsCard(gettingCard, user.Enemy));
                }
            })),
            new Card("Жулик", "Позваляет украсть случайную карту врага", "Images/cards/thief.png", new Card.CardAction((Player user) =>
            {
                if (user.Enemy.cards.Count < 1)
                {
                    Global.CurrentGame.chat.SendMessage("Не получилось украсть карту. У соперника нет карт!", Color.Gold);
                    return;
                }
                int stealingCard = Global.rand.Next(user.Enemy.cards.Count);
                user.cards.Add(user.Enemy.cards[stealingCard]);
                if (user.slidingPanel != null)
                {
                    user.slidingPanel.Controls.Add(new EgoldsCard(user.Enemy.cards[stealingCard], user));
                }
                user.Enemy.cards.RemoveAt(stealingCard);
                if (user.Enemy.slidingPanel != null)
                {
                    user.Enemy.slidingPanel.Controls.RemoveAt(stealingCard);
                }
                Global.CurrentGame.chat.SendMessage(user.Name + " украл карту", Color.Gold);
            })),
            new Card("Барыга", "Позволяет поменятся руками с соперником. У вас забирается одна карта", "Images/cards/salesman.png", new Card.CardAction((Player user) =>
            {
                if (user.cards.Count < 2)
                {
                    Global.CurrentGame.chat.SendMessage("У вас нет карты, чтобы забрать", Color.Gold);
                    return;
                }
                if (user.Enemy.RightHand == signs.EMPTY)
                {
                    Global.CurrentGame.chat.SendMessage("Враг не выбрал символы", Color.Gold);
                    return;
                }
                (signs, signs) bufSigns = (user.LeftHand, user.RightHand);
                user.LeftHand = user.Enemy.LeftHand;
                user.RightHand = user.Enemy.RightHand;
                user.Enemy.LeftHand = bufSigns.Item1;
                user.Enemy.RightHand = bufSigns.Item2;
                int removingCard = Global.rand.Next(user.Enemy.cards.Count);
                user.cards.RemoveAt(removingCard);
                if (user.slidingPanel != null) {
                    user.slidingPanel.Controls.RemoveAt(removingCard);
                }
            })),
            new Card("Назад", "Меняет текущий выбор рук на предыдущий", "Images/cards/back.png", new Card.CardAction((Player user) =>
            {
                if (user.PreviousHands == (signs.EMPTY, signs.EMPTY))
                {
                    Global.CurrentGame.chat.SendMessage("Вы не выбирали руки до этого!", Color.Gold);
                    return;
                }
                if (user.LeftHand == signs.EMPTY)
                    Global.CurrentGame.MoveHand(Global.CurrentGame.player1 == user ? 0 : 2, dirs.FORWARD);
                user.LeftHand = user.PreviousHands.Item1;
                if (user.RightHand == signs.EMPTY)
                    Global.CurrentGame.MoveHand(Global.CurrentGame.player1 == user ? 1 : 3, dirs.FORWARD);
                user.RightHand = user.PreviousHands.Item2;
            })),
            new Card("Ангел", "Защищает от любого урона 1 раз", "Images/cards/angel.png", new Card.CardAction((Player user) =>
            {
                user.ActiveCard = "Ангел";
            })),
            new Card("Рекошет", "Урон игрока получает соперник 1 раз", "Images/cards/reco.png",  new Card.CardAction((Player user) =>
            {
                user.ActiveCard = "Рекошет";
            })),
            new Card("Отмена", "Убирает активную карту соперника", "Images/cards/cancel.png",  new Card.CardAction((Player user) =>
            {
                user.Enemy.ActiveCard = "";
            })),
        };
    }
}