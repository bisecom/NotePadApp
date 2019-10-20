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
    public partial class SearchForm : Form
    {
        public int NextSearchIndex { set; get; }
        public RichTextBox tempRichBox;
        public int SearchStart { set; get; }
        public SearchForm()
        {
            InitializeComponent();
            button1.Enabled = false;
            checkBox1.Checked = false;
            radioButton2.Checked = true;
            NextSearchIndex = 0;
            tempRichBox = new RichTextBox();
            SearchStart = 1;
        }

        
        public string TextBoxShared
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        private void SearchFormCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SearchFormSearchButton_Click(object sender, EventArgs e)
        {
            MainForm main = this.Owner as MainForm;
            //main.MyRichText.SelectionStart = main.MyRichText.Text.Length - main.MyRichText.Text.Length;
            //main.MyRichText.SelectionLength = main.MyRichText.Text.Length;
            //main.MyRichText.SelectionBackColor = Color.Empty;
            //main.MyRichText.Refresh();
            string []temp = main.MyRichText.Lines; main.MyRichText.Text = ""; main.MyRichText.Lines = temp;
            
            if (main.MyRichText.Text.Contains(textBox1.Text) && textBox1.Text.Length > 0 && main != null && checkBox1.Checked == true && radioButton2.Checked == true)
            {

                if (NextSearchIndex != 0)
                {
                    NextSearchIndex = main.MyRichText.Find(textBox1.Text, (NextSearchIndex + 1), main.MyRichText.Text.Length, RichTextBoxFinds.MatchCase);
                    if (NextSearchIndex < 0)
                        NextSearchIndex = 0;
                }
                if (NextSearchIndex == 0)
                    NextSearchIndex = main.MyRichText.Find(textBox1.Text, 0, main.MyRichText.Text.Length, RichTextBoxFinds.MatchCase);
              
                main.MyRichText.SelectionStart = NextSearchIndex;
                main.MyRichText.SelectionLength = textBox1.Text.Length;
                main.MyRichText.SelectionBackColor = Color.Teal;
                main.NextSearchIndex = main.MyRichText.Find(textBox1.Text);
            }

            if (main.MyRichText.Text.ToLower().Contains(textBox1.Text.ToLower()) && textBox1.Text.Length > 0 && main != null && checkBox1.Checked == false && radioButton2.Checked == true)
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
            }

            if (main.MyRichText.Text.Contains(textBox1.Text) && textBox1.Text.Length > 0 && main != null && checkBox1.Checked == false && radioButton1.Checked == true)
            {

                if (NextSearchIndex != 0)
                { 
                NextSearchIndex = main.MyRichText.Find(textBox1.Text, SearchStart, NextSearchIndex, RichTextBoxFinds.Reverse);
                    if (NextSearchIndex < 0)
                    {
                        NextSearchIndex = main.MyRichText.Text.Length;
                        NextSearchIndex = main.MyRichText.Find(textBox1.Text, SearchStart, NextSearchIndex, RichTextBoxFinds.Reverse);
                    }
                }
                if (NextSearchIndex == 0)
                    NextSearchIndex = main.MyRichText.Find(textBox1.Text, SearchStart, main.MyRichText.Text.Length, RichTextBoxFinds.Reverse);
               
                main.MyRichText.SelectionStart = NextSearchIndex;
                main.MyRichText.SelectionLength = textBox1.Text.Length;
                main.MyRichText.SelectionBackColor = Color.Teal;
                main.NextSearchIndex = main.MyRichText.Find(textBox1.Text);
            }

            if (main.MyRichText.Text.Contains(textBox1.Text) && textBox1.Text.Length > 0 && main != null && checkBox1.Checked == true && radioButton1.Checked == true)
            {
                if (NextSearchIndex != 0)
                {
                    NextSearchIndex = main.MyRichText.Find(textBox1.Text, SearchStart, NextSearchIndex, RichTextBoxFinds.Reverse | RichTextBoxFinds.MatchCase);
                    if (NextSearchIndex < 0)
                    {
                        NextSearchIndex = main.MyRichText.Text.Length;
                        NextSearchIndex = main.MyRichText.Find(textBox1.Text, SearchStart, NextSearchIndex, RichTextBoxFinds.Reverse | RichTextBoxFinds.MatchCase);
                    }
                }

            if (NextSearchIndex == 0)
                    NextSearchIndex = main.MyRichText.Find(textBox1.Text, SearchStart, main.MyRichText.Text.Length, RichTextBoxFinds.Reverse | RichTextBoxFinds.MatchCase);

                main.MyRichText.SelectionStart = NextSearchIndex;
                main.MyRichText.SelectionLength = textBox1.Text.Length;
                main.MyRichText.SelectionBackColor = Color.Teal;
                main.NextSearchIndex = main.MyRichText.Find(textBox1.Text);
            }

            if (!main.MyRichText.Text.Contains(textBox1.Text) && textBox1.Text.Length > 0 && main != null && checkBox1.Checked == true)
                MessageBox.Show("Не удается найти \"" + textBox1.Text + "\"", "Блокнот");

            if (!main.MyRichText.Text.ToLower().Contains(textBox1.Text.ToLower()) && textBox1.Text.Length > 0 && main != null)
                MessageBox.Show("Не удается найти \"" + textBox1.Text + "\"", "Блокнот");

            if (textBox1.Text.Length > 0)//для опции "Найти далее"
                main.NextSearch = textBox1.Text;
        }

        public void ButtonsAvailiability()
        {
            if (textBox1.Text.Length > 0)
            { button1.Enabled = true;}
            else
            { button1.Enabled = false; }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ButtonsAvailiability();
        }


    }
}
