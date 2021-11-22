using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;

namespace Lab2Films
{
    public partial class FilmsForm : Form
    {
        private string path = "XMLFilms.xml";  //  \\Mac\Home\Desktop\Lab2Films\Lab2Films\XMLFilms.xml
        private string pathXSL = "XSLFilm.xsl";

        public FilmsForm()
        {
            InitializeComponent();
            BuiltBox(comboBoxName, comboBoxGenre, comboBoxYear, comboBoxDirector, comboBoxCountry, comboBoxLanguage);
            comboBoxName.Enabled = false;
            comboBoxGenre.Enabled = false;
            comboBoxYear.Enabled = false;
            comboBoxDirector.Enabled = false;
            comboBoxCountry.Enabled = false;
            comboBoxLanguage.Enabled = false;
        }

        public void BuiltBox(ComboBox nameBox, ComboBox genreBox, ComboBox yearBox, 
            ComboBox directorBox, ComboBox countryBox, ComboBox languageBox)
        {
            IAnalizatorXMLStrategy i = new LinqToXML();
            List<Films> res = i.AnalyzeFile(new Films(), path);
            List<string> name = new List<string>();
            List<string> genre = new List<string>();
            List<string> year = new List<string>();
            List<string> director = new List<string>();
            List<string> country = new List<string>();
            List<string> language = new List<string>();

            foreach( Films elem in res)
            {
                if (!name.Contains(elem.Name))
                {
                    name.Add(elem.Name);
                }
                if (!genre.Contains(elem.Genre))
                {
                    genre.Add(elem.Genre);
                }
                if (!year.Contains(elem.Year))
                {
                    year.Add(elem.Year);
                }
                if (!director.Contains(elem.Director))
                {
                    director.Add(elem.Director);
                }
                if (!country.Contains(elem.Country))
                {
                    country.Add(elem.Country);
                }
                if (!language.Contains(elem.Language))
                {
                    language.Add(elem.Language);
                }
            }

            name = name.OrderBy(x => x).ToList();
            genre = genre.OrderBy(x => x).ToList();
            year = year.OrderBy(x => x).ToList();
            director = director.OrderBy(x => x).ToList();
            country = country.OrderBy(x => x).ToList();
            language = language.OrderBy(x => x).ToList();

            nameBox.Items.AddRange(name.ToArray());
            genreBox.Items.AddRange(genre.ToArray());
            yearBox.Items.AddRange(year.ToArray());
            directorBox.Items.AddRange(director.ToArray());
            countryBox.Items.AddRange(country.ToArray());
            languageBox.Items.AddRange(language.ToArray());
        }

        private Films OurSearch()
        {
            string[] info = new string[7];
            if (checkBoxName.Checked) { info[0] = Convert.ToString(comboBoxName.Text); }
            if (checkBoxGenre.Checked) { info[1] = Convert.ToString(comboBoxGenre.Text); }
            if (checkBoxYear.Checked) { info[2] = Convert.ToString(comboBoxYear.Text); }
            if (checkBoxDirector.Checked) { info[3] = Convert.ToString(comboBoxDirector.Text); }
            if (checkBoxCountry.Checked) { info[4] = Convert.ToString(comboBoxCountry.Text); }
            if (checkBoxLanguage.Checked) { info[5] = Convert.ToString(comboBoxLanguage.Text); }
            Films idealSearch = new Films(info);
            return idealSearch;
        }

        private void ParserForXML()
        {
            Films myTemplate = OurSearch();
            List<Films> res;

            if (radioButtonDOM.Checked)
            {
                IAnalizatorXMLStrategy parser = new DOM();
                res = parser.AnalyzeFile(myTemplate, path);
                Output(res);
            }
            else if (radioButtonLINQ.Checked)
            {
                IAnalizatorXMLStrategy parser = new LinqToXML();
                res = parser.AnalyzeFile(myTemplate, path);
                Output(res);
            }
            else if (radioButtonSAX.Checked)
            {
                IAnalizatorXMLStrategy parser = new SAX();
                res = parser.AnalyzeFile(myTemplate, path);
                Output(res);
            }
        }

        private void Output(List<Films> res)
        {
            richTextBox1.Clear();
             
            foreach (Films n in res)
            {
                richTextBox1.AppendText("Name: " + n.Name + "\n");
                richTextBox1.AppendText("Genre: " + n.Genre + "\n");
                richTextBox1.AppendText("Year: " + n.Year + "\n");
                richTextBox1.AppendText("Director: " + n.Director + "\n");
                richTextBox1.AppendText("Country: " + n.Country + "\n");
                richTextBox1.AppendText("Language: " + n.Language + "\n");
                richTextBox1.AppendText("_____________________________________\n");
            }
        }

        
        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            richTextBox1.Clear();
            radioButtonDOM.Checked = false;
            radioButtonLINQ.Checked = false;
            radioButtonSAX.Checked = false;
            comboBoxName.Text = null;
            comboBoxGenre.Text = null;
            comboBoxYear.Text = null;
            comboBoxDirector.Text = null;
            comboBoxCountry.Text = null;
            comboBoxLanguage.Text = null;
            checkBoxName.Checked = false;
            checkBoxGenre.Checked = false;
            checkBoxYear.Checked = false;
            checkBoxDirector.Checked = false;
            checkBoxCountry.Checked = false;
            checkBoxLanguage.Checked = false;
        }

        private void Help()
        {
            MessageBox.Show("Короткі відомості про програму\n" +
               "Дана програма допоможе вам вибрати фільм\n" +
               "Ви можете відсортувати за такими показниками\n\t" +
               "1. Name - назва фільму \n\t " +
               "2. Genre - жанр \n\t " +
               "3. Year - рік виходу \n\t " +
               "4. Director - режисер \n\t " +
               "5. Country - країна  \n\t " +
               "6. Language - мова",
               "HELP меню");
        }
        private void buttonTransform_Click(object sender, EventArgs e)
        {
            IntoHTML();
            OpenHTML();
        }
        private void buttonOpenHTML_Click(object sender, EventArgs e)
        {
            var OpenHTML = System.Diagnostics.Process.Start("HTML.html");
        }
        private void OpenHTML()
        {
            buttonOpenHTML.Enabled = true;
        }
        private void IntoHTML()
        {
            XslCompiledTransform xsl = new XslCompiledTransform();
            xsl.Load(pathXSL);
            string input = path;
            string result = @"HTML.html";
            xsl.Transform(input, result);
            MessageBox.Show("HTML done!");
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            ParserForXML();
        }
        
        private void FilmsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Ви впевнені, що хочете закрити форму?",
                "WARNING",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);

            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }
        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help();
        }
        private void Films_Load(object sender, EventArgs e)
        {
            
        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void checkBoxName_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxName.Checked) { comboBoxName.Enabled = true; } else { comboBoxName.Enabled = false; }
        }

        private void checkBoxGenre_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxGenre.Checked) { comboBoxGenre.Enabled = true; } else { comboBoxGenre.Enabled = false; }
        }

        private void checkBoxYear_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxYear.Checked) { comboBoxYear.Enabled = true; } else { comboBoxYear.Enabled = false; }
        }

        private void checkBoxDirector_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDirector.Checked) { comboBoxDirector.Enabled = true; } else { comboBoxDirector.Enabled = false; }
        }

        private void checkBoxCountry_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCountry.Checked) { comboBoxCountry.Enabled = true; } else { comboBoxCountry.Enabled = false; }
        }

        private void checkBoxLanguage_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLanguage.Checked) { comboBoxLanguage.Enabled = true; } else { comboBoxLanguage.Enabled = false; }
        }
    }
}
