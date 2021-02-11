using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myBrowser
{
    public partial class Form1 : Form
    {
        WebBrowser webTab = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser.Navigate("https://www.google.com/");
            webBrowser.DocumentCompleted += WebBrowser_DocumentCompleted;
        }

        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            tabControl.SelectedTab.Text = webBrowser.DocumentTitle;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            WebBrowser web = tabControl.SelectedTab.Controls[0] as WebBrowser;
            if( web != null)
            {
                Regex isSearch = new Regex("^¿?([\\wáéíóú]+\\s?)+\\??$");
                if( isSearch.IsMatch(textURL.Text))
                {
                    web.Navigate($"http://www.google.com/search?q={textURL.Text.Trim()}");
                    textURL.Text = $"http://www.google.com/search?q={textURL.Text.Trim()}";
                    web.DocumentCompleted += WebBrowser_DocumentCompleted;
                }
                else
                {
                    textURL.Text = normURL(textURL.Text);
                    web.Navigate(textURL.Text);
                }
            }
        }

        private string normURL(string text)
        {
            text.Trim();
            //^(https)?(http)?(://)?\w{3}?\.?\w+\.?\w{3,5}.*$
            Regex completeURL = new Regex("^(https)?(http)?(://)?\\w{3}?\\.?\\w+\\.?\\w{3,5}.*$");
            if( !completeURL.IsMatch(text))
            {
                return $"https://{text}";
            }
            return text;
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            TabPage tab = new TabPage();
            tab.Text = "New Tab";
            tabControl.Controls.Add(tab);
            tabControl.SelectTab(tabControl.TabCount - 1);
            webTab = new WebBrowser() { ScriptErrorsSuppressed = true };
            webTab.Parent = tab;
            webTab.Dock = DockStyle.Fill;
            webTab.Navigate("https://www.google.com/");
            webTab.DocumentCompleted += WebBrowser_DocumentCompleted;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            WebBrowser web = tabControl.SelectedTab.Controls[0] as WebBrowser;
            if( web != null )
            {
                if( web.CanGoBack )
                {
                    web.GoBack();
                }
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            WebBrowser web = tabControl.SelectedTab.Controls[0] as WebBrowser;
            if (web != null)
            {
                if (web.CanGoForward)
                {
                    web.GoForward();
                }
            }
        }
    }
}