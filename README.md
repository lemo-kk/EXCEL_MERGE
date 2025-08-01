# ExcelToMerge

医疗数据处理工具，用于导入Excel/CSV数据，进行SQL转换，并支持调度任务。

## 项目概述

ExcelToMerge是一个专为医疗机构设计的数据处理工具，主要功能包括：

1. 导入Excel/CSV数据到本地SQLite数据库
2. 使用SQL语句进行数据转换和清洗
3. 配置和执行调度任务，实现批量处理
4. 导出处理后的数据

## 技术栈

- C# .NET Framework 4.5.1
- WinForms (使用MetroModernUI美化界面)
- SQLite数据库
- EPPlus (Excel操作)
- CsvHelper (CSV操作)
- Dapper (数据库访问)

## 已完成任务/功能

### 基础架构
- [x] 项目结构设计
- [x] 数据库设计
- [x] 配置文件设置

### 核心服务
- [x] DatabaseService (数据库服务)
- [x] ImportService (导入服务)
- [x] ConvertService (转换服务)
- [x] ScheduleService (调度服务)

### 工具类
- [x] SqliteHelper (SQLite操作助手)
- [x] ExcelHelper (Excel/CSV操作助手)
- [x] ProgressReporter (进度报告工具)

### 用户界面
- [x] MainForm (主窗体)
- [x] ImportForm (导入窗体)
- [x] ConvertForm (转换窗体)
- [x] ScheduleForm (调度窗体)
- [x] SqlTemplateForm (SQL模板管理窗体)
- [x] BatchExecutionForm (批量执行窗体)
- [x] TaskSelectionForm (任务选择窗体)
- [x] ExecutionLogForm (执行日志窗体)

### 功能模块
- [x] Excel/CSV文件导入
- [x] 表结构自动推断
- [x] 数据导入进度显示
- [x] SQL编辑器（语法高亮）
- [x] 数据库表结构树形展示
- [x] SQL模板管理
- [x] 查询结果导出
- [x] 批量任务执行
- [x] 调度任务配置
- [x] 调度任务执行
- [x] 执行日志记录与查看

## 待完成任务/功能

### 用户界面完善
- [ ] 优化UI交互体验和视觉效果

### 功能完善
- [ ] 完善调度任务的定时执行功能

### 性能优化
- [ ] 针对大数据量（几十万级）进行优化
- [ ] 实现批量处理和异步操作
- [ ] 优化数据库查询和Excel操作性能

### 测试和调试
- [ ] 单元测试和集成测试
- [ ] 性能测试和压力测试
- [ ] 错误处理和异常捕获完善

### 打包和部署
- [ ] 创建安装程序
- [ ] 配置自动更新功能
- [ ] 准备发布版本

## 使用说明

### 导入模块
1. 选择Excel或CSV文件
2. 设置导入选项（是否包含表头等）
3. 点击导入按钮开始导入
4. 导入完成后可在转换模块中查看和操作数据

### 转换模块
1. 在左侧树形结构中浏览数据库表
2. 在SQL编辑器中编写查询语句
3. 执行查询并查看结果
4. 可将结果导出为Excel或CSV
5. 可保存常用SQL为模板

### 调度模块
1. 创建调度任务
2. 选择要执行的转换任务
3. 设置执行顺序和输出路径
4. 手动执行或设置定时执行
5. 查看执行日志和结果

## 开发环境
- Visual Studio 2022
- .NET Framework 4.5.1
- Windows 10/11 