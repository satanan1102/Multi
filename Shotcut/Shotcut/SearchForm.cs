using AForge.Imaging;
using AForge.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shotcut
{
    public partial class SearchForm : Form
    {
        int[][] Allhis;
        int[] distance;
        int[] his;
        Dictionary<int,int>  sortdis = new Dictionary<int,int>();
        public SearchForm()
        {
            InitializeComponent();
        }

        private void Choose_botton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileVideo = new OpenFileDialog();
            openFileVideo.Filter = "Video files|*.mp4|All Files|*.*";
            if (openFileVideo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = openFileVideo.FileName;
                

            }
        }

        private void Search_button1_Click(object sender, EventArgs e)
        {
           keyFrame();
            GetAll();
            distanceSearch();

        }

        private void distanceSearch()
        {
            int[] dis;
            distance = new int[Allhis.Length];
            for (int i = 0; i < Allhis.Length; i++)
            {
                dis = Allhis[i];
                for (int j = 0; j < his.Length; j++)
                {
                    distance[i] = distance[i] + Math.Abs(dis[j] - his[j]);
                }
                sortdis.Add(i, distance[i]);
            }

            var list = sortdis.Values.ToList();
            list.Sort();

            for (int j = 0; j < list.Count; j++)
            {
                var myValue = sortdis.FirstOrDefault(x => x.Value == list[j]).Key;
                Console.WriteLine(list[j]+" : "+myValue);

                string constr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();

                SqlCommand cmd = new SqlCommand("select KeyFrame from CollectionShot where No = '"+ myValue + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        pictureBox1.ImageLocation = reader[0].ToString();
                    }
                }
                reader.Close();
                con.Close();



            }
        }

        private void GetAll()
        {
            string constr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            int count = 0;
            SqlCommand cmd = new SqlCommand("select count(*) from CollectionShot", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    count = int.Parse(reader[0].ToString());
                }
            }
            reader.Close();

            Allhis = new int[count][];
            SqlCommand cmd1 = new SqlCommand("select HistrogrameVecter from CollectionShot", con);
            SqlDataReader reader1 = cmd1.ExecuteReader();
            count = 0;
            if (reader1.HasRows)
            {

                while (reader1.Read())
                {
                    Allhis[count] = Cut_histogram(reader1[0].ToString());
                }
            }
            reader1.Close();
            con.Close();
        }

        private int[] Cut_histogram(string str)
        {
            string[] res = str.Split(' ');
            int[] his = new int[res.Length];

            for (int i = 0; i < his.Length; i++)
            {
                his[i] = int.Parse (res[i].ToString());
            }

            return his;

        }
   
        private void keyFrame()
        {
            VideoFileReader reader = new VideoFileReader();
            reader.Open(textBox1.Text);
            int Fcon = int.Parse(reader.FrameCount.ToString());
            int Fcount = Fcon / 2;
            for (int i = 0; i < reader.FrameCount; i++)
            {
                Bitmap videoFrame = reader.ReadVideoFrame();
                if (i == Fcount)
                {
                    hisVec(videoFrame);
                    break;
                }

            }


            reader.Close();

        }
        private void hisVec(Bitmap videoFrame)
        {
            ImageStatistics rgbStatistics = new ImageStatistics(videoFrame);
            int[] redValues = rgbStatistics.Red.Values;
            int[] greenValues = rgbStatistics.Green.Values;
            int[] blueValues = rgbStatistics.Blue.Values;

             his = new int[256];
            for (int j = 0; j < 256; j++)
            {
                his[j] = (redValues[j] + greenValues[j] + blueValues[j]) / 3;

            }

        }
    }
}
