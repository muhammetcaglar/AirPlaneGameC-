using PlaneGame.Properties;
using System.Collections;
using System.ComponentModel;

namespace PlaneGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private bool isLeftArrowPressed = false;
        private bool isRightArrowPressed = false;
        PictureBox ucakSavar = new PictureBox();

        ArrayList mermiList = new ArrayList();
        ArrayList dusmanUcak = new ArrayList();
        void UcakSavarHareket(KeyEventArgs e)
        {
            int ucakSavarX = ucakSavar.Location.X;
            int ucakSavarY = ucakSavar.Location.Y;
            if (e.KeyCode == Keys.Right)
            {
                ucakSavarX += 5;
                isRightArrowPressed = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                ucakSavarX -= 5;
                isLeftArrowPressed = true;

            }
            ucakSavar.Location = new Point(ucakSavarX, ucakSavarY);
            this.Controls.Add(ucakSavar);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ucakSavar.Image = Resources.ucaksavar;
            this.Controls.Add(ucakSavar);
            ucakSavar.Location = new Point(300, 500);
            timer1.Enabled = true;
        }
    
        private void timer1_Tick(object sender, EventArgs e)
        {

            
            dusmanUcak.Add(UcakUret());
            foreach (PictureBox item1 in dusmanUcak)
            {
                UcakHareket(item1);
            }
            
        }


        private void UcakHareket(PictureBox ucak)
        {
            int x = ucak.Location.X;
            int y = ucak.Location.Y;
            y += 5;
            ucak.Location = new Point(x, y);
        }


        PictureBox UcakUret()
        {
            PictureBox ucak = new PictureBox();
            ucak.Image = Resources.ucak;
            Random rnd = new Random();
            int ucakBaslax = rnd.Next(1000);
            ucak.Location = new Point(ucakBaslax, 0);

            timer3.Enabled = true;
            this.Controls.Add(ucak);
            return ucak;
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            UcakSavarHareket(e);
            if (e.KeyCode == Keys.Space)
            {
                mermiList.Add(MermiUret());
                timer2.Enabled = true;
            }
        }

        private object MermiUret()
        {
            PictureBox mermi = new PictureBox();
            mermi.Image = Resources.mermi2;
            mermi.SizeMode = PictureBoxSizeMode.StretchImage;
            mermi.Location = new Point(ucakSavar.Location.X, ucakSavar.Location.Y);
            this.Controls.Add((mermi));
            return mermi;
        }



        private void timer2_Tick(object sender, EventArgs e)
        {
            foreach (PictureBox mermi in mermiList)
            {
                MermiHareket(mermi);
            }
        }

        private void MermiHareket(PictureBox mermi)
        {
            mermi.Image = Resources.mermi2;
            int x = mermi.Location.X;
            int y = mermi.Location.Y;
            y = y - 30;
            mermi.Location = new Point(x, y);
            this.Controls.Add(mermi);

        }
        int sayi = 0;
        PictureBox gidenUcaklar = new PictureBox();
        PictureBox gidenMermiler = new PictureBox();
        private void timer3_Tick(object sender, EventArgs e)
        {
            List<PictureBox> mermiToRemove = new List<PictureBox>();
            List<PictureBox> ucakToRemove = new List<PictureBox>();

            foreach (PictureBox item in mermiList)
            {
                foreach (PictureBox item1 in dusmanUcak)
                {
                    if (item.Bounds.IntersectsWith(item1.Bounds))
                    {
                        mermiToRemove.Add(item);
                        ucakToRemove.Add(item1);
                        sayi++;
                        label1.Text = sayi.ToString();
                    }
                }
            }

            // Remove the items after the loop to avoid modifying the collection during iteration
            foreach (PictureBox item in mermiToRemove)
            {
                this.Controls.Remove(item);
                mermiList.Remove(item);
            }

            foreach (PictureBox item1 in ucakToRemove)
            {
                this.Controls.Remove(item1);
                dusmanUcak.Remove(item1);
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                isLeftArrowPressed = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                isRightArrowPressed = false;
            }
        }
    }
}