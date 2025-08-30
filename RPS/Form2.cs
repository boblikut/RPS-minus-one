using RPS.Classes;
using System.Drawing.Drawing2D;
using yt_DesignUI;

namespace RPS
{
    public partial class Form2 : GameForm
    {
        int ticks = 0;

        private HashSet<(signs, signs)> winnersTbl = new HashSet<(signs, signs)>()
        {
            {(signs.ROCK, signs.SCISSORS)},
            {(signs.SCISSORS, signs.PAPER)},
            {(signs.PAPER, signs.ROCK)}
        };

        //Game functions
        private int drumPos = 0;
        private void spinDrum()
        {
            int spinTimes = Global.rand.Next(12, 24);
            drumPos = (drumPos + spinTimes % 6) % 6;
            spinResult = wheel.drum[(6 - drumPos) % 6];
            int n = spinTimes * 20;
            int mod = n / 20;
            int delay = 10;
            spining_timer.Interval = mod * 550 + 1000; // (25 * ((2 * delay) + 25 - 1) / 2)
                                                       //Не имею ни малейшего поняти почему с 25 всё работает идеально. Я для оптимизации решил считать это
                                                       //через форулу арифметической прогрессии. Так, что здесь вместо 25 по идее должно быть 20, но с ним
                                                       //конец раунда наступает слишком рано. Решил пересчитать по формуле, где мы знаем последний элемент прогрессии
                                                       //но тогда получается 400, что тоже слишком мало. Не знаю как так вышло, что 550 оказалось идеальным множителем
                                                       //для 5% выполнения кручения. Вот, так как - то повезло
            spining_timer.Start();
            Thread spiningThread = new Thread(() =>
            {
                for (int i = 0; i < n; i++)
                {
                    if (isClosed)
                    {
                        break;
                    }
                    if (i % mod == 0)
                    {
                        delay += 1;
                    }
                    if (wheelAngle + 3 < 360)
                    {
                        wheelAngle += 3;
                    }
                    else
                    {
                        wheelAngle = 0;
                    }
                    pictureBox1.Invalidate(false);
                    Thread.Sleep(delay);
                }
            });
            spiningThread.IsBackground = true;
            spiningThread.Start();
        }
        private void UpdateHands()
        {
            for (int i = 0; i < 4; i++)
            {
                handsPictures[i].Invalidate(false);
            }
        }
        private int[] maxForward = { -10, -10, 1043, 1043 };
        private int[] maxBack = { -510, -510, 1543, 1543 };
        public override void MoveHand(int id, dirs dir)
        {
            int pos = dir == dirs.FORWARD ? maxForward[id] : maxBack[id];

            handsAnimations[id] = new Animation("Hand_" + id, new Action(() => { InvalidateHands(id); }), handsAnimations[id].Value, pos);
            Animator.Request(handsAnimations[id]);
        }

        private void StartGame()
        {
            player1 = new Player("Игрок 1", 2, slidingPanel1);
            player2 = new Player("Игрок 2", 2, slidingPanel2);
            player1.Enemy = player2;
            player2.Enemy = player1;
            wheel = new Wheel();
            isFirstStage = true;
            UpdateHands();
            ticks = 0;
            StartPrepairing();
        }
        private void StartRound()
        {
            if (player1.playingSign != signs.EMPTY)
                player1.lastSign = player1.playingSign;
            if (player2.playingSign != signs.EMPTY)
                player2.lastSign = player2.playingSign;
            player1.clearHands();
            player2.clearHands();
            for (int i = 0; i < 4; i++)
            {
                MoveHand(i, dirs.BACK);
            }
            isFirstStage = true;
            UpdateHands();
            ticks = 0;
            counter.Start();
            Card bufCard = Cards.cards[Global.rand.Next(Cards.cards.Count)];
            player1.getCard(bufCard);
            slidingPanel1.Controls.Add(new EgoldsCard(bufCard, player1));
            bufCard = Cards.cards[Global.rand.Next(Cards.cards.Count)];
            player2.getCard(bufCard);
            slidingPanel2.Controls.Add(new EgoldsCard(bufCard, player2));
            chat.SendMessage("Раунд начался!", Color.White);
            GC.Collect(0); //с каждым раундом копится разный хлам и в принципе не помешает почистить память немного
        }
        private void StartPrepairing()
        {
            chat.SendMessage("Преготовьтесь к новому раунду...", Color.White);
            preparing_timer.Start();
        }
        private void looseRound(Player player)
        {
            if (player == player1)
            {
                chat.SendMessage(player2.Name + " выйграл раунд!", Color.Red);
            }
            else if (player == player2)
            {
                chat.SendMessage(player1.Name + " выйграл раунд!", Color.Green);
            }
            spinDrum();
            loosedPlayer = player;
        }
        private void FinishGame()
        {
            if (loosedPlayer == player1)
            {
                chat.SendMessage(player2.Name + " выйграл игру!", Color.Green);
            }
            else if (loosedPlayer == player2)
            {
                chat.SendMessage(player1.Name + " выйграл игру!", Color.Green);
            }
        }

