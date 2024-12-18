using System;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SpotlightGallery {
  public partial class Form1 : Form {
    string desktopPicPath = Environment.ExpandEnvironmentVariables(
      @"%USERPROFILE%\AppData\Local\Packages\MicrosoftWindows.Client.CBS_cw5n1h2txyewy\LocalCache\Microsoft\IrisService\"
    );
    string lockscreenPicPath = Environment.ExpandEnvironmentVariables(
      @"%USERPROFILE%\AppData\Local\Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets\"
    );
    ContextMenuStrip contextMenu = new ContextMenuStrip();

    public Form1() {
      InitializeComponent();
      AutoScaleMode = AutoScaleMode.None;

      contextMenu.Items.Add(
        "Save as...", null,
        onClick: ( object sender, EventArgs e ) => {
          DialogResult result = saveFileDialog1.ShowDialog();
          if ( result == DialogResult.OK ) {
            File.Copy( ( string ) contextMenu.Tag, saveFileDialog1.FileName, true );
          }
        }
      );

      contextMenu.Items.Add(
        "Open file location", null,
        onClick: ( object sender, EventArgs e ) => {
          OpenFileExplorerToImage( ( string ) contextMenu.Tag );
        }
      );
    }

    const int SW_SHOWNORMAL = 1;
    [DllImport( "shell32.dll", CharSet = CharSet.Auto )]
    public static extern IntPtr ShellExecute(
        IntPtr hwnd,         // Handle to the parent window (null for no parent)
        string lpOperation,  // Operation to perform (e.g., "open", "print", etc.)
        string lpFile,       // The file to open
        string lpParameters, // Parameters (optional, can be null)
        string lpDirectory,  // Initial directory (optional, can be null)
        int nShowCmd         // How to display the window (e.g., SW_SHOWNORMAL)
    );

    void OpenFile( string path ) {
      ShellExecute( IntPtr.Zero, "open", path, null, null, SW_SHOWNORMAL );
    }

    void OpenFileExplorerToImage( string fullPath ) {
      Process.Start( "explorer.exe", $"/select, \"{ fullPath }\"" );
    }

    void DrawImages( string[] picturePaths, int imageWidth = 300, string tag = "" ) {
      foreach ( string picturePath in picturePaths ) {
        Image image = Image.FromFile( picturePath );
        Bitmap picture = new Bitmap( image );

        double scale = ( double ) picture.Width / picture.Height;
        if ( scale != 16.0 / 9.0 ) continue;

        int newHeight = ( int ) Math.Floor( imageWidth / scale );
        PictureBox pictureBox = new PictureBox() {
          Image = picture,
          SizeMode = PictureBoxSizeMode.StretchImage,
          ClientSize = new Size( imageWidth, newHeight )
        };

        pictureBox.MouseClick += ( object sender, MouseEventArgs e ) => {
          if ( e.Button == MouseButtons.Right ) {
            saveFileDialog1.FileName = Path.GetFileName( picturePath );
            contextMenu.Tag = picturePath;
            contextMenu.Show( pictureBox, e.Location );
          }
        };

        pictureBox.MouseDoubleClick += ( object sender, MouseEventArgs e ) => {
          OpenFile( picturePath );
        };

        TableLayoutPanel panel = new TableLayoutPanel() {
          AutoSize = true,
          AutoSizeMode = AutoSizeMode.GrowAndShrink,
          Margin = new Padding {
            Left = 0, Right = 0,
            Top = 1, Bottom = 1
          },
          RowCount = 2, ColumnCount = 1,
        };

        string pictureSize = $"{ image.Width } x { image.Height }";
        string pictureDate = File.GetCreationTime( picturePath ).ToString( "yyyy/MM/dd" );

        Label label = new Label() {
          Text = $"{ tag }; { pictureSize } - { pictureDate }",
          TextAlign = ContentAlignment.MiddleCenter,
          Anchor = AnchorStyles.None,
          AutoSize = true
        };
        label.Size = new Size( label.Width, 10 );
        label.Font = new Font( "Consolas", label.Font.Size, FontStyle.Regular );

        panel.Controls.Add( pictureBox );
        panel.Controls.Add( label );

        imagePanel.Controls.Add( panel );
      }
    }

    private void Form1_Load( object sender, EventArgs e ) {
      if ( Directory.Exists( lockscreenPicPath ) ) {
        string[] lockscreenPics = Directory.GetFiles( lockscreenPicPath, "*.*", SearchOption.AllDirectories );
        DrawImages( lockscreenPics, tag: "Lock Screen", imageWidth: 400 );
      }
      if ( Directory.Exists( desktopPicPath ) ) {
        string[] desktopPics = Directory.GetFiles( desktopPicPath , "*.*", SearchOption.AllDirectories );
        DrawImages( desktopPics, tag: "Desktop", imageWidth: 400 );
      }

      this.Size = new Size(
        width: 425 * 4,
        height: 285 * imagePanel.Controls.Count / 4
      );
    }
  }
}
