using RPS.Classes;

namespace RPS
{
    public partial class Info : Form
    {
        public Info()
        {
            InitializeComponent();
        }

        int page = 0;
        string[] Texts =
        {
            "   Для перехода в меню из любого окна\n" + 
                "восполльзуйтесь клавишей Esc",
            "   С началом игры начинается классический отсчёт\n" +
                "\"Камень, ножницы, бумага\". Чтобы выбрать\n" +
                "руки нажмите одну из трёх цифр(для второго\n" +
                "игрока на NumPad(не забудьте отключить NumLock)).\n" +
                "1 - Камень, 2 - Ножницы, 3 - Бумага\n" +
                "   После фразы \"МИНУС ОДИН!!!\" уберите одну\n" +
                "руку одной из двух цифр\n" +
                "1 - Верхняя рука, 2 - Нижняя рука",
            "   После первого выбора рук вы можете воспользоваться\n" +
                "бонусными картами. Для этого нажмите на кнопку \"КАРТЫ\".\n" + 
                "   Чтобы прочитать описание карты навидитесь на неё\n" +
                "мышкой.\n" +
                "   Чтобы использовать карту нажмите на неё\n" +
                "левой кнопкой мыши\n" +
                "   Чтобы перейти к убиранию рук просто нажмите\n" + 
                "на кнопку \"ПРОПУСТИТЬ\"",
            "   При поражении из одного игрока начинает крутится\n" +
                "барабан. Если хлопушечный заряд выпадает под\n" + 
                "стрелкой, то проигравший игрок соответствующий получает\n" +
                "урон(настоящий или фальшивый). Но от этого урона можно\n" +
                "защитится, если у игрока есть одна из активных карт.\n" +
                "   Если вы выбирите активную карту, когда у вас уже есть\n" +
                "активная карта, то вы просто замените старую карту на новую",
            "   Все действия во время игры отображаются в чате\n" +
                "сверху по центру экрана." +
                "   Там вы можете увидеть: информацию о использованных\n" +
                "картах, отсчёты до выбора рук, победы/поражения игроков\n" +
                "и так далее"
        };

        private void Info_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            BackgroundImage = Image.FromFile("Images/floor2.jpg");
            label3.Text = "1/" + Texts.Length;
            label1.Text = Texts[page];
        }

        private void Info_KeyDown(object sender, KeyEventArgs e)
        {
            Global.Menu.Show();
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            page = Math.Min(page + 1, Texts.Length - 1);
            label3.Text = (page + 1) + "/" + Texts.Length;
            label1.Text = Texts[page];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            page = Math.Max(page - 1, 0);
            label3.Text = (page + 1) + "/" + Texts.Length;
            label1.Text = Texts[page];
        }
    }
}