using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mutation
{
    [Activity(Label = "acSplash")]
    public class acSplash : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Splash);
            //Indexamos
            ImageView img = this.FindViewById<ImageView>(Resource.Id.imgSplash);
            img.SetImageResource(Resource.Drawable.LogodeMilVeinitcuatro);
            System.Threading.Thread.Sleep(3000);
            Finish();
        }
    }
}