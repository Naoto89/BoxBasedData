using System.Drawing;
using System.Windows.Forms;

namespace BoxBasedDataTest
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            treeView_Test = new TreeView();
            button_Load = new Button();
            button_Save = new Button();
            SuspendLayout();
            // 
            // treeView_Test
            // 
            treeView_Test.Location = new Point(12, 12);
            treeView_Test.Name = "treeView_Test";
            treeView_Test.Size = new Size(603, 426);
            treeView_Test.TabIndex = 0;
            // 
            // button_Load
            // 
            button_Load.Location = new Point(621, 12);
            button_Load.Name = "button_Load";
            button_Load.Size = new Size(167, 50);
            button_Load.TabIndex = 1;
            button_Load.Text = "読み込み";
            button_Load.UseVisualStyleBackColor = true;
            button_Load.Click += button_Load_Click;
            // 
            // button_Save
            // 
            button_Save.Location = new Point(621, 68);
            button_Save.Name = "button_Save";
            button_Save.Size = new Size(167, 50);
            button_Save.TabIndex = 1;
            button_Save.Text = "保存";
            button_Save.UseVisualStyleBackColor = true;
            button_Save.Click += button_Save_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button_Save);
            Controls.Add(button_Load);
            Controls.Add(treeView_Test);
            Name = "FormMain";
            Text = "Box構造データ読み書きテスト";
            Load += FormMain_Load;
            ResumeLayout(false);
        }

        #endregion

        private TreeView treeView_Test;
        private Button button_Load;
        private Button button_Save;
    }
}