        //Form functions
        public Form2()
        {
            InitializeComponent();
            Game = this;
        }

        private static Image _rockImage = Image.FromFile("Images/rock.png");
        private static Image _scissorsImage = Image.FromFile("Images/scissors.png");
        private static Image _paperImage = Image.FromFile("Images/paper.png");
        private PictureBox[] handsPictures = new PictureBox[4];
        private Animation[] handsAnimations = new Animation[4];

        private static FlowLayoutPanel slidingPanel1;
        private static FlowLayoutPanel slidingPanel2;
        private static Animation slideAnimation1;
        private static Animation slideAnimation2;
        private void Form2_Load(object sender, EventArgs e)
        {
            Icon = Global.Icon;
            Global.CurrentGame = Game;
            chat = new Chat();
            BackgroundImage = Image.FromFile("Images/floor2.jpg");
            DoubleBuffered = true;
            slidingPanel1 = new FlowLayoutPanel
            {
                Height = 500,
                Width = 512,
                BackColor = Color.Red,
                Location = new Point(200, Height),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoScroll = true,
            };
            Controls.Add(slidingPanel1);
            slidingPanel1.BringToFront();
            slideAnimation1 = new Animation();
            slideAnimation1.Value = slidingPanel1.Location.Y;

            slidingPanel2 = new FlowLayoutPanel
            {
                Height = 500,
                Width = 512,
                BackColor = Color.Red,
                Location = new Point(821, Height),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoScroll = true,
            };
            Controls.Add(slidingPanel2);
            slidingPanel2.BringToFront();
            slideAnimation2 = new Animation();
            slideAnimation2.Value = slidingPanel2.Location.Y;


            handsPictures[0] = new PictureBox
            {
                Height = 225,
                Width = 500,
                Location = new Point(-510, 90),
                BackColor = Color.Transparent
            };

            handsPictures[1] = new PictureBox
            {
                Height = 225,
                Width = 500,
                Location = new Point(-510, 400),
                BackColor = Color.Transparent
            };

            handsPictures[2] = new PictureBox
            {
                Height = 225,
                Width = 500,
                Location = new Point(1543, 90),
                BackColor = Color.Transparent
            };

            handsPictures[3] = new PictureBox
            {
                Height = 225,
                Width = 500,
                Location = new Point(1543, 400),
                BackColor = Color.Transparent
            };

            handsPictures[0].Paint += handPicture1_Paint;
            handsPictures[1].Paint += handPicture2_Paint;
            handsPictures[2].Paint += handPicture3_Paint;
            handsPictures[3].Paint += handPicture4_Paint;

            for (int i = 0; i < 4; i++)
            {
                handsAnimations[i] = new Animation();
                handsAnimations[i].Value = handsPictures[i].Location.X;
                Controls.Add(handsPictures[i]);
            }

            StartGame();
        }

        private bool isFirstStage;

        private void Start1Stage()
        {
            player1.canChoose = true;
            player2.canChoose = true;

            stage_timer.Start();
            counter.Stop();
        }
        private void Start2Stage()
        {

            if (isSlidingAtUp1)
            {
                player1.canChoose = false;
                slideAnimation1 = new Animation("Card1_" + Handle, InvalidateCardPanel1, slideAnimation1.Value, Height);
                isSlidingAtUp1 = false;
                Animator.Request(slideAnimation1);
            }
            if (isSlidingAtUp2)
            {
                player2.canChoose = false;
                slideAnimation2 = new Animation("Card2_" + Handle, InvalidateCardPanel2, slideAnimation2.Value, Height);
                isSlidingAtUp2 = false;
                Animator.Request(slideAnimation2);
            }
            

            player1.canChoose = true;
            player2.canChoose = true;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            chat.SendMessage("МИНУС ОДИН!!!", Color.White);
            stage_timer.Start();
            counter.Stop();
        }

