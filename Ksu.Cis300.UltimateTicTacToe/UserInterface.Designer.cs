namespace Ksu.Cis300.UltimateTicTacToe
{
    partial class UserInterface
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uxFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.uxTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // uxFlowLayoutPanel
            // 
            this.uxFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.uxFlowLayoutPanel.Location = new System.Drawing.Point(12, 12);
            this.uxFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(2);
            this.uxFlowLayoutPanel.Name = "uxFlowLayoutPanel";
            this.uxFlowLayoutPanel.Size = new System.Drawing.Size(370, 441);
            this.uxFlowLayoutPanel.TabIndex = 0;
            // 
            // uxTextBox
            // 
            this.uxTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxTextBox.Location = new System.Drawing.Point(12, 463);
            this.uxTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.uxTextBox.Name = "uxTextBox";
            this.uxTextBox.ReadOnly = true;
            this.uxTextBox.Size = new System.Drawing.Size(372, 35);
            this.uxTextBox.TabIndex = 1;
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 511);
            this.Controls.Add(this.uxTextBox);
            this.Controls.Add(this.uxFlowLayoutPanel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UserInterface";
            this.Text = "Ultimate Tic Tac Toe";
            this.Load += new System.EventHandler(this.UserInterface_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel uxFlowLayoutPanel;
        private System.Windows.Forms.TextBox uxTextBox;
    }
}

