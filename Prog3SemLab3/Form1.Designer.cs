
namespace Prog3SemLab3
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.xyPointsGridView = new System.Windows.Forms.DataGridView();
            this.xValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.launchBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.xyPointsGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // xyPointsGridView
            // 
            this.xyPointsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.xyPointsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.xValue,
            this.yValue});
            this.xyPointsGridView.Location = new System.Drawing.Point(12, 42);
            this.xyPointsGridView.Name = "xyPointsGridView";
            this.xyPointsGridView.Size = new System.Drawing.Size(193, 150);
            this.xyPointsGridView.TabIndex = 0;
            // 
            // xValue
            // 
            this.xValue.HeaderText = "x";
            this.xValue.Name = "xValue";
            this.xValue.Width = 75;
            // 
            // yValue
            // 
            this.yValue.HeaderText = "y";
            this.yValue.Name = "yValue";
            this.yValue.Width = 75;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.launchBtn,
            this.importToolStripMenuItem,
            this.closeBtn});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1058, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // launchBtn
            // 
            this.launchBtn.Name = "launchBtn";
            this.launchBtn.Size = new System.Drawing.Size(80, 20);
            this.launchBtn.Text = "Рассчитать";
            this.launchBtn.Click += new System.EventHandler(this.LaunchBtn_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excelToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(107, 20);
            this.importToolStripMenuItem.Text = "Импортировать";
            // 
            // excelToolStripMenuItem
            // 
            this.excelToolStripMenuItem.Name = "excelToolStripMenuItem";
            this.excelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.excelToolStripMenuItem.Text = "Excel";
            this.excelToolStripMenuItem.Click += new System.EventHandler(this.excel_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(54, 20);
            this.closeBtn.Text = "Выход";
            this.closeBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // chart
            // 
            chartArea2.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea2);
            this.chart.Location = new System.Drawing.Point(224, 42);
            this.chart.Name = "chart";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series4.Name = "Points";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Name = "Line";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Name = "Sqr";
            this.chart.Series.Add(series4);
            this.chart.Series.Add(series5);
            this.chart.Series.Add(series6);
            this.chart.Size = new System.Drawing.Size(822, 300);
            this.chart.TabIndex = 2;
            this.chart.Text = "chart";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 372);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.xyPointsGridView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.xyPointsGridView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView xyPointsGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn xValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn yValue;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem launchBtn;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeBtn;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
    }
}

