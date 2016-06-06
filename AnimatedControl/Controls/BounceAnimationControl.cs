using System;
using System.Collections.Generic;
using System.Text;
using CoreGraphics;
using UIKit;
using Foundation;
using System.ComponentModel;
using CoreAnimation;
using System.Drawing;

namespace AnimatedControl.Controls
{
    public class BounceAnimationControl : UIView
    {
        const double DefaultHeartBeatRate = 70;
        const float DefaultMaxScale = 1.2f;
        readonly CGColor _defaultColor = UIColor.Red.CGColor;

        CGPath _path;
        nfloat _shapeWidht = 4.1f;

        public BounceAnimationControl()
        {
            Initialize();
        }

        public BounceAnimationControl(CGRect frame) : base(frame)
        {
            Initialize();
        }

        public BounceAnimationControl(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        protected BounceAnimationControl(NSObjectFlag t) : base(t)
        {
            Initialize();
        }

        protected internal BounceAnimationControl(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        private CGColor _color;
        public CGColor Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                RefreshView();
            }
        }

        private double Duration
        {
            get
            {
                return 60 / HeartBeatRate;
            }
        }

        private double _heartBeatRate;

        public double HeartBeatRate
        {
            get
            {
                if (_heartBeatRate == 0)
                {
                    _heartBeatRate = DefaultHeartBeatRate;
                }
                return _heartBeatRate;
            }
            set
            {
                _heartBeatRate = value;
                RestartAnimation();
            }
        }

        private float _maxScale;
        public float MaxScale
        {
            get
            {
                if (_maxScale == 0)
                {
                    _maxScale = DefaultMaxScale;
                }
                return _maxScale;
            }
            set
            {
                _maxScale = value;
                RestartAnimation();
            }
        }

        public override void Draw(CGRect rect)
        {
            nfloat ratio = rect.Width / _shapeWidht;
            using (CGContext context = UIGraphics.GetCurrentContext())
            {

                context.SetLineWidth(1);
                if (_color == null)
                {
                    _color = _defaultColor;
                }
                context.SetFillColor(_color);
                _path = CreatePath().CGPath;
                context.ScaleCTM(ratio, ratio);
                context.AddPath(_path);
                context.DrawPath(CGPathDrawingMode.Fill);
            }
        }

        private void Initialize()
        {
            this.BackgroundColor = UIColor.Clear;
            StartAnimation();

        }

        private void StartAnimation()
        {
            CABasicAnimation theAnimation = CABasicAnimation.FromKeyPath("transform.scale");
            theAnimation.Duration = Duration;
            theAnimation.RepeatCount = int.MaxValue;
            theAnimation.AutoReverses = true;
            theAnimation.From = NSNumber.FromFloat(1f);
            theAnimation.To = NSNumber.FromFloat(MaxScale);
            theAnimation.TimingFunction = CAMediaTimingFunction.FromControlPoints(0.5f,1.8f,1,1);
            this.Layer.AddAnimation(theAnimation, "animateOpacity");
        }

        private void RestartAnimation()
        {
            InvokeOnMainThread(() =>
            {
                Layer.RemoveAllAnimations();
                StartAnimation();
            });
        }

        private void UIView_AnimationWillEnd()
        {
            return;
        }

        private static UIBezierPath CreatePath()
        {
            UIBezierPath path = new UIBezierPath();

            string pathAsString = @"M 2.5,4 
                                    C 0,2 1,0 2.5,1
                                    C 4,0 5,2 2.5,4 Z";

            string[] tokens = pathAsString.Split(separator: new char[2] { ',', ' ' }, options: StringSplitOptions.RemoveEmptyEntries);
            int i = 0;
            while (i < tokens.Length)
            {
                string token = tokens[i++];
                if (token.Equals("M"))
                {
                    float x = float.Parse(tokens[i++]);
                    float y = float.Parse(tokens[i++]);
                    path.MoveTo(new PointF(x, y));
                }
                else if (token.Equals("L"))
                {
                    float x = float.Parse(tokens[i++]);
                    float y = float.Parse(tokens[i++]);
                    path.AddLineTo(new PointF(x, y));
                }
                else if (token.Equals("C"))
                {
                    float x1 = float.Parse(tokens[i++]);
                    float y1 = float.Parse(tokens[i++]);
                    float x2 = float.Parse(tokens[i++]);
                    float y2 = float.Parse(tokens[i++]);
                    float x3 = float.Parse(tokens[i++]);
                    float y3 = float.Parse(tokens[i++]);
                    path.AddCurveToPoint(new PointF(x3, y3), new PointF(x1, y1), new PointF(x2, y2));
                }
                else if (token.Equals("Z"))
                {
                    path.ClosePath();
                }
            }
            return path;
        }

        private void RefreshView()
        {
            this.InvokeOnMainThread(() => SetNeedsDisplay());
        }
    }
}
