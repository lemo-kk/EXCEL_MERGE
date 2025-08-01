using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using ExcelToMerge.Models;
using ExcelToMerge.Utils;

namespace ExcelToMerge
{
    /// <summary>
    /// Excel数据处理类，用于医院数据清洗与转换
    /// </summary>
    public class ExcelDataProcessor
    {
        // 科室ID字典，用于存储科室名称和ID的映射关系
        private Dictionary<string, int> _departmentIds = new Dictionary<string, int>();
        
        // 科室类型字典，用于存储科室名称和类型(门诊/病房)的映射关系
        private Dictionary<string, string> _departmentTypes = new Dictionary<string, string>();
        
        // 科室三层结构字典，格式: {科室名称: {上级ID, 本级ID, 类型}}
        private Dictionary<string, (int ParentId, int Id, string Type)> _departmentStructure = 
            new Dictionary<string, (int ParentId, int Id, string Type)>();
            
        // 门诊病人数量字典，格式: {科室名称: 人数}
        private Dictionary<string, int> _outpatientCounts = new Dictionary<string, int>();
        
        // 住院病人数量字典，格式: {科室名称: 人数}
        private Dictionary<string, int> _inpatientCounts = new Dictionary<string, int>();
        
        // 科室总金额字典，格式: {科室名称: 总金额}
        private Dictionary<string, decimal> _departmentAmounts = new Dictionary<string, decimal>();
        
