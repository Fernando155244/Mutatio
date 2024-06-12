using Android.App;
using Android.Content;
using Android.Content.PM;
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
    [Activity(Label = "Mutatio", Theme = "@style/TemaSplash", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize)]
    public class AcAplash : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here

            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}