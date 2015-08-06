using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace shooter
{
    public partial class shooter : Form
    {
        public shooter()
        {
            InitializeComponent();
            t = 0;
        }

        const int time = 10;
        int n; 
        int _w, _h;
        int k;
        double t;
        Obj[] objects;
        string fileName = "kolvo_sbitih.txt";                //пишем полный путь к файлу
        Random rand = new Random();   

      
        Graphics g;  

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Interval = 60;
            timer1.Start();
            timer2.Interval = 300;
            timer2.Start(); 
      
         }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (t < time)
            {
                int i;
                
                for (i = 0; i < n; i++)
                    objects[i].Step(_w, _h);
                for (i = 0; i < n - 1; i++)
                    for (int j = i + 1; j < n; j++)
                        objects[i].Clash(ref objects[j]);
                for (i = 0; i < n; i++)
                    objects[i].MoveNext();
                Refresh();
                
            }
            else
            {
                timer2.Stop();
                timer1.Stop();
                
            }
            t += (timer1.Interval * 1f) / 1000f;
               
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int i;
            for (i = 0; i < n; i++)
                objects[i].Draw(e.Graphics);           
            
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Cross;
        }

       

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int m_x = e.X;
            int m_y = e.Y;
            if(t<time)
            for(int i=0;i<n;i++){
                k += objects[i].Shooted(m_x, m_y);
            }

            textBox1.Text = Convert.ToString(k);
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.White;
            _w = pictureBox1.Width;
            _h = pictureBox1.Height;
            g = pictureBox1.CreateGraphics();
            k = 0;
            textBox1.Text = Convert.ToString(k);
            
            
            
            int sluch_chisl_o1, sluch_chisl_o2, sluch_chisl_o3;
            sluch_chisl_o1 = rand.Next(2, 5);
            sluch_chisl_o2 = rand.Next(2, 4);
            sluch_chisl_o3 = rand.Next(2, 6);
            n = sluch_chisl_o1 + sluch_chisl_o2 + sluch_chisl_o3;

            
            int a1, a2; //a1,a2 - интервал, откуда будет браться случайное число
            int dl_intervala = _w / n;
            a1 = 0; a2 = a1 + dl_intervala;
            int[] a = new int[n];
            for (int i = 0; i < n; i++)
            {
                a[i] = rand.Next(Math.Min(a1, Math.Abs(a2 - 30)), Math.Max(a1, Math.Abs(a2 - 30)));
                a1 = a2;
                a2 += dl_intervala;
            }

            dl_intervala = _h / n;
            a1 = 0; a2 = a1 + dl_intervala;
            int[] b = new int[n];
            for (int i = 0; i < n; i++)
            {
                b[i] = rand.Next(Math.Min(a1, Math.Abs(a2 - 30)), Math.Max(a1, Math.Abs(a2 - 30)));
                a1 = a2;
                a2 += dl_intervala;
            }

            
            objects = new Obj[n];
            


            double v1,v2;
            
            for (int i = 0; i < sluch_chisl_o1; i++)
            {
                v1 = rand.Next(-11, 12);
                v2 = rand.Next(-9, 11);
                objects[i] = new Obj_square(a[i], b[rand.Next(0, n - 1)], v1, v2, Color.BlueViolet, 30);                
                
            }
            for (int i = sluch_chisl_o1 - 1; i < sluch_chisl_o1+sluch_chisl_o2; i++)
            {
                v1 = rand.Next(-8, 7);
                v2 = rand.Next(-9, 9);
                objects[i] = new Obj_circle(a[i], b[rand.Next(0, n - 1)], v1, v2, Color.Green, 20);
            }
            for (int i = sluch_chisl_o1+sluch_chisl_o2-1; i < n; i++)
            {
                v1 = rand.Next(-6, 12);
                v2 = rand.Next(-9, 11);
                objects[i] = new Obj_star(a[i], b[rand.Next(0, n - 1)], v1, v2, Color.Orange, 30);

            }
               
           
        }      

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            
            for (int i = 0; i < n; i++)
                objects[i].Erase(g);
            if (!String.ReferenceEquals(textBox3.Text, ""))
            {
                if (File.Exists(fileName) != true)
                {  //проверяем есть ли такой файл, если его нет, то создаем
                    using (StreamWriter sw = new StreamWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write)))
                    {
                        sw.Write(textBox3.Text);
                        sw.Write(' ');
                        sw.WriteLine(k);
                    }
                }
                else
                {                              //если файл есть, то откырваем его и пишем в него 
                    using (StreamWriter sw = new StreamWriter(new FileStream(fileName, FileMode.Open, FileAccess.Write)))
                    {
                        (sw.BaseStream).Seek(0, SeekOrigin.End);         //идем в конец файла и пишем строку или пишем то, что хотим
                        sw.Write(textBox3.Text);
                        sw.Write(' ');
                        sw.WriteLine(k);
                    }
                }
            }
            else MessageBox.Show("ВВедите НИК!!!");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            textBox2.Text = Convert.ToString(time-Convert.ToInt32(t));
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            k = 0;
            textBox1.Text = Convert.ToString(k);

            t = time + 1;
            Random rand1 = new Random();
            int sluch_chisl_o1, sluch_chisl_o2, sluch_chisl_o3;
            sluch_chisl_o1 = rand1.Next(2, 5);
            sluch_chisl_o2 = rand1.Next(2, 4);
            sluch_chisl_o3 = rand1.Next(2, 6);
            n = sluch_chisl_o1 + sluch_chisl_o2 + sluch_chisl_o3;

            int a1, a2; //a1,a2 - интервал, откуда будет браться случайное число
            int dl_intervala = _w / n;
            a1 = 0; a2 = a1 + dl_intervala;
            int[] a = new int[n];
            for (int i = 0; i < n; i++)
            {
                a[i] = rand1.Next(Math.Min(a1, Math.Abs(a2 - 30)), Math.Max(a1, Math.Abs(a2 - 30)));
                a1 = a2;
                a2 += dl_intervala;
            }

            dl_intervala = _h / n;
            a1 = 0; a2 = a1 + dl_intervala;
            int[] b = new int[n];
            for (int i = 0; i < n; i++)
            {
                b[i] = rand1.Next(Math.Min(a1, Math.Abs(a2 - 30)), Math.Max(a1, Math.Abs(a2 - 30)));
                a1 = a2;
                a2 += dl_intervala;
            }

            MessageBox.Show(Convert.ToString(n));
            objects = new Obj[n];



            double v1, v2;
            int i2;

            for (int i = 0; i < sluch_chisl_o1; i++)
            {
                v1 = rand1.Next(-11, 12);
                v2 = rand1.Next(-9, 11);
                i2 = rand1.Next(0, n - 1);
                objects[i] = new Obj_square(a[i], b[i2], v1, v2, Color.BlueViolet, 30);

            }
            for (int i = sluch_chisl_o1 - 1; i < sluch_chisl_o1 + sluch_chisl_o2; i++)
            {
                v1 = rand1.Next(-8, 7);
                v2 = rand1.Next(-9, 9);
                i2 = rand1.Next(0, n - 1);
                objects[i] = new Obj_circle(a[i], b[i2], v1, v2, Color.Green, 20);
            }
            for (int i = sluch_chisl_o1 + sluch_chisl_o2 - 1; i < n; i++)
            {
                v1 = rand1.Next(-6, 12);
                v2 = rand1.Next(-9, 11);
                i2 = rand1.Next(0, n - 1);
                objects[i] = new Obj_star(a[i], b[i2], v1, v2, Color.Orange, 30);

            }

            textBox3.Text = "";
            t = 0;
            timer1.Interval = 60;
            timer1.Start();
            timer2.Interval = 300;
            timer2.Start(); 

        }
                         
    }
}