        /// <summary>
        /// 处理明细表数据
        /// </summary>
        /// <param name="filePath">明细表文件路径</param>
        /// <returns>是否处理成功</returns>
        public bool ProcessDetailData(string filePath)
        {
            try
            {
                Console.WriteLine($"正在处理明细表: {filePath}");
                
                // 使用EPPlus读取Excel文件
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        Console.WriteLine("无法读取工作表");
                        return false;
                    }
                    
                    // 获取行数
                    int rowCount = worksheet.Dimension.End.Row;
                    
                    // 从第2行开始读取数据(跳过表头)
                    for (int row = 2; row <= rowCount; row++)
                    {
                        // 读取科室名称(B列)
                        string departmentName = worksheet.Cells[row, 2].Text;
                        if (string.IsNullOrEmpty(departmentName))
                            continue;
                            
                        // 读取药品名称(E列)
                        string medicineName = worksheet.Cells[row, 5].Text;
                        
                        // 读取数量(M列)
                        decimal quantity = 0;
                        decimal.TryParse(worksheet.Cells[row, 13].Text, out quantity);
                        
                        // 读取总金额(O列)
                        decimal amount = 0;
                        decimal.TryParse(worksheet.Cells[row, 15].Text, out amount);
                        
                        // 确定科室类型(门诊/病房)
                        string departmentType = DetermineDepartmentType(departmentName);
                        
                        // 生成科室ID
                        int departmentId = GetOrCreateDepartmentId(departmentName);
                        
                        // 更新科室类型字典
                        _departmentTypes[departmentName] = departmentType;
                        
                        // 累加科室总金额
                        if (_departmentAmounts.ContainsKey(departmentName))
                        {
                            _departmentAmounts[departmentName] += amount;
                        }
                        else
                        {
                            _departmentAmounts[departmentName] = amount;
                        }
                    }
                    
                    Console.WriteLine($"明细表处理完成，共处理 {rowCount - 1} 行数据");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"处理明细表时出错: {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// 处理门诊病人数据
        /// </summary>
        /// <param name="filePath">门诊病人表文件路径</param>
        /// <returns>是否处理成功</returns>
        public bool ProcessOutpatientData(string filePath)
        {
            try
            {
                Console.WriteLine($"正在处理门诊病人表: {filePath}");
                
                // 使用EPPlus读取Excel文件
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        Console.WriteLine("无法读取工作表");
                        return false;
                    }
                    
                    // 获取行数
                    int rowCount = worksheet.Dimension.End.Row;
                    
                    // 从第2行开始读取数据(跳过表头)
                    for (int row = 2; row <= rowCount; row++)
                    {
                        // 读取科室名称
                        string departmentName = worksheet.Cells[row, 1].Text;
                        if (string.IsNullOrEmpty(departmentName))
                            continue;
                            
                        // 读取病人数量
                        int count = 0;
                        int.TryParse(worksheet.Cells[row, 2].Text, out count);
                        
                        // 更新门诊病人数量字典
                        _outpatientCounts[departmentName] = count;
                    }
                    
                    Console.WriteLine($"门诊病人表处理完成，共处理 {rowCount - 1} 行数据");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"处理门诊病人表时出错: {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// 处理住院病人数据
        /// </summary>
        /// <param name="filePath">住院病人表文件路径</param>
        /// <returns>是否处理成功</returns>
        public bool ProcessInpatientData(string filePath)
        {
            try
            {
                Console.WriteLine($"正在处理住院病人表: {filePath}");
                
                // 使用EPPlus读取Excel文件
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        Console.WriteLine("无法读取工作表");
                        return false;
                    }
                    
                    // 获取行数
                    int rowCount = worksheet.Dimension.End.Row;
                    
                    // 从第2行开始读取数据(跳过表头)
                    for (int row = 2; row <= rowCount; row++)
                    {
                        // 读取科室名称
                        string departmentName = worksheet.Cells[row, 1].Text;
                        if (string.IsNullOrEmpty(departmentName))
                            continue;
                            
                        // 读取病人数量
                        int count = 0;
                        int.TryParse(worksheet.Cells[row, 2].Text, out count);
                        
                        // 更新住院病人数量字典
                        _inpatientCounts[departmentName] = count;
                    }
                    
                    Console.WriteLine($"住院病人表处理完成，共处理 {rowCount - 1} 行数据");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"处理住院病人表时出错: {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// 构建科室三层结构
        /// </summary>
        /// <returns>是否构建成功</returns>
        public bool BuildDepartmentStructure()
        {
            try
            {
                Console.WriteLine("正在构建科室三层结构...");
                
                // 创建全院根节点
                int hospitalId = 1;
                
                // 为每个科室创建二级结构
                int departmentIdCounter = 2; // 从2开始，1已用于全院
                
                foreach (var departmentName in _departmentTypes.Keys)
                {
                    // 科室ID
                    int departmentId = departmentIdCounter++;
                    
                    // 科室类型(门诊/病房)
                    string departmentType = _departmentTypes[departmentName];
                    
                    // 添加到科室结构字典
                    _departmentStructure[departmentName] = (hospitalId, departmentId, departmentType);
                }
                
                Console.WriteLine($"科室三层结构构建完成，共 {_departmentStructure.Count} 个科室");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"构建科室三层结构时出错: {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// 生成目标Excel文件
        /// </summary>
        /// <param name="filePath">目标文件路径</param>
        /// <returns>是否生成成功</returns>
        public bool GenerateTargetExcel(string filePath)
        {
            try
            {
                Console.WriteLine($"正在生成目标Excel文件: {filePath}");
                
                // 创建新的Excel包
                using (var package = new ExcelPackage())
                {
                    // 创建药品使用信息sheet
                    var medicineSheet = package.Workbook.Worksheets.Add("药品使用信息");
                    
                    // 添加表头
                    medicineSheet.Cells[1, 1].Value = "序号";
                    medicineSheet.Cells[1, 2].Value = "医院药品名称";
                    medicineSheet.Cells[1, 3].Value = "科室ID";
                    medicineSheet.Cells[1, 4].Value = "科室名称";
                    medicineSheet.Cells[1, 5].Value = "门诊/病房";
                    medicineSheet.Cells[1, 6].Value = "药品规格";
                    medicineSheet.Cells[1, 7].Value = "使用量";
                    medicineSheet.Cells[1, 8].Value = "使用金额";
                    
                    // 创建科室基础信息sheet
                    var departmentSheet = package.Workbook.Worksheets.Add("科室基础信息");
                    
                    // 添加表头
                    departmentSheet.Cells[1, 1].Value = "ID";
                    departmentSheet.Cells[1, 2].Value = "上级ID";
                    departmentSheet.Cells[1, 3].Value = "科室名称";
                    departmentSheet.Cells[1, 4].Value = "科室类型";
                    
                    // 创建药费及人次sheet
                    var statisticsSheet = package.Workbook.Worksheets.Add("药费及人次");
                    
                    // 添加表头
                    statisticsSheet.Cells[1, 1].Value = "科室ID";
                    statisticsSheet.Cells[1, 2].Value = "科室名称";
                    statisticsSheet.Cells[1, 3].Value = "门诊人次";
                    statisticsSheet.Cells[1, 4].Value = "住院人次";
                    statisticsSheet.Cells[1, 5].Value = "科室总金额";
                    
                    // 填充科室基础信息sheet
                    // 添加全院根节点
                    departmentSheet.Cells[2, 1].Value = 1; // ID
                    departmentSheet.Cells[2, 2].Value = 0; // 上级ID
                    departmentSheet.Cells[2, 3].Value = "全院"; // 科室名称
                    departmentSheet.Cells[2, 4].Value = "医院"; // 科室类型
                    
                    // 添加各科室
                    int departmentRow = 3;
                    foreach (var entry in _departmentStructure)
                    {
                        string departmentName = entry.Key;
                        var structure = entry.Value;
                        
                        departmentSheet.Cells[departmentRow, 1].Value = structure.Id; // ID
                        departmentSheet.Cells[departmentRow, 2].Value = structure.ParentId; // 上级ID
                        departmentSheet.Cells[departmentRow, 3].Value = departmentName; // 科室名称
                        departmentSheet.Cells[departmentRow, 4].Value = structure.Type; // 科室类型
                        
                        departmentRow++;
                    }
                    
                    // 填充药费及人次sheet
                    int statisticsRow = 2;
                    foreach (var entry in _departmentStructure)
                    {
                        string departmentName = entry.Key;
                        var structure = entry.Value;
                        
                        statisticsSheet.Cells[statisticsRow, 1].Value = structure.Id; // 科室ID
                        statisticsSheet.Cells[statisticsRow, 2].Value = departmentName; // 科室名称
                        
                        // 门诊人次
                        if (_outpatientCounts.ContainsKey(departmentName))
                        {
                            statisticsSheet.Cells[statisticsRow, 3].Value = _outpatientCounts[departmentName];
                        }
                        
                        // 住院人次
                        if (_inpatientCounts.ContainsKey(departmentName))
                        {
                            statisticsSheet.Cells[statisticsRow, 4].Value = _inpatientCounts[departmentName];
                        }
                        
                        // 科室总金额
                        if (_departmentAmounts.ContainsKey(departmentName))
                        {
                            statisticsSheet.Cells[statisticsRow, 5].Value = _departmentAmounts[departmentName];
                        }
                        
                        statisticsRow++;
                    }
                    
                    // 保存Excel文件
                    package.SaveAs(new FileInfo(filePath));
                    
                    Console.WriteLine($"目标Excel文件生成成功: {filePath}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"生成目标Excel文件时出错: {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// 根据科室名称判断科室类型(门诊/病房)
        /// </summary>
        /// <param name="departmentName">科室名称</param>
        /// <returns>科室类型</returns>
        private string DetermineDepartmentType(string departmentName)
        {
            // 根据科室名称判断类型
            // 这里使用简单的规则，实际应用中可能需要更复杂的判断逻辑
            if (departmentName.Contains("门诊") || 
                departmentName.Contains("急诊") || 
                departmentName.Contains("诊室") ||
                departmentName.Contains("诊所"))
            {
                return "门诊";
            }
            else
            {
                return "病房";
            }
        }
        
        /// <summary>
        /// 获取或创建科室ID
        /// </summary>
        /// <param name="departmentName">科室名称</param>
        /// <returns>科室ID</returns>
        private int GetOrCreateDepartmentId(string departmentName)
        {
            if (_departmentIds.ContainsKey(departmentName))
            {
                return _departmentIds[departmentName];
            }
            else
            {
                int newId = _departmentIds.Count + 2; // +2是因为ID=1已用于全院
                _departmentIds[departmentName] = newId;
                return newId;
            }
        }
        
        /// <summary>
        /// 执行完整的数据处理流程
        /// </summary>
        /// <param name="detailFilePath">明细表文件路径</param>
        /// <param name="outpatientFilePath">门诊病人表文件路径</param>
        /// <param name="inpatientFilePath">住院病人表文件路径</param>
        /// <param name="targetFilePath">目标文件路径</param>
        /// <returns>是否处理成功</returns>
        public bool ProcessAll(string detailFilePath, string outpatientFilePath, string inpatientFilePath, string targetFilePath)
        {
            // 处理明细表
            if (!ProcessDetailData(detailFilePath))
                return false;
                
            // 处理门诊病人表
            if (!ProcessOutpatientData(outpatientFilePath))
                return false;
                
            // 处理住院病人表
            if (!ProcessInpatientData(inpatientFilePath))
                return false;
                
            // 构建科室三层结构
            if (!BuildDepartmentStructure())
                return false;
                
            // 生成目标Excel文件
            if (!GenerateTargetExcel(targetFilePath))
                return false;
                
            return true;
        }
    }
} 