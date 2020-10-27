using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Linq;
using RHApplicationLib.Core;

namespace DoctorApplication
{
    public partial class GraphControl : UserControl
    {
        private decimal[] _dataSource;
        public decimal[] DataSource
        {
            get
            {
                return this._dataSource;
            }
            set
            {
                this._dataSource = value;

                this.DrawToBuffer(this.bGraphics.Graphics);
                this.Invalidate();
            }
        }

        private int _pointsToShow = 10;
        public int PointsToShow
        {
            get
            {
                return this._pointsToShow;
            }
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(this.PointsToShow), $"value must be higher than 0");
                this._pointsToShow = value;
            }
        }

        private float _lineWidth = 1f;
        public float LineWidth
        {
            get
            {
                return this._lineWidth;
            }
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(this.LineWidth), $"value must be higher than 0");
                this._lineWidth = value;
            }
        }

        public GraphControlSizeMode GraphSizeMode { get; set; } = GraphControlSizeMode.Scroll;

        public decimal? MaxValue { get; set; } = null;
        public decimal? MinValue { get; set; } = null;

        private BufferedGraphicsContext bgContext;
        private BufferedGraphics bGraphics;

        private bool disposed = false;

        public GraphControl()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            this.Resize += GraphControl_Resize;

            this.bgContext = BufferedGraphicsManager.Current;
            this.bGraphics = this.bgContext.Allocate(this.CreateGraphics(), new Rectangle(0, 0, this.Width, this.Height));

            this.DrawToBuffer(this.bGraphics.Graphics);
        }

        private void GraphControl_Resize(object sender, EventArgs e)
        {
            this.bGraphics?.Dispose();
            this.bGraphics = this.bgContext.Allocate(this.CreateGraphics(), new Rectangle(0, 0, this.Width, this.Height));

            this.DrawToBuffer(this.bGraphics.Graphics);
            this.Invalidate();
        }

        private void GraphControl_Paint(object sender, PaintEventArgs e)
        {
            this.bGraphics.Render(e.Graphics);
        }

        private void DrawToBuffer(Graphics g)
        {
            if(!this.disposed)
            {
                Brush graphBrush = new SolidBrush(this.ForeColor);
                Pen graphPen = new Pen(graphBrush, this.LineWidth);

                // clear background
                g.Clear(this.BackColor);

                if (this.DataSource != null && this.DataSource.Length > 0)
                {
                    decimal minValue = this.MinValue.HasValue ? this.MinValue.Value : this.DataSource.Aggregate(decimal.MaxValue, (accu, elem) => Math.Min(accu, elem));
                    decimal maxValue = this.MaxValue.HasValue ? this.MaxValue.Value : this.DataSource.Aggregate(decimal.MinValue, (accu, elem) => Math.Max(accu, elem));

                    IEnumerable<decimal> valuesEnum;
                    switch(this.GraphSizeMode)
                    {
                        case GraphControlSizeMode.Stretch:
                            {
                                valuesEnum = this.DataSource.AsEnumerable();
                                break;
                            }
                        case GraphControlSizeMode.None:
                            {
                                valuesEnum = this.DataSource.Take(this.PointsToShow);
                                break;
                            }
                        case GraphControlSizeMode.Scroll:
                        default:
                            {
                                valuesEnum = this.DataSource.TakeLast(this.PointsToShow);
                                break;
                            }
                    }

                    List<float> valuesMapped;
                    if (minValue != maxValue)
                    {
                        valuesMapped = valuesEnum.Select(elem => (float)Utility.Map(elem, minValue, maxValue, 0, this.Height))
                            .ToList();
                    }
                    else
                    {
                        valuesMapped = valuesEnum.Select(elem => this.Height / 2f)
                            .ToList();
                    }


                    float pointDistance = (float)this.Width / ((float)this.PointsToShow - 1f);

                    PointF current;
                    PointF previous = new PointF(0f, this.Height - valuesMapped[0]);
                    if (valuesMapped.Count == 1)
                    {
                        g.FillEllipse(graphBrush, previous.X, previous.Y, this.LineWidth, this.LineWidth);
                    }
                    else
                    {
                        for (int i = 1; i < valuesMapped.Count; i++)
                        {
                            current = new PointF(i * pointDistance, this.Height - valuesMapped[i]);
                            g.DrawLine(graphPen, previous, current);

                            previous = current;
                        }
                    }
                }
            }
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.disposed = true;

                if((components != null))
                {
                    components.Dispose();
                }
                
                this.bGraphics?.Dispose();
                this.bgContext?.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public enum GraphControlSizeMode
    {
        // Automatically scrolls with the data (default)
        Scroll,
        // Stretches the data so it will always fill the whole with
        Stretch,
        // Extends the graph untill PointsToShow has been reached
        None
    }
}
