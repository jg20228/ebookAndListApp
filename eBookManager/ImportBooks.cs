using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using eBookManager.Engine;
using static eBookManager.Helper.ExtensionMethods;
using static System.Math;

namespace eBookManager
{
    public partial class ImportBooks : Form
    {
        private string _jsonPath; //전차책 정보 저장하는 경로
        private List<StorageSpace> spaces;
        private enum StorageSpaceSelection { New = -9999, NoSelection = -1}
        public ImportBooks()
        {
            InitializeComponent();
            _jsonPath = Path.Combine(Application.StartupPath, "bookData.txt");
            spaces = spaces.ReadFromDataStore(_jsonPath);
        }

        private HashSet<string> AllowedExtensions => new HashSet<string>(StringComparer.InvariantCultureIgnoreCase)
        {
            ".doc",
            ".docx",
            ".pdf",
            ".epub"
        };
        // 위와 아래는 똑같은 코드이다.
        //private HashSet<string> AllowedExtensions
        //{
        //    get
        //    {
        //        return new HashSet<string>
        //            (StringComparer.InvariantCultureIgnoreCase)
        //        {
        //            ".doc",
        //            ".docx",
        //            ".pdf",
        //            ".epub"
        //        };
        //    }
        //}
        private enum Extension { doc = 0, docx = 1, pdf = 2, epub = 3 }


        public void PopulateBookList(string paramDir, TreeNode paramNode)
        {
            //재귀함수

            //TreeView컨트롤을 선택한 위치의 파일과 폴더로 채우는 것
            DirectoryInfo dir = new DirectoryInfo(paramDir);
            foreach(DirectoryInfo dirInfo in dir.GetDirectories())
            {
                TreeNode node = new TreeNode(dirInfo.Name);
                node.ImageIndex = 4;
                node.SelectedImageIndex = 5;

                if (paramNode != null)
                    paramNode.Nodes.Add(node);
                else
                    tvFoundBooks.Nodes.Add(node);
                PopulateBookList(dirInfo.FullName, node);
            }
            foreach(FileInfo fileInfo in dir.GetFiles().Where(x => AllowedExtensions.Contains(x.Extension)).ToList())
            {
                TreeNode node = new TreeNode(fileInfo.Name);
                node.Tag = fileInfo.FullName;
                int iconIndex = Enum.Parse(typeof(Extension),
                    fileInfo.Extension.TrimStart('.'), true).GetHashCode();
                node.ImageIndex = iconIndex;
                node.SelectedImageIndex = iconIndex;
                if (paramNode != null)
                    paramNode.Nodes.Add(node);
                else
                    tvFoundBooks.Nodes.Add(node);
            }
        }



        private void btnSelectSourceFolder_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "Select the location of your eBooks and documents";

                DialogResult dlgResult = fbd.ShowDialog();
                if(dlgResult == DialogResult.OK)
                {
                    tvFoundBooks.Nodes.Clear();
                    tvFoundBooks.ImageList = tvImages;

                    string path = fbd.SelectedPath;
                    DirectoryInfo di = new DirectoryInfo(path);
                    TreeNode root = new TreeNode(di.Name);
                    root.ImageIndex = 4;
                    root.SelectedImageIndex = 5;
                    tvFoundBooks.Nodes.Add(root);
                    PopulateBookList(di.FullName, root);
                    tvFoundBooks.Sort();

                    root.Expand();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tvFoundBooks_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //상세정보에 값 전달
            DocumentEngine engine = new DocumentEngine();
            string path = e.Node.Tag?.ToString() ?? "";

            if(File.Exists(path))
            {
                var (dateCreated, dateLastAccessed, fileName, fileExtention, fileLength, hasError)
                    = engine.GetFileProperties(e.Node.Tag.ToString());
                if (!hasError)
                {
                    txtFileName.Text = fileName;
                    txtExtension.Text = fileExtention;
                    dtCreated.Value = dateCreated;
                    dtLastAccessed.Value = dateLastAccessed;
                    txtFilePath.Text = e.Node.Tag.ToString();
                    txtFileSize.Text = $"{Round(fileLength.ToMegabytes(), 2).ToString()}MB";
                }
            }
        }
    }
}
