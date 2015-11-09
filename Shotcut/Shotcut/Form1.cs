using AForge.Imaging;
using AForge.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Shotcut
{
    public partial class Form1 : Form
    {
        int[] Sdis;
        int[] Savei;
        String Url = "";
        String Url2 = "";
        string name;
        string keyF;
        string Vechist;

        int countCutFrame;

        string videoname;
        public Form1()
        {
            InitializeComponent();
        }

        private void Run_Click(object sender, EventArgs e)
        {
            VideoFileReader reader = new VideoFileReader();
            // open video file
            reader.Open(textBox1.Text);
            // check some of its attributes
            //Console.WriteLine("width:  " + reader.Width);
            //Console.WriteLine("height: " + reader.Height);
            //Console.WriteLine("fps:    " + reader.FrameRate);
            //Console.WriteLine("codec:  " + reader.CodecName);
            int[][] frameRGB = new int[reader.FrameCount][];
            // read 100 video frames out of it
            for (int i = 0; i < reader.FrameCount; i++)
            {
                //int disRed = 0;
                //int disGreen = 0;
                //int disBlue = 0;
                Bitmap videoFrame = reader.ReadVideoFrame();
                // process the frame somehow
                ImageStatistics rgbStatistics = new ImageStatistics(videoFrame);
                int[] redValues = rgbStatistics.Red.Values;
                int[] greenValues = rgbStatistics.Green.Values;
                int[] blueValues = rgbStatistics.Blue.Values;

                int[] his = new int[256];
                for (int j = 0; j < 256; j++)
                {
                    his[j] = (redValues[j] + greenValues[j] + blueValues[j]) / 3;

                }
                frameRGB[i] = his;



                //Bitmap videoFrame2 = reader.ReadVideoFrame();
                //// process the frame somehow
                //ImageStatistics rgbStatistics2 = new ImageStatistics(videoFrame2);
                //int[] redValues2 = rgbStatistics2.Red.Values;
                //int[] greenValues2 = rgbStatistics2.Green.Values;
                //int[] blueValues2 = rgbStatistics2.Blue.Values;

                //for (int j = 0; j < 256; j++)
                //{
                //    disRed = disRed + Math.Abs(redValues[j] - redValues2[j]);
                //    disGreen = disGreen + Math.Abs(greenValues[j] - greenValues2[j]);
                //    disBlue = disBlue + Math.Abs(blueValues[j] - blueValues2[j]);

                //}
                //int sumdis = disRed + disGreen + disBlue;
                //สร้างตัวแปรเก็บr+G+B เอาไว้หาค่าthredshold;กำหนดเอง
                // dispose the frame when it is no longer required
                videoFrame.Dispose();
            }

            reader.Close();
            Sdis = new int[frameRGB.Length - 1];
            for (int g = 0; g < frameRGB.Length - 1; g++)
            {
                int dis = 0;
                for (int k = 0; k < 256; k++)
                {

                    dis += Math.Abs(frameRGB[g][k] - frameRGB[g + 1][k]);
                }
                Sdis[g] = dis;
            }

            this.chart1.Titles.Add("Distance");
            Series series = this.chart1.Series.Add("Distance");
            for (int ss = 0; ss < frameRGB.Length - 1; ss++)
            {

                series.Points.AddXY(ss, Sdis[ss]);
            }


        }

        private void Search_button_Click(object sender, EventArgs e)
        {
            SearchForm search = new SearchForm();
            search.ShowDialog();
        }

        private void Choose_botton_Click(object sender, EventArgs e)
        {


            OpenFileDialog openFileVideo = new OpenFileDialog();
            openFileVideo.Filter = "Video files|*.mp4|All Files|*.*";
            if (openFileVideo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = openFileVideo.FileName;
                Url = openFileVideo.SafeFileName;

            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void Thredshold_botton_Click(object sender, EventArgs e)
        {
            Savei = new int[Sdis.Length];
            int thread = int.Parse(textBox2.Text);
            countCutFrame = 0;


            for (int i = 0; i < Sdis.Length; i++)
            {
                if (Sdis[i] > thread)
                {
                    Savei[countCutFrame] = i;
                    countCutFrame++;
                }


            }


            saveshot();


        }








        private void saveshot()
        {
            string str = Url;
            string[] sAry = str.Split('.');
            Url2 = sAry[0];

            VideoFileReader reader = new VideoFileReader();
            reader.Open(textBox1.Text);
            int co = 0;
            int end = int.Parse(reader.FrameCount.ToString());

            int countFrameCut = 0;
            VideoFileWriter writerShort = new VideoFileWriter();
            name = "..\\..\\..\\VideoName\\" + Url2 + "_" + Convert.ToString(countFrameCut) + ".avi";
            videoname = Url2 + "_" + countFrameCut + ".avi";
            writerShort.Open(name, reader.Width, reader.Height, reader.FrameRate, VideoCodec.MPEG4, 1000000);
            for (int i = 0; i < end; i++)
            {
                Bitmap videoFrame = reader.ReadVideoFrame();
                writerShort.WriteVideoFrame(videoFrame);
                videoFrame.Dispose();
                if (i == Savei[countFrameCut]) {
                    // cut short 
                    writerShort.Close();

                    keyFrame();
                 
                    
                    if (countCutFrame != countFrameCut)
                    {
                        name = "..\\..\\..\\VideoName\\" + Url2 + "_" + Convert.ToString(countFrameCut) + ".avi";
                        videoname = Url2 + "_" + countFrameCut + ".avi";
                        writerShort.Open(name, reader.Width, reader.Height, reader.FrameRate, VideoCodec.MPEG4, 1000000);
                    }
                    countFrameCut++;
                }
            }
            writerShort.Close();
            reader.Close();

            /*
            for (int i = 0; i < Savei.Length; i++)
            {
                
                if (Savei[i] == 0)
                {
                    if (co == 0)
                    {
                        Savei[i] = end;
                        co = 1;
                    }
                    else
                    {

                        break;
                    }
                }
               
                VideoFileWriter writer = new VideoFileWriter();

                try
                {
                    
                    name = "..\\..\\..\\VideoName\\" + Url2 + "_" + Convert.ToString(i) + ".avi";
                    videoname = Url2 + "_" + i + ".avi";
                    writer.Open(name, reader.Width, reader.Height, reader.FrameRate, VideoCodec.MPEG4, 1000000);

                    for (int j = 0; j < end; j++)
                    {
                        if (Savei[i] == j)
                        {
                            writer.Close();
                            break;
                        }
                        Bitmap videoFrame = reader.ReadVideoFrame();
                        
                        writer.WriteVideoFrame(videoFrame);
                        videoFrame.Dispose();
                    }
                   
                }
                catch (Exception exception)
                {
                    writer.Close();
                }

                if (i == 0)
                {
                    end = end - Savei[i];
                }
                else
                {
                    end = end - Math.Abs(Savei[i] - Savei[i - 1]);
                }
                
                keyFrame();


                //string constr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
                //SqlConnection con = new SqlConnection(constr);
                //con.Open();

                //SqlCommand cmd = new SqlCommand("INSERT into CollectionShot (No,VideoName,PathVideoName,KeyFrame,HistrogrameVecter) " +
                //       " VALUES ( (Select count(*) from CollectionShot ),'" + videoname + "','" + name + "' , '" + keyF + "','" + Vechist + "')", con);
                //cmd.ExecuteNonQuery();
                //con.Close();

            }
            reader.Close();
            */
        }

        private void keyFrame()
        {
            VideoFileReader readerShort= new VideoFileReader();
            readerShort.Open(name);

            // set many frame
            int Fcon = int.Parse(readerShort.FrameCount.ToString());
            // set frame/2
            int Fcount = Fcon / 2;

            for (int i = 0; i < readerShort.FrameCount; i++)
            {
                Bitmap videoFrame = readerShort.ReadVideoFrame();
                if (i == Fcount)
                {
                    hisVec(videoFrame);
                    keyF = ("..\\..\\..\\Keyframe\\" + Url2 + "_" + i + ".jpeg");
                    videoFrame.Save(keyF, ImageFormat.Jpeg);
                    break;
                }
                videoFrame.Dispose();
            }
            // close short video
            readerShort.Close();
        }



        private void hisVec(Bitmap videoFrame)
        {
            ImageStatistics rgbStatistics = new ImageStatistics(videoFrame);
            int[] redValues = rgbStatistics.Red.Values;
            int[] greenValues = rgbStatistics.Green.Values;
            int[] blueValues = rgbStatistics.Blue.Values;

            int[] his = new int[256];
            for (int j = 0; j < 256; j++)
            {
                his[j] = (redValues[j] + greenValues[j] + blueValues[j]) / 3;

            }

            Vechist = his[0].ToString();
            for (int i = 1; i < his.Length; i++)
            {
                Vechist = Vechist + " " + his[i].ToString();
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
        }
    }
}
