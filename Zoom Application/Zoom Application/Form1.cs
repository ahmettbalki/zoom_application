using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zoom_Application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Panel içinde mouse kontrolü için mouse eventi atadım.
            panel1.MouseWheel += new System.Windows.Forms.MouseEventHandler(Panel1_MouseWheel);
            //Form başlarken ekranı ortalar.
            this.StartPosition = FormStartPosition.CenterScreen;

            float x = (Convert.ToInt32(this.Width) - Convert.ToInt32(panel1.Width)) / 2;
            float y = (Convert.ToInt32(this.Height) - Convert.ToInt32(panel1.Height)) / 2;

            panel1.Location = new Point(x: Convert.ToInt32(x), y: Convert.ToInt32(y));

        }
        //Formun içinde scroll değişeceği için ilk tanım tutulur.
        private Size ImageSize = new Size(0, 0);
        Image img = null;

        // Kaydırma ile resmi büyütmek ve küçültmek sağlanır.
        private void Panel1_MouseWheel(Object sender, MouseEventArgs a)
        {
            
            if (pictureBox1.Image != null)
            {
                // Fare tekerliği ileri yönde hareket ettirilirse yakınlaştırma işlemi yapılır.
                if (a.Delta > 0)
                {
                    
                    if ((pictureBox1.Width < (5 * panel1.Width)) && (pictureBox1.Height < (5 * panel1.Height)))
                    {
                        // Picturebox'ın boyutu değiştirilir.
                        pictureBox1.Width = (int)(pictureBox1.Width * 1.10);
                        pictureBox1.Height = (int)(pictureBox1.Height * 1.10);
                    }
                }
                else
                {
                    //  Fare tekerliği geri yönde hareket ettirilirse uzaklaştırma işlemi yapılır.
                    if ((pictureBox1.Width > (panel1.Width / 5)) && (pictureBox1.Height > (panel1.Height / 5)))
                    {
                        // Picturebox'ın boyutu değiştirilir.
                        pictureBox1.Width = (int)(pictureBox1.Width / 1.10);
                        pictureBox1.Height = (int)(pictureBox1.Height / 1.10);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Picturebox'ı yeniler.
            Clear();
            //Bilgisayardan bir resim dosyası seçilir ve picturebox'a aktarılır.
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            file.ShowDialog();
            if (!string.IsNullOrWhiteSpace(file.FileName))
            {
                img = Image.FromFile(file.FileName);
                pictureBox1.Image = img;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }
        //Picturebox ve paneli temizlemek için uygulanır.
        private void Clear()
        {
            pictureBox1.Image = null;
            pictureBox1.Size = ImageSize;
            pictureBox1.Refresh();
            panel1.Refresh();
        }
        //Panelin arkaplan rengini ayarlar. 
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackColor = Color.White;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            ImageSize = pictureBox1.Size;
        }
        
    }
}