        //Timers
        private static string[] ticks_names = {
            "КАМЕНЬ!",
            "НОЖНИЦЫ!!",
            "БУМАГА!!!"
        };
        private void counter_Tick(object sender, EventArgs e)
        {
            ticks++;

            if (isFirstStage)
            {
                chat.SendMessage(ticks_names[ticks - 1], Color.White);
                if (ticks >= 3)
                {
                    Start1Stage();
                }
            }
            else
            {
                if (ticks >= 40)
                {
                    Start2Stage();
                }
            }
        }

        private shots spinResult;
        private Player loosedPlayer;
        private void stage_timer_Tick(object sender, EventArgs e)
        {
            player1.canChoose = false;
            ticks = 0;

            //First stage
            if (isFirstStage)
            {
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                isFirstStage = false;
                counter.Start();
                if (player1.LeftHand != signs.EMPTY)
                    MoveHand(0, dirs.FORWARD);
                if (player1.RightHand != signs.EMPTY)
                    MoveHand(1, dirs.FORWARD);
                if (player2.LeftHand != signs.EMPTY)
                    MoveHand(2, dirs.FORWARD);
                if (player2.RightHand != signs.EMPTY)
                    MoveHand(3, dirs.FORWARD);
            }
            //Second stage
            else
            {
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;

                player1.PreviousHands = (player1.LeftHand, player1.RightHand);
                player2.PreviousHands = (player2.LeftHand, player2.RightHand);

                if (player1.RightHand == signs.EMPTY)
                {
                    chat.SendMessage(player1.Name + " не выбрал ни одну руку...", Color.Red);
                    if(player2.RightHand != signs.EMPTY)
                        MoveHand(player2.droppingHand == hands.LEFT ? 2 : 3, dirs.BACK);
                    looseRound(player1);
                    goto Skip;
                }
                if (player2.RightHand == signs.EMPTY)
                {
                    chat.SendMessage(player2.Name + " не выбрал ни одну руку...", Color.Red);
                    if (player1.RightHand != signs.EMPTY)
                        MoveHand(player1.droppingHand == hands.LEFT ? 0 : 1, dirs.BACK);
                    looseRound(player2);
                    goto Skip;
                }
                
                player1.dropHand();
                if (player1.playingSign != signs.EMPTY)
                    MoveHand(player1.droppingHand == hands.LEFT ? 0 : 1, dirs.BACK);
                player2.dropHand();
                if (player2.playingSign != signs.EMPTY)
                    MoveHand(player2.droppingHand == hands.LEFT ? 2 : 3, dirs.BACK);

                if (player1.playingSign == signs.EMPTY)
                {
                    chat.SendMessage(player1.Name + " не опустил ни одну руку...", Color.Red);
                    looseRound(player1);
                }
                else if (player2.playingSign == signs.EMPTY)
                {
                    chat.SendMessage(player2.Name + " не опустил ни одну руку...", Color.Red);
                    looseRound(player1);
                }
                else if (player1.playingSign == player2.playingSign)
                {
                    chat.SendMessage("Ничья...", Color.Green);
                    Thread.Sleep(1000);
                    StartPrepairing();
                }
                else if (winnersTbl.Contains((player1.playingSign, player2.playingSign)))
                {
                    looseRound(player2);
                }
                else
                {
                    looseRound(player1);
                }
            }

        Skip:
            UpdateHands();
            stage_timer.Stop();
        }

        private void spining_timer_Tick(object sender, EventArgs e)
        {
            loosedPlayer.onGetDamage(spinResult);
            Invalidate();
            if (loosedPlayer.HP <= 0)
                FinishGame();
            else
                StartPrepairing();
            spining_timer.Stop();
        }

        private void preparing_timer_Tick(object sender, EventArgs e)
        {
            StartRound();
            preparing_timer.Stop();
        }

