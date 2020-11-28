namespace eBookManager
{
    partial class ImportBooks
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ImageList imageList1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportBooks));
            imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageList1
            // 
            imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            imageList1.TransparentColor = System.Drawing.Color.Transparent;
            imageList1.Images.SetKeyName(0, "docx16.png");
            imageList1.Images.SetKeyName(1, "docxx16.png");
            imageList1.Images.SetKeyName(2, "epubx16.png");
            imageList1.Images.SetKeyName(3, "folder_exp_x16.png");
            imageList1.Images.SetKeyName(4, "folder-close-x16.png");
            imageList1.Images.SetKeyName(5, "pdfx16.png");
            // 
            // ImportBooks
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "ImportBooks";
            this.ResumeLayout(false);

        }

        #endregion

    }
}

