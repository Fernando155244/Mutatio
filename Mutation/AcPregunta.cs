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
    [Activity(Label = "AcPregunta")]
    public class AcPregunta : Activity
    {
        //Lista de preguntas fijas
        private String[] ds = { "¿Cuántos han sido beneficiados?",
            "Directorio", "¿Cuanto tarda la transición?",
            "¿Tengo que tener casa alla antes de hacer la certificación?",
            "¿Me regalan el departamento?",
            "¿Ustedes pagan la mudanza?" };
        private String[] dsLegend = {"Error",
            "Error",
            "La transición tardara lo que tenga que tardar",
            "Si y Regalarle una al programador de esta app",
            "No",
            "Si, pero con derecho a romper sus vasos"};
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //Variable para ingresar la posición anterior y posición de la lista
            int id = this.Intent.GetIntExtra("id", 0);
            // Create your application here

            SetContentView(Resource.Layout.Pregunta);
            //Index y definición de logo
            ImageView imgLogo = this.FindViewById<ImageView>(Resource.Id.Logo);
            imgLogo.SetImageResource(Resource.Drawable.Logo);

            //Indexados
            TextView Titulo = this.FindViewById<TextView>(Resource.Id.lblPreguntasTitulo);
            TextView Texto = this.FindViewById<TextView>(Resource.Id.txtPreguntasDatos);
            if (id > 1)
            {
                Titulo.Text = ds[id];
                Texto.Text = dsLegend[id];

            }
            else
            {
                Titulo.Text = "Error";
            }
        }
    }
}