        //Keys
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            //player1
            if (isFirstStage && player1.canChoose && (int)e.KeyCode > 48 && (int)e.KeyCode < 52)
            {
                if (player1.LeftHand == 0)
                {
                    player1.LeftHand = (signs)e.KeyCode - 48;
                }
                else if (player1.LeftHand != (signs)e.KeyCode - 48)
                {
                    player1.RightHand = (signs)e.KeyCode - 48;
                }
            }
            if (!isFirstStage && player1.canChoose && (int)e.KeyCode > 48 && (int)e.KeyCode < 51)
            {
                player1.droppingHand = (hands)e.KeyCode - 48;
            }
            //player2
            if (isFirstStage && player2.canChoose && (int)e.KeyCode > 96 && (int)e.KeyCode < 100)
            {
                if (player2.LeftHand == 0)
                {
                    player2.LeftHand = (signs)e.KeyCode - 96;
                }
                else if (player2.LeftHand != (signs)e.KeyCode - 96)
                {
                    player2.RightHand = (signs)e.KeyCode - 96;
                }
            }
            if (!isFirstStage && player2.canChoose && (int)e.KeyCode > 96 && (int)e.KeyCode < 99)
            {
                player2.droppingHand = (hands)e.KeyCode - 96;
            }
            //EXIT
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        //Cards
        private Animation slideAnimation;
        private bool isSlidingAtUp1 = false;
        private bool isSlidingAtUp2 = false;
        private FlowLayoutPanel flowLayoutPanel;

        private void InvalidateCardPanel1()
        {
            slidingPanel1.Invoke(new Action(() =>
            {
                slidingPanel1.Location = new Point(slidingPanel1.Location.X, (int)slideAnimation1.Value);
            }));
        }
        private void InvalidateCardPanel2()
        {
            slidingPanel2.Invoke(new Action(() =>
            {
                slidingPanel2.Location = new Point(slidingPanel2.Location.X, (int)slideAnimation2.Value);
            }));
        }
        private void InvalidateHands(int id)
        {
            handsPictures[id].Invoke(new Action(() =>
            {
                handsPictures[id].Location = new Point((int)handsAnimations[id].Value, handsPictures[id].Location.Y);
            }));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (isSlidingAtUp1)
            {
                player1.canChoose = false;
                slideAnimation1 = new Animation("Card1_" + Handle, InvalidateCardPanel1, slideAnimation1.Value, Height);
                isSlidingAtUp1 = false;
            }
            else
            {
                player1.canChoose = true;
                slideAnimation1 = new Animation("Card1_" + Handle, InvalidateCardPanel1, slideAnimation1.Value, Height - slidingPanel1.Height - 100);
                isSlidingAtUp1 = true;
            }
            Animator.Request(slideAnimation1);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Start2Stage();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (isSlidingAtUp2)
            {
                player2.canChoose = false;
                slideAnimation2 = new Animation("Card2_" + Handle, InvalidateCardPanel2, slideAnimation2.Value, Height);
                isSlidingAtUp2 = false;
            }
            else
            {
                player2.canChoose = true;
                slideAnimation2 = new Animation("Card2_" + Handle, InvalidateCardPanel2, slideAnimation2.Value, Height - slidingPanel2.Height - 100);
                isSlidingAtUp2 = true;
            }
            Animator.Request(slideAnimation2);
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Image heart = Image.FromFile("Images/heart.png");

            for (int i = 0; i < player1.HP; i++)
            {
                e.Graphics.DrawImage(heart, 200 + i * 60, 20, 50, 50);
            }
            for (int i = 0; i < player2.HP; i++)
            {
                e.Graphics.DrawImage(heart, Width - (250 + i * 60), 20, 50, 50);
            }
        }

        //Painting
        private Point[] shots_xy = {
            new Point(82,28),
            new Point(131,55),
            new Point(131, 109),
            new Point(82,136),
            new Point(33,109),
            new Point(33,55)
        };

        private static Image _wheelImage = Image.FromFile("Images/wheel.png");
        private static Image _emptyImage = Image.FromFile("Images/shot_empty.png");
        private static Image _fullImage = Image.FromFile("Images/shot_full.png");


        private int wheelAngle = 0;

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            e.Graphics.TranslateTransform(102.5f, 102.5f);
            e.Graphics.RotateTransform(wheelAngle);
            e.Graphics.TranslateTransform(-102.5f, -102.5f);
            e.Graphics.DrawImage(_wheelImage, 10, 10, 185, 185);

            for (int i = 0; i < 6; i++)
            {
                Image image = wheel.drum[i] switch
                {
                    shots.EMPTY => _emptyImage,
                    shots.FULL => _fullImage,
                    _ => null,
                };
                if (image == null)
                    break;

                e.Graphics.DrawImage(image, shots_xy[i]);
            }
        }

