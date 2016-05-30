using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Views.Animations;
using Android.Graphics.Drawables;
using AndroidHeartBeater.Controls;
using System.Threading.Tasks;
using Android.Graphics;
using System.Collections.Generic;
using System.Timers;

namespace AndroidHeartBeater
{
    [Activity(Label = "AndroidHeartBeater", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private BounceAnimationControl _heartBeater;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            _heartBeater = FindViewById<BounceAnimationControl>(Resource.Id.heartBeater);
            Task.Delay(2000).ContinueWith(_ =>
            {
                _heartBeater.ShapeColor = Color.Green;
            });
            Task.Delay(4000).ContinueWith(_ =>
            {
                _heartBeater.MaxScale = 1.3f;
            });
            Task.Delay(5000).ContinueWith(_ =>
            {
                _heartBeater.ShapeColor = Color.Blue;
            });
            Task.Delay(10000).ContinueWith(_ =>
            {
                _heartBeater.MaxScale = 1.1f;
            });
            Task.Delay(3000).ContinueWith(_ =>
            {
                _heartBeater.AnimationDuration = 1000;
            });
        }
    }
}

