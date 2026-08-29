using BoxBasedDataLib;
using System;
using System.Text;
using System.Windows.Forms;

namespace BoxBasedDataTest
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            
            Box root = new Box("Root");

            Box header = new Box("Header");

            header.AddChild(new Box("Version", BitConverter.GetBytes(1)));

            header.AddChild(new Box("Name", Encoding.UTF8.GetBytes("Sample")));
            root.AddChild(header);

            Box video = new Box("Video");
            video.AddChild(new Box("Width", BitConverter.GetBytes(1920)));
            video.AddChild(new Box("Height", BitConverter.GetBytes(1080)));
            video.AddChild(new Box("FrameData", new byte[] { 1, 2, 3, 4, 5 }));
            root.AddChild(video);

            BoxWriter.Write(@".\sample.box", root);

            var trees = root.CreateTreeNode();
            treeView_Test.Nodes.Add(trees);

        }

        /// <summary>
        /// ファイル読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Load_Click(object sender, EventArgs e)
        {
            var tmp = BoxReader.Read(@".\sample.box");

            var trees = tmp.CreateTreeNode();
            treeView_Test.Nodes.Add(trees);
        }

        /// <summary>
        /// ファイル保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Save_Click(object sender, EventArgs e)
        {

        }
    }
}
