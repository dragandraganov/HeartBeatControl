using CoreGraphics;
using System;
using System.Threading.Tasks;
using UIKit;
using AnimatedControl.Controls;

namespace AnimatedControl
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            BounceAnimationControl customControl = new BounceAnimationControl(new CGRect(50, 50, 250, 250));
            this.View.AddSubview(customControl);

            Task.Delay(2000).ContinueWith(_ => InvokeOnMainThread(() => customControl.Color = UIColor.Blue.CGColor));
            Task.Delay(4000).ContinueWith(_ => InvokeOnMainThread(() => customControl.Duration = 0.3));
            Task.Delay(6000).ContinueWith(_ => InvokeOnMainThread(() => customControl.MaxScale = 0.6f));
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }
}