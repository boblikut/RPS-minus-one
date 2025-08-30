using RPS.Classes;
using System.Drawing.Drawing2D;

//Third party

namespace yt_DesignUI
{
    public class EgoldsCard : Control
    {
        Animation animCurtain;
        private float CurtainHeight;
        private int CurtainMinHeight = 20;

        private bool MouseEntered = false;
        private bool MousePressed = false;

        StringFormat SF = new StringFormat();

        public string TextHeader { get; set; } = "Header";
        public Font FontHeader { get; set; } = new Font("Verdana", 12F, FontStyle.Bold);
        public Color ForeColorHeader { get; set; } = Color.White;

        public string TextDescrition { get; set; } = "Your description text for this control";
        public Font FontDescrition { get; set; } = new Font("Verdana", 8.25F, FontStyle.Regular);
        public Color ForeColorDescrition { get; set; } = Color.White;

        public Color BackColorCurtain { get; set; } = Color.RoyalBlue;
        public Image innerImage { get; set; } = Image.FromFile("Images/card.png");

        private Card card;
        private Player owner;

        public EgoldsCard(Card card, Player owner)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            TextHeader = card.Name;
            TextDescrition = card.Description;
            this.card = card;
            this.owner = owner;
            if (card.Image != null)
                innerImage = card.Image;

            Size = new Size(125, 200);
            Margin = new Padding(30, 20, 0, 0);
            CurtainHeight = Height - 60;

            Font = new Font("Verdana", 9F, FontStyle.Regular);
            BackColor = Color.RosyBrown;

            animCurtain = new Animation();
            animCurtain.Value = CurtainMinHeight;

            SF.Alignment = StringAlignment.Near;
            SF.LineAlignment = StringAlignment.Near;

            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.HighQuality;

            graph.Clear(Parent.BackColor);

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            Rectangle rectCurtain = new Rectangle(0, Height - (int)animCurtain.Value, Width - 1, (int)animCurtain.Value);
            Rectangle rectDescription = new Rectangle(5, rect.Height - rectCurtain.Height + 10, rect.Width - 22, rect.Height);
            Rectangle rectImage = new Rectangle((int)(rect.Width * 0.15), 45, (int)(rect.Width * 0.7), rect.Height - CurtainMinHeight - 60);

            graph.FillRectangle(new SolidBrush(BackColor), rect);
            graph.DrawImage(innerImage, rectImage);

            graph.DrawRectangle(new Pen(BackColorCurtain), rectCurtain);
            graph.FillRectangle(new SolidBrush(BackColorCurtain), rectCurtain);

            graph.DrawRectangle(new Pen(Color.Gray), rect);

            if (animCurtain.Value == CurtainHeight)
            {
                graph.DrawString(TextDescrition, FontDescrition, new SolidBrush(ForeColorDescrition), rectDescription, SF);
            }
            if (!MouseEntered)
            {
                graph.DrawString(Text, Font, new SolidBrush(ForeColor), 15, Height - 37);
            }
            graph.DrawString(TextHeader, FontHeader, new SolidBrush(ForeColorHeader),
                new Rectangle(15, 15, rect.Width, 30));
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (Height <= 100)
                Height = 100;
            if (Width <= 100)
                Width = 100;

            CurtainHeight = Height - 60;

            animCurtain = new Animation();
            animCurtain.Value = CurtainMinHeight;

            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            MouseEntered = true;

            DoCurtainAnimation();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            MouseEntered = false;

            DoCurtainAnimation();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            MousePressed = true;

            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            MousePressed = false;

            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            owner.useCard(card);

            this.Dispose();
        }

        private void DoCurtainAnimation()
        {
            if (!MouseEntered)
            {
                animCurtain = new Animation("Curtain_" + Handle, Invalidate, animCurtain.Value, CurtainMinHeight);
            }
            else
            {
                animCurtain = new Animation("Curtain_" + Handle, Invalidate, animCurtain.Value, CurtainHeight);
            }

            Animator.Request(animCurtain, true);
        }
    }
}
