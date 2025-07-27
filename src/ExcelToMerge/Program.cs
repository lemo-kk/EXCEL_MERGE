using System;
using System.IO;
using System.Windows.Forms;
using ExcelToMerge.UI;

namespace ExcelToMerge
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                // 创建必要的目录
                CreateDirectories();
                
                // 运行测试程序
                // TestProgram.Test();
                
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"应用程序启动失败: {ex.Message}\n\n{ex.StackTrace}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 创建必要的目录
        /// </summary>
        private static void CreateDirectories()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            
            // 创建数据目录
            string dataDir = Path.Combine(baseDir, "Data");
            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }
            
            // 创建备份目录
            string backupDir = Path.Combine(baseDir, "Backup");
            if (!Directory.Exists(backupDir))
            {
                Directory.CreateDirectory(backupDir);
            }
            
            // 创建导出目录
            string exportDir = Path.Combine(baseDir, "Export");
            if (!Directory.Exists(exportDir))
            {
                Directory.CreateDirectory(exportDir);
            }
        }
    }
} 