        private static Image _heartImage = Image.FromFile("Images/heart.png");
        private static Image _angelImage = Image.FromFile("Images/cards/angel.png");
        private static Image _recoImage = Image.FromFile("Images/cards/reco.png");
        private void Form2_Paint_1(object sender, PaintEventArgs e)
        {
            foreach (ChatMessage message in chat.messages)
            {
                e.Graphics.DrawString(message.Text, messageFont, new SolidBrush(Color.FromArgb(message.alpha, message.Color)), message.x, message.y);
            }
            switch (player1.ActiveCard)
            {
                case "Ангел":
                    {
                        e.Graphics.DrawImage(_angelImage, 10, 20, 75, 100);
                        break;
                    }
                case "Рекошет":
                    {
                        e.Graphics.DrawImage(_recoImage, 10, 20, 75, 100);
                        break;
                    }
            }
            switch (player2.ActiveCard)
            {
                case "Ангел":
                    {
                        e.Graphics.DrawImage(_angelImage, 1438, 20, 75, 100);
                        break;
                    }
                case "Рекошет":
                    {
                        e.Graphics.DrawImage(_recoImage, 1438, 20, 75, 100);
                        break;
                    }
            }
            for (int i = 0; i < player1.HP; i++)
            {
                e.Graphics.DrawImage(_heartImage, 200 + i * 60, 20, 50, 50);
            }
            for (int i = 0; i < player2.HP; i++)
            {
                e.Graphics.DrawImage(_heartImage, Width - (250 + i * 60), 20, 50, 50);
            }
        }

        private Dictionary<signs, Image> handsTbl = new Dictionary<signs, Image>() {
            {signs.ROCK, _rockImage},
            {signs.SCISSORS, _scissorsImage},
            {signs.PAPER, _paperImage}
        };

        private void handPicture1_Paint(object sender, PaintEventArgs e)
        {
            Image img;
            if (player1.LeftHand == signs.EMPTY)
            {
                if (player1.lastSign == signs.EMPTY)
                    return;
                img = handsTbl[player1.lastSign];
            }
            else
            {
                img = handsTbl[player1.LeftHand];
            }
            e.Graphics.DrawImage(img, 0, 0, handsPictures[0].Width, handsPictures[0].Height);
        }
        private void handPicture2_Paint(object sender, PaintEventArgs e)
        {
            Image img;
            if (player1.RightHand == signs.EMPTY)
            {
                if (player1.lastSign == signs.EMPTY)
                    return;
                img = handsTbl[player1.lastSign];
            }
            else
            {
                img = handsTbl[player1.RightHand];
            }
            e.Graphics.DrawImage(img, 0, 0, handsPictures[1].Width, handsPictures[1].Height);
        }
        private void handPicture3_Paint(object sender, PaintEventArgs e)
        {
            Image img;
            if (player2.LeftHand == signs.EMPTY)
            {
                if (player2.lastSign == signs.EMPTY)
                    return;
                img = handsTbl[player2.lastSign];
            }
            else
            {
                img = handsTbl[player2.LeftHand];
            }
            Matrix mirrorMatrix = new Matrix();
            mirrorMatrix.Scale(-1, 1);
            mirrorMatrix.Translate(handsPictures[2].Width, 0, MatrixOrder.Append);

            e.Graphics.Transform = mirrorMatrix;

            e.Graphics.DrawImage(img, 0, 0, handsPictures[2].Width, handsPictures[2].Height);

            e.Graphics.ResetTransform();
            mirrorMatrix.Dispose();
        }
        private void handPicture4_Paint(object sender, PaintEventArgs e)
        {
            Image img;
            if (player2.RightHand == signs.EMPTY)
            {
                if (player2.lastSign == signs.EMPTY)
                    return;
                img = handsTbl[player2.lastSign];
            }
            else
            {
                img = handsTbl[player2.RightHand];
            }
            Matrix mirrorMatrix = new Matrix();
            mirrorMatrix.Scale(-1, 1);
            mirrorMatrix.Translate(handsPictures[3].Width, 0, MatrixOrder.Append);

            e.Graphics.Transform = mirrorMatrix;

            e.Graphics.DrawImage(img, 0, 0, handsPictures[3].Width, handsPictures[3].Height);

            e.Graphics.ResetTransform();
            mirrorMatrix.Dispose();
        }
        private bool isClosed = false;

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            isClosed = true;
            chat.deleteChat();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Global.Menu.Show();
            this.Dispose();
        }
    }
}
