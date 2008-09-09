using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenCS.Common.Plugin;
using System.IO;

namespace DemoApp
{
    public partial class Form1 : Form
    {
        private IPluginHost mPluginHost = new DemoPluginHost();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mPluginHost.ExecutingPath = Path.GetFullPath(".");
            mPluginHost.LoadPlugins(Path.GetFullPath(textBoxPluginFolder.Text));
        }
    }
}