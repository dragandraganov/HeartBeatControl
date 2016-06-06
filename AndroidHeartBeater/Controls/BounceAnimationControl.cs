using System;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Util;
using static Android.Resource;
using Android.Views.Animations;
using Java.Util.Jar;
using static Android.Views.Animations.Animation;
using Android.Animation;
using System.Diagnostics;

namespace AndroidHeartBeater.Controls
{
    public class BounceAnimationControl : View, IAnimationListener
    {
        private static Path DrawPath = CreatePath();
        private static double ShapeWidth = 5;
        private Android.Views.Animations.Animation _animation;
        private float _lastScale;

        private Context _context;

        public BounceAnimationControl(Context context) : base(context)
        {
            Init(context);
        }

        public BounceAnimationControl(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Init(context);
        }

        public BounceAnimationControl(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Init(context);
        }

        private Android.Graphics.Color _shapeColor;
        public Android.Graphics.Color ShapeColor
        {
            get
            {
                return _shapeColor;
            }
            set
            {
                _shapeColor = value;
                RequestLayout();
                Invalidate();
            }
        }

        private long _animationDuration;
        public long AnimationDuration
        {
            get
            {
                return _animationDuration;
            }
            set
            {
                _animationDuration = value;
                RequestLayout();
                Invalidate();
            }
        }

        private float _maxScale;
        public float MaxScale
        {
            get
            {
                return _maxScale;
            }
            set
            {
                _maxScale = value;

            }
        }

        protected override void OnDraw(Canvas canvas)
        {
            _animation.Duration = AnimationDuration;
            using (Path p = new Path(DrawPath))
            {
                using (Paint paint = new Paint()
                {
                    Color = ShapeColor,
                    AntiAlias = true
                })
                {
                    double ratio = canvas.Width / ShapeWidth;
                    float floatRatio = Convert.ToSingle(ratio);
                    using (Matrix matrix = new Matrix())
                    {
                        matrix.SetScale(floatRatio, floatRatio);
                        p.Transform(matrix);
                        paint.SetStyle(Paint.Style.Stroke);
                        paint.StrokeWidth = 1;
                        canvas.DrawPath(p, paint);
                        paint.SetStyle(Paint.Style.Fill);
                        canvas.DrawPath(p, paint);
                    }
                }
            }
        }

        private static Path CreatePath()
        {
            Path path = new Path();

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
                    path.MoveTo(x, y);
                }
                else if (token.Equals("L"))
                {
                    float x = float.Parse(tokens[i++]);
                    float y = float.Parse(tokens[i++]);
                    path.LineTo(x, y);
                }
                else if (token.Equals("C"))
                {
                    float x1 = float.Parse(tokens[i++]);
                    float y1 = float.Parse(tokens[i++]);
                    float x2 = float.Parse(tokens[i++]);
                    float y2 = float.Parse(tokens[i++]);
                    float x3 = float.Parse(tokens[i++]);
                    float y3 = float.Parse(tokens[i++]);
                    path.CubicTo(x1, y1, x2, y2, x3, y3);
                }
                else if (token.Equals("Z"))
                {
                    path.Close();
                }
            }
            return path;
        }

        private void Init(Context context)
        {
            _context = context;
            InitAnimation();
        }

        private void InitAnimation()
        {
            if (MaxScale == 0)
            {
                MaxScale = 1;
                _lastScale = 1;
            }

            if (ShapeColor == default(Android.Graphics.Color))
            {
                ShapeColor = Android.Graphics.Color.Red;
            }

            if (AnimationDuration == 0)
            {
                AnimationDuration = 1000;
            }
            _animation = new ScaleAnimation(MaxScale, 0.7f, MaxScale, 0.7f, Android.Views.Animations.Dimension.RelativeToSelf, 0.5f, Android.Views.Animations.Dimension.RelativeToSelf, 0.5f);
            _animation.Interpolator = new AnticipateInterpolator();
            _animation.RepeatCount = int.MaxValue;
            _animation.RepeatMode = RepeatMode.Reverse;
            _animation.SetAnimationListener(this);

            this.StartAnimation(_animation);

        }

        public void OnAnimationEnd(Android.Views.Animations.Animation animation)
        {
            return;
        }

        public void OnAnimationRepeat(Android.Views.Animations.Animation animation)
        {
            if (MaxScale != _lastScale)
            {
                _lastScale = MaxScale;
                InitAnimation();
                RequestLayout();
                Invalidate();
            };
        }

        public void OnAnimationStart(Android.Views.Animations.Animation animation)
        {
            return;
        }
    }
}