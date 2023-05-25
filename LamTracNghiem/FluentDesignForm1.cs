using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace LamTracNghiem
{
    public partial class FluentDesignForm1 : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public class Question
        {
            public string Ques { get; set; }
            public List<string> Answer { get; set; }
            public bool Status { get; set; } = false;
            public string Ans { get; set; }
            public bool DaLam { get; set; } = false;
            public bool Dung { get; set; } = false;
            public string playerchon { get; set; }
        }

        public FluentDesignForm1()
        {
            InitializeComponent();
        }
        private OpenFileDialog openFileDialog1;

        private void UpdateDeThi()
        {
            string remoteUri = "https://raw.githubusercontent.com/kimstars/LamTracNghiem/main/LamTracNghiem/HCTC.txt";     
            string installDirectory = Application.StartupPath;
            string dest = Path.Combine(installDirectory, "HCTC.txt");

            using (WebClient webClient = new WebClient())
            {
                Form1 newform = new Form1();
                newform.Show();

                Console.WriteLine("Downloading file ...");

                // Download the Web resource and save it into the destination
                webClient.DownloadFile(remoteUri , dest);

                MessageBox.Show("Successfully downloaded file ");
            }
        }

        private void btnLoadDe_ItemClick(object sender, ItemClickEventArgs e)
        {
            currentQues = 0;
            socaudung = 0;
            socausai = 0;

            flPanelAns.Controls.Clear();
            flowLayoutPanel1.Controls.Clear();
            txtQues.Text = "";

            openFileDialog1 = new OpenFileDialog()
            {
                FileName = "Select a text file",
                Filter = "Text files (*.txt)|*.txt",
                Title = "Open text file"
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = openFileDialog1.FileName;
                    LoadDe(filePath);

                    //MessageBox.Show(lstQuestion[0].Ques);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        List<Question> lstQuestion;
        public void ExtractQuestions(string text)
        {
            lstQuestion = new List<Question>();

            //string[] lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            string[] lines = text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            string dapan = "ABCDE";

            string ques = null;
            string answer = null;
            bool newques = true;

            Question tempques = null;
            for (int i = 0; i < lines.Length; i++)
            {
                if (newques)
                {
                    tempques = new Question
                    {
                        Ques = "",
                        Answer = new List<string>(),
                        Ans = "",
                        Status = false
                    };
                    newques = false;
                    if (lines[i].StartsWith("Câu "))
                    {
                        answer = lines[i][lines[i].Length - 1].ToString();
                        ques = lines[i].Substring(0, lines[i].Length - 1);
                        i++;
                        while (!lines[i].StartsWith("A."))
                        {
                            ques += lines[i] + "\n";
                            ++i;
                        }
                        i--;
                    }


                }
                else
                {
                    string ansTemp = "";
                    for (int j = 0; j < dapan.Length; j++)
                    {
                        string startChar = dapan[j].ToString();

                        if (lines[i].StartsWith("Câu")) break;

                        if (lines[i].StartsWith(startChar + "."))
                        {
                            while (!lines[i].StartsWith(dapan[j + 1].ToString() + "."))
                            {
                                if (lines[i].StartsWith("Câu")) break;

                                ansTemp += lines[i] + "\n";
                                if (i < lines.Length - 1) ++i;
                                else break;
                            }
                            tempques.Answer.Add(ansTemp);
                            ansTemp = "";
                        }
                    }



                    tempques.Ques = ques;

                    tempques.Ans = answer;

                    lstQuestion.Add(tempques);
                    i--;

                    newques = true;
                }


            }


            //MessageBox.Show(lstQuestion.Count.ToString());


        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form1 newform = new Form1();
            newform.Show();
        }





        private void FluentDesignForm1_Load(object sender, EventArgs e)
        {
            //UpdateDeThi();

            if (File.Exists("./HCTC.txt"))
            {
                LoadDe("./HCTC.txt");
            }
            else
            {
                Form1 newform = new Form1();
                newform.Show();
            }
        }

        private void LoadDe(string path)
        {
            Encoding isoLatin1 = Encoding.UTF8;

            string text = File.ReadAllText(path, isoLatin1);
            ExtractQuestions(text);

            flowLayoutPanel1.Controls.Clear();

            for (int i = 0; i < lstQuestion.Count; i++)
            {
                Button btn = new Button();


                btn.Name = "btn" + i;
                btn.Tag = i;
                btn.Text = i.ToString();
                btn.Font = new Font("Tahoma", 16f, FontStyle.Bold);
                // btn.UseCompatibleTextRendering = true;
                if (lstQuestion[i].Status)
                {
                    btn.BackColor = Color.Green;
                }
                else
                {
                    btn.BackColor = Color.White;
                }

                btn.Height = 57;
                btn.Width = 116;
                btn.Click += button1_Click;

                flowLayoutPanel1.Controls.Add(btn);

            }
            Question oneques = lstQuestion[0];

            txtQues.Text = oneques.Ques;



            flPanelAns.Controls.Clear();

            foreach (string ques in oneques.Answer)
            {
                flPanelAns.Controls.Add(newButton(ques));

            }


            currentQues = 0;
            socaudung = 0;
            socausai = 0;
            lbSoCauDung.Text = socaudung.ToString();
            lbSoCauSai.Text = socausai.ToString();
            lbTongSoCau.Text = lstQuestion.Count.ToString();

        }

        int currentQues = 0;
        int socaudung = 0;
        int socausai = 0;


        private RadioButton newButton(string text)
        {
            RadioButton Rbtn = new RadioButton();
            Rbtn.Text = text;
            Rbtn.Name = "rBtn" + text[0].ToString();
            Rbtn.Tag = text[0].ToString();
            Rbtn.Font = new Font("Palatino Linotype", 13.8f);
            Rbtn.AutoSize = true;
            Rbtn.Click += checkDapan;
            //Rbtn.Size = new System.Drawing.Size(483, 91);
            return Rbtn;
        }

 
        private void LoadCauHoi(int stt)
        {
            Question oneques = lstQuestion[stt];
            currentQues = stt;
            txtQues.Text = oneques.Ques;

            flPanelAns.Controls.Clear();

            foreach (string ques in oneques.Answer)
            {
                flPanelAns.Controls.Add(newButton(ques));

            }

        }

        protected void button1_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                //MessageBox.Show(lstQuestion[Convert.ToInt32(b.Tag)].Ques);
                currentQues = Convert.ToInt32(b.Tag);
                LoadCauHoi(currentQues);
            }

            foreach (Control control in flPanelAns.Controls)
            {
                RadioButton btn = control as RadioButton;
                if (lstQuestion[currentQues].DaLam)
                {

                    if (!lstQuestion[currentQues].Dung && btn != null && btn.Tag.ToString() == lstQuestion[currentQues].playerchon)
                    {
                        btn.ForeColor = Color.Red;
                    }
                    if (lstQuestion[currentQues].Dung && btn != null && btn.Tag.ToString() == lstQuestion[currentQues].Ans)
                    {
                        //MessageBox.Show(radioButton.Tag.ToString());
                        btn.ForeColor = Color.Green;
                    }
                }

            }

        }


        protected void checkDapan(object sender, EventArgs e)
        {

            RadioButton b = sender as RadioButton;
            lstQuestion[currentQues].DaLam = true;
            lstQuestion[currentQues].playerchon = b.Tag.ToString();

            if (lstQuestion[currentQues].Ans == b.Tag.ToString())
            {
                b.ForeColor = Color.Green;
                //MessageBox.Show("Chinh xac");
                foreach (Control control in flowLayoutPanel1.Controls)
                {
                    Button btn = control as Button;
                    if (btn != null && btn.Tag.ToString() == currentQues.ToString())
                    {
                        btn.BackColor = Color.Green;
                        btn.Text = currentQues.ToString() + lstQuestion[currentQues].Ans;
                        break;
                    }
                }
                lstQuestion[currentQues].Dung = true;
            }
            else
            {
                b.ForeColor = Color.Red;
                // Tìm RadioButton đáp án đúng để thay đổi màu chữ thành màu xanh
                foreach (Control control in flPanelAns.Controls) // Thay groupBox1 bằng tên của GroupBox chứa các RadioButton
                {
                    RadioButton radioButton = control as RadioButton;
                    if (radioButton != null && radioButton.Tag.ToString() == lstQuestion[currentQues].Ans)
                    {
                        //MessageBox.Show(radioButton.Tag.ToString());
                        radioButton.ForeColor = Color.Green;
                        break;
                    }
                }
                foreach (Control control in flowLayoutPanel1.Controls)
                {
                    Button btn = control as Button;
                    if (btn != null && btn.Tag.ToString() == currentQues.ToString())
                    {
                        btn.BackColor = Color.Red;
                        break;
                    }
                }
                lstQuestion[currentQues].Dung = false;

            }





            ReloadSoLuong();

            lbTongSoCau.Text = lstQuestion.Count.ToString();



        }

        private void ReloadSoLuong()
        {
            socaudung = 0;
            socausai = 0;
            foreach (var item in lstQuestion)
            {
                if (item.DaLam)
                {

                    if (item.Dung)
                    {
                        socaudung += 1;
                    }
                    else
                    {
                        socausai += 1;
                    }
                }

            }

            lbSoCauDung.Text = socaudung.ToString();
            lbSoCauSai.Text = socausai.ToString();
        }


        private void btnReload_Click(object sender, EventArgs e)
        {
            currentQues = 0;
            socaudung = 0;
            socausai = 0;
            lbSoCauDung.Text = socaudung.ToString();
            lbSoCauSai.Text = socausai.ToString();

            flPanelAns.Controls.Clear();
            txtQues.Text = "";
            foreach (var item in lstQuestion)
            {
                item.DaLam = false;
            }

            foreach (Control control in flowLayoutPanel1.Controls)
            {
                Button btn = control as Button;
                if (btn != null)
                {
                    btn.Text = btn.Tag.ToString();
                    btn.BackColor = Color.White;
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            currentQues = 0;
            socaudung = 0;
            socausai = 0;
            lbSoCauDung.Text = socaudung.ToString();
            lbSoCauSai.Text = socausai.ToString();

            flPanelAns.Controls.Clear();
            flowLayoutPanel1.Controls.Clear();
            txtQues.Text = "";
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            Report newform = new Report();
            newform.Show();
        }

        private void btnTron_Click(object sender, EventArgs e)
        {
            currentQues = 0;
            socaudung = 0;
            socausai = 0;
            lbSoCauDung.Text = socaudung.ToString();
            lbSoCauSai.Text = socausai.ToString();

            flPanelAns.Controls.Clear();
            txtQues.Text = "";
            foreach (var item in lstQuestion)
            {
                item.DaLam = false;
                item.Dung = false;
            }

            foreach (Control control in flowLayoutPanel1.Controls)
            {
                Button btn = control as Button;
                if (btn != null)
                {
                    btn.Text = btn.Tag.ToString();
                    btn.BackColor = Color.White;
                }
            }
            var rnd = new Random();
            Suffler.Shuffle(lstQuestion, rnd);

        }
    }
}
