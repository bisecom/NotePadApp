using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class MainForm : Form
    {
        public string NextSearch { get; set; }
        public int NextSearchIndex { get; set; }
        public string FilePath { get; set; }
       
        public RichTextBox MyRichText
        {
            get { return richTextBox; }
            set { richTextBox = value; }
        }

        public MainForm()
        {
            InitializeComponent();
           
            Debug.WriteLine($"Modified: {richTextBox.Modified}");
            richTextBox.ForeColor = Properties.Settings.Default.ForeColor;
            richTextBox.Font = Properties.Settings.Default.Font;
            Debug.WriteLine($"Modified: {richTextBox.Modified}");
            NextSearch = null;
            NextSearchIndex = 0;
            FilePath = null;
        }

        private bool checkSave()
        {
            if (richTextBox.Modified)
            {
                switch (MessageBox.Show(this, "Save changes?", "?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        return Save();
                    case DialogResult.Cancel:
                        return false;
                }
            }
            return true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkSave())
            {
                Application.Exit();
            }
        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e) => richTextBox.Cut();

        private void copyToolStripMenuItem_Click(object sender, EventArgs e) => richTextBox.Copy();

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e) => richTextBox.Paste();

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) => richTextBox.SelectedText = "";

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e) => richTextBox.SelectAll();

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkSave())
            {
                richTextBox.Clear();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                richTextBox.Text = File.ReadAllText(openFileDialog.FileName);
                Debug.WriteLine($"Modified: {richTextBox.Modified}");
            }
        }

        
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "Файлы txt (*.txt)|*.txt|Все файлы (*.*)|*.*";
                dialog.FilterIndex = 2;
                dialog.RestoreDirectory = true;
                dialog.FileName = "your_file_name.txt";

                if (dialog.ShowDialog() == DialogResult.OK && dialog.FileName.Length > 0)
                {
                    richTextBox.SaveFile(dialog.FileName, RichTextBoxStreamType.PlainText);
                    FilePath = dialog.FileName;
                }

            }
        }

        private bool Save()
        {
            if (FilePath != null)
            {
                File.WriteAllText(FilePath, richTextBox.Text);
                return true;
            }
            if (FilePath == null)
            {
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Filter = "Файлы txt (*.txt)|*.txt|Все файлы (*.*)|*.*";
                    dialog.FilterIndex = 2;
                    dialog.RestoreDirectory = true;
                    dialog.FileName = "your_file_name.txt";

                    if (dialog.ShowDialog() == DialogResult.OK && dialog.FileName.Length > 0)
                    {
                        richTextBox.SaveFile(dialog.FileName, RichTextBoxStreamType.PlainText);
                        FilePath = dialog.FileName;
                    }

                }
                return true;
            }

            return false;
        }

        private void MainFormSaveToolStripButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void MainFormSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.WordWrap = wordWrapToolStripMenuItem.Checked;
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debug.WriteLine($"Modified: {richTextBox.Modified}");
            fontDialog.Font = richTextBox.Font;
            fontDialog.Color = richTextBox.ForeColor;
            if (fontDialog.ShowDialog() != DialogResult.Cancel)
            {
                richTextBox.Font = fontDialog.Font;
                richTextBox.ForeColor = fontDialog.Color;
                Properties.Settings.Default.Font = fontDialog.Font;
                Properties.Settings.Default.ForeColor = fontDialog.Color;
                Properties.Settings.Default.Save();
                Debug.WriteLine($"Modified: {richTextBox.Modified}");
            }
            
        }

        private void aboutNotepadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        public string RichtextboxShared
        {
            get { return this.richTextBox.Text; }
            set { this.richTextBox.Text = value; }
        }

   
        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            // Set the Document property to the PrintDocument for 
            // which the PrintPage Event has been handled. To display the
            // dialog, either this property or the PrinterSettings property 
            // must be set 
            DialogResult result = printDialog.ShowDialog();

            // If the result is OK then print the document.
            if (result == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Draw the content.
            e.Graphics.DrawString(richTextBox.Text, richTextBox.Font, new SolidBrush(richTextBox.ForeColor), 10, 10);
        }

        private void SearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
           //--------------------------------------------------
            SearchForm mysearch = new SearchForm();
            mysearch.Owner = this;
            mysearch.Show();

            //--------------------------------------------------
        }

        private void ReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReplaceForm myreplace = new ReplaceForm();
            myreplace.Owner = this;
            myreplace.Show();
        }

        private void SearchNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
           if(NextSearch != "")
            {
                string[] temp = richTextBox.Lines; richTextBox.Text = ""; richTextBox.Lines = temp;
                NextSearchIndex = richTextBox.Find(NextSearch, (NextSearchIndex+1), richTextBox.Text.Length, RichTextBoxFinds.None);

            }

        }

        private void MainForm_Closing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show($"Сохранить изменения в файле \"{FilePath}\"?", "Блокнот",
                                                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
             Save();
            else if (result == DialogResult.No)
             e.Cancel = false;
            else if (result == DialogResult.Cancel)
             e.Cancel = true;
  
        }

    }
}
