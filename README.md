# Excel数据处理转换工具

## 项目简介
Excel数据处理转换工具是一个专为医疗机构设计的数据处理应用程序。该工具可以帮助用户导入医院HIS系统导出的Excel数据，进行合并、清洗和转换，最终按照标准格式导出，提高数据处理效率。

## 功能特点
1. **Excel导入功能**
   - 支持导入Excel或CSV格式数据
   - 导入过程显示进度条
   - 数据自动存储到本地SQLite数据库
   - 智能处理表名冲突（追加或重建）

2. **数据转换功能**
   - 配置多个转换作业
   - 自定义SQL转换语句
   - 支持多种导出格式（Excel/CSV）
   - 可视化查看本地数据库表和字段

3. **调度功能**
   - 配置多个转换任务
   - 一键执行并导出数据
   - 批量处理大数据量（支持几十万级数据处理）

## 技术栈
- 编程语言：C#
- 框架：.NET Framework 4.5.1
- 数据库：SQLite
- Excel操作库：EPPlus/NPOI
- 用户界面：WinForms（科技感UI设计）
- UI框架：MetroModernUI

## 项目结构
```
ExcelToMerge_v1/
├── src/                       # 源代码目录
│   ├── ExcelToMerge/          # 主项目目录
│   │   ├── Models/            # 数据模型
│   │   │   ├── ImportTask.cs  # 导入任务模型
│   │   │   ├── ConvertTask.cs # 转换任务模型
│   │   │   └── ScheduleTask.cs# 调度任务模型
│   │   ├── Services/          # 业务逻辑服务
│   │   │   ├── ImportService.cs    # 导入服务
│   │   │   ├── ConvertService.cs   # 转换服务
│   │   │   ├── ScheduleService.cs  # 调度服务
│   │   │   └── DatabaseService.cs  # 数据库服务
│   │   ├── Utils/             # 工具类
│   │   │   ├── ExcelHelper.cs      # Excel操作辅助类
│   │   │   ├── SqliteHelper.cs     # SQLite操作辅助类
│   │   │   └── ProgressReporter.cs # 进度报告辅助类
│   │   └── UI/                # 用户界面
│   │       ├── MainForm.cs         # 主窗体
│   │       ├── ImportForm.cs       # 导入窗体
│   │       ├── ConvertForm.cs      # 转换窗体
│   │       └── ScheduleForm.cs     # 调度窗体
├── tests/                     # 测试代码目录
├── docs/                      # 文档目录
│   ├── UI设计/                # UI设计文档
│   └── 数据库设计/            # 数据库设计文档
└── README.md                  # 项目说明文档
```

## 已完成任务/功能
1. **项目基础架构**
   - 创建解决方案和项目结构
   - 配置.NET Framework 4.5.1环境
   - 添加必要的NuGet包引用（EPPlus, SQLite, Dapper, CsvHelper, MetroModernUI）

2. **数据模型设计**
   - 完成ImportTask, ConvertTask, ScheduleTask等核心模型类
   - 完成SqlTemplate模型类（SQL模板管理）

3. **业务逻辑服务**
   - 实现ImportService（Excel/CSV导入服务）
   - 实现ConvertService（数据转换服务）
   - 实现ScheduleService（调度服务）
   - 实现DatabaseService（数据库访问服务）

4. **工具类实现**
   - 完成ExcelHelper（Excel操作辅助类）
   - 完成SqliteHelper（SQLite操作辅助类）
   - 完成ProgressReporter（进度报告辅助类）

5. **用户界面开发**
   - 完成MainForm（主窗体）
   - 完成ImportForm（导入窗体）
   - 完成ConvertForm（转换窗体）的基本功能
     - 数据库表和字段的可视化浏览（带图标和主键标识）
     - SQL编辑器（带语法高亮）
     - 表右键菜单（预览数据、生成SELECT语句、生成COUNT语句）
     - SQL模板管理功能
     - 批量执行功能
   - 创建ScheduleForm的基本框架

6. **文档编写**
   - 完成UI设计文档
   - 完成数据库设计文档

## 待完成任务/功能
1. **用户界面完善**
   - 完成ScheduleForm（调度窗体）的详细实现
   - 优化UI交互体验和视觉效果

2. **功能完善**
   - 实现调度任务的配置和执行界面

3. **性能优化** 
   - 针对大数据量（几十万级）进行优化
   - 实现批量处理和异步操作
   - 优化数据库查询和Excel操作性能

4. **测试和调试**
   - 单元测试和集成测试
   - 性能测试和压力测试
   - 错误处理和异常捕获完善

5. **打包和部署**
   - 创建安装程序
   - 配置自动更新功能
   - 准备发布版本

## 开发计划
1. **产品页面设计** - 已完成基础部分
   - 设计具有科技感的用户界面
   - 规划各功能模块的界面布局和交互

2. **功能模块开发**
   - 模块1：Excel导入功能 - 已完成
   - 模块2：数据转换功能 - 进行中
   - 模块3：调度功能 - 待开始

3. **性能优化** - 待开始
   - 针对大数据量（几十万级）进行优化
   - 提高导入、转换和导出效率

4. **测试和文档** - 部分完成
   - 功能测试和性能测试 - 待开始
   - 编写用户手册和开发文档 - 部分完成

## 使用说明

### 1. Excel导入功能
- 点击"导入"按钮，选择Excel或CSV文件
- 导入过程中显示进度条
- 导入成功后，数据存储到本地SQLite数据库
- 如数据库中已存在同名表，系统会提示是继续追加数据还是删除原表重建

### 2. 转换功能
- 配置转换作业，包括转换SQL和导出格式
- 查看本地SQLite数据库中的表和字段
- 编写SQL语句进行数据转换和清洗
- 使用SQL模板管理功能保存和复用常用SQL语句
- 批量执行多个转换任务，一次性导出多个结果

### 3. 调度功能
- 配置多个转换任务
- 点击"运行"按钮执行所有任务
- 自动导出处理后的数据

## 开发状态
项目已完成基础架构、导入功能模块和转换功能模块的主要部分，正在进行调度功能模块的开发。

## 性能考虑
- 针对几十万级数据量进行优化
- 采用批量处理和异步操作提高效率
- 优化数据库查询和Excel操作性能 