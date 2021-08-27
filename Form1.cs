﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Permissions;


namespace WebBrowser
{

    public partial class Form1 : Form
    {
        private System.Windows.Forms.WebBrowser webBrowser1 = new System.Windows.Forms.WebBrowser();
        private Button button1 = new Button();
        public Form1()
        {
            InitializeComponent();
            button1.Text = "call script code from client code";
            button1.Dock = DockStyle.Top;
            button1.Click += new EventHandler(button1_Click);
            webBrowser1.Dock = DockStyle.Fill;
            Controls.Add(webBrowser1);
            Controls.Add(button1);
          //  Load += new EventHandler(Form1_Load);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // webBrowser1.AllowWebBrowserDrop = false;
           // webBrowser1.IsWebBrowserContextMenuEnabled = false;
            //webBrowser1.WebBrowserShortcutsEnabled = false;
           // webBrowser1.ObjectForScripting = this;

            // Uncomment the following line when you are finished debugging.
            //webBrowser1.ScriptErrorsSuppressed = true;

           // webBrowser1.DocumentText =
          //  "<html><head><script>" +
           // "function test() { alert('123'); }" +
          //  "</script></head><body><button " +
          //  "onclick=\"window.external.Test('called from script code')\">" +
          //  "call client code from script code</button>" +
          //  "</body></html>";
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            webBrowser1.AllowWebBrowserDrop = false;
            webBrowser1.IsWebBrowserContextMenuEnabled = false;
            webBrowser1.WebBrowserShortcutsEnabled = false;
            webBrowser1.ObjectForScripting = new MyScriptObject(this);
            // Uncomment the following line when you are finished debugging.
            //webBrowser1.ScriptErrorsSuppressed = true;

            webBrowser1.DocumentText =
                "<html><head><script>" +
                "function test(message) { alert(message); }" +
                "</script></head><body><button " +
                "onclick=\"window.external.Test('called from script code')\">" +
                "call client code from script code</button>" +
                "</body></html>";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("test",
            new String[] { "called from client code" });
        }
    }
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class MyScriptObject
    {
        private Form1 _form;

        public MyScriptObject(Form1 form)
        {
            _form = form;
        }

        public void Test(string message)
        {
            MessageBox.Show(message, "client code");
        }
    }

}

