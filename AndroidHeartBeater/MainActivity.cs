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
      
        }
    }
}

