namespace SpotlightGallery {
  partial class Form1 {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing ) {
      if ( disposing && ( components != null ) ) {
        components.Dispose();
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.imagePanel = new System.Windows.Forms.FlowLayoutPanel();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.SuspendLayout();
      // 
      // imagePanel
      // 
      this.imagePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.imagePanel.AutoScroll = true;
      this.imagePanel.BackColor = System.Drawing.Color.White;
      this.imagePanel.Location = new System.Drawing.Point(12, 12);
      this.imagePanel.Name = "imagePanel";
      this.imagePanel.Size = new System.Drawing.Size(776, 426);
      this.imagePanel.TabIndex = 1;
      // 
      // saveFileDialog1
      // 
      this.saveFileDialog1.CheckPathExists = false;
      this.saveFileDialog1.DefaultExt = "jpg";
      this.saveFileDialog1.Filter = "JPEG Files|*.jpg;*.jpeg|All Files|*";
      this.saveFileDialog1.RestoreDirectory = true;
      this.saveFileDialog1.Title = "Save image to...";
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.imagePanel);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "Form1";
      this.Text = "SpotlightGallery v0.1";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.FlowLayoutPanel imagePanel;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
  }
}

