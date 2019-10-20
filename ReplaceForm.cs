using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class ReplaceForm : Form
    {
        public int NextSearchIndex { get; set; }
        public ReplaceForm()
        {
            InitializeComponent();
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            NextSearchIndex = 0;
        }
        public string TextBoxSharedReplace
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public Button FindNextButtonSharedReplace
        {
            get { return button1; }
            set { button1 = value; }
        }

        public void ButtonsAvailiability()
        {
            if (textBox1.Text.Length > 0)
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ButtonsAvailiability();
        }

        private void ReplaceFormSearchNextButton_Click(object sender, EventArgs e)
        {
            MainForm main = this.Owner as MainForm; bool positiveResult = false;
            string[] temp = main.MyRichText.Lines; main.MyRichText.Text = ""; main.MyRichText.Lines = temp;

            if (main.MyRichText.Find(textBox1.Text) >= 0 && textBox1.Text.Length > 0 && main != null && checkBox1.Checked == true)
            {
                if (NextSearchIndex != 0)
                {
                    NextSearchIndex = main.MyRichText.Find(textBox1.Text, (NextSearchIndex + 1), main.MyRichText.Text.Length, RichTextBoxFinds.MatchCase);
                    if (NextSearchIndex < 0)
                        NextSearchIndex = 0;
                }
                if (NextSearchIndex == 0)
                    NextSearchIndex = main.MyRichText.Find(textBox1.Text, NextSearchIndex, main.MyRichText.Text.Length, RichTextBoxFinds.MatchCase);

                main.MyRichText.SelectionStart = NextSearchIndex;
                main.MyRichText.SelectionLength = textBox1.Text.Length;
                main.MyRichText.SelectionBackColor = Color.Teal;
                main.NextSearchIndex = main.MyRichText.Find(textBox1.Text);
                positiveResult = true;
            }

            if (main.MyRichText.Find(textBox1.Text) >= 0 && textBox1.Text.Length > 0 && main != null && checkBox1.Checked == false)
            {
                if (NextSearchIndex != 0)
                {
                    NextSearchIndex = main.MyRichText.Find(textBox1.Text, (NextSearchIndex + 1), main.MyRichText.Text.Length, RichTextBoxFinds.None);
                    if (NextSearchIndex < 0)
                        NextSearchIndex = 0;
                }
                if (NextSearchIndex == 0)
                    NextSearchIndex = main.MyRichText.Find(textBox1.Text, 0, main.MyRichText.Text.Length, RichTextBoxFinds.None);

                main.MyRichText.SelectionStart = NextSearchIndex;
                main.MyRichText.SelectionLength = textBox1.Text.Length;
                main.MyRichText.SelectionBackColor = Color.Teal;
                main.NextSearchIndex = main.MyRichText.Find(textBox1.Text);
                positiveResult = true;
            }

            if (positiveResult == false)
                MessageBox.Show("Не удается найти \"" + textBox1.Text + "\"", "Блокнот");

        }

        private void ReplaceFormReplaceButton_Click(object sender, EventArgs e)
        {
            MainForm main = this.Owner as MainForm;
            bool positiveResult = false;
            if (main.MyRichText.Text.IndexOf(textBox1.Text) == NextSearchIndex && textBox2.Text.Length > 0)
                { 
                 main.MyRichText.Rtf = main.MyRichText.Rtf.Replace(textBox1.Text, textBox2.Text);
                positiveResult = true;
                }
            if (positiveResult == false)
                MessageBox.Show("Не удается найти \"" + textBox1.Text + "\"", "Блокнот");
        }
         private void ReplaceFormReplaceAllButton_Click(object sender, EventArgs e)
        {
            MainForm main = this.Owner as MainForm;
            int searchStart = 0;
            string[] temp = main.MyRichText.Lines; main.MyRichText.Text = ""; main.MyRichText.Lines = temp;
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && main != null && checkBox1.Checked == false)
            {
                while (searchStart < main.MyRichText.Text.ToLower().LastIndexOf(textBox1.Text.ToLower()))
                {
                    main.MyRichText.Find(textBox1.Text, searchStart, main.MyRichText.Text.Length, RichTextBoxFinds.None);
                    //main.MyRichText.SelectionBackColor = Color.Yellow;
                    main.MyRichText.Rtf = main.MyRichText.Rtf.Replace(textBox1.Text, textBox2.Text);
                    searchStart = main.MyRichText.Text.ToLower().IndexOf(textBox1.Text.ToLower(), searchStart) + 1;
                }
            }
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && main != null && checkBox1.Checked == true)
            {
                while (searchStart < main.MyRichText.Text.LastIndexOf(textBox1.Text))
                {
                    main.MyRichText.Find(textBox1.Text, searchStart, main.MyRichText.Text.Length, RichTextBoxFinds.MatchCase);
                   // main.MyRichText.SelectionBackColor = Color.Yellow;
                    main.MyRichText.Rtf = main.MyRichText.Rtf.Replace(textBox1.Text, textBox2.Text);
                    searchStart = main.MyRichText.Text.IndexOf(textBox1.Text, searchStart) + 1;
                }
            }

        }
        private void ReplaceFormCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
