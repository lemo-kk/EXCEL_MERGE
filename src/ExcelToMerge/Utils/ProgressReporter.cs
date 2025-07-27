using System;
using System.Threading;

namespace ExcelToMerge.Utils
{
    /// <summary>
    /// 进度报告辅助类
    /// </summary>
    public class ProgressReporter : IDisposable
    {
        private readonly IProgress<ProgressInfo> _progress;
        private readonly Timer _timer;
        private readonly int _totalItems;
        private int _processedItems;
        private bool _isCompleted;
        private string _currentStatus;
        private Exception _error;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="progress">进度回调</param>
        /// <param name="totalItems">总项目数</param>
        /// <param name="reportIntervalMs">报告间隔（毫秒）</param>
        public ProgressReporter(IProgress<ProgressInfo> progress, int totalItems, int reportIntervalMs = 100)
        {
            _progress = progress ?? throw new ArgumentNullException(nameof(progress));
            _totalItems = totalItems > 0 ? totalItems : 1; // 防止除零错误
            _processedItems = 0;
            _isCompleted = false;
            _currentStatus = "准备中...";
            
            // 创建定时器，定期报告进度
            _timer = new Timer(ReportProgress, null, 0, reportIntervalMs);
        }
        
        /// <summary>
        /// 更新处理项目数
        /// </summary>
        /// <param name="processedItems">已处理项目数</param>
        public void Update(int processedItems)
        {
            Interlocked.Exchange(ref _processedItems, processedItems);
        }
        
        /// <summary>
        /// 增加处理项目数
        /// </summary>
        /// <param name="increment">增加数量</param>
        public void Increment(int increment = 1)
        {
            Interlocked.Add(ref _processedItems, increment);
        }
        
        /// <summary>
        /// 设置当前状态
        /// </summary>
        /// <param name="status">状态描述</param>
        public void SetStatus(string status)
        {
            _currentStatus = status ?? string.Empty;
            ReportProgress(null);
        }
        
        /// <summary>
        /// 设置错误
        /// </summary>
        /// <param name="error">错误信息</param>
        public void SetError(Exception error)
        {
            _error = error;
            ReportProgress(null);
        }
        
        /// <summary>
        /// 完成进度报告
        /// </summary>
        public void Complete()
        {
            _isCompleted = true;
            _processedItems = _totalItems;
            ReportProgress(null);
        }
        
        /// <summary>
        /// 报告进度
        /// </summary>
        /// <param name="state">状态对象</param>
        private void ReportProgress(object state)
        {
            // 计算进度百分比
            int processedItems = Math.Min(_processedItems, _totalItems);
            double percentage = (double)processedItems / _totalItems * 100;
            
            // 创建进度信息
            var progressInfo = new ProgressInfo
            {
                Percentage = percentage,
                ProcessedItems = processedItems,
                TotalItems = _totalItems,
                Status = _currentStatus,
                IsCompleted = _isCompleted,
                Error = _error
            };
            
            // 报告进度
            _progress.Report(progressInfo);
        }
        
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
    
    /// <summary>
    /// 进度信息类
    /// </summary>
    public class ProgressInfo
    {
        /// <summary>
        /// 进度百分比
        /// </summary>
        public double Percentage { get; set; }
        
        /// <summary>
        /// 已处理项目数
        /// </summary>
        public int ProcessedItems { get; set; }
        
        /// <summary>
        /// 总项目数
        /// </summary>
        public int TotalItems { get; set; }
        
        /// <summary>
        /// 当前状态
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// 是否已完成
        /// </summary>
        public bool IsCompleted { get; set; }
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public Exception Error { get; set; }
    }
} 