using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Icu.Text.Transliterator;

namespace Mutation
{
    [Activity(Label = "AcPreguntas")]
    /*En esta pantalla tenemos la lista de preguntas frecuentes que podría hacer el usuario*/
    public class AcPreguntas : Activity
    {
        //Lista de preguntas fijas
        private String[] ds = { "¿Cuántos han sido beneficiados?", "Directorio", "¿Cuanto tarda la transición?", "¿Tengo que tener casa alla antes de hacer la certificación?", "¿Me regalan el departamento?", "¿Ustedes pagan la mudanza?" };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Preguntas);

            //Index y definición de logo
            ImageView imgLogo = this.FindViewById<ImageView>(Resource.Id.Logo);
            imgLogo.SetImageResource(Resource.Drawable.Logo);

            //Indexados
            ListView ListaPreguntas = this.FindViewById<ListView>(Resource.Id.lsPreguntaTtablas);

            //Mandamos a llenar la lista
            ListaPreguntas.Adapter = new Rellenar(this,ds);

            //Mandamos la función por si presionan en una valor
            ListaPreguntas.ItemClick += ListaPreguntas_ItemClick;
        }

        private void ListaPreguntas_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //Si la posición selecciónada es 0 mandamos al usuario a la pantalla de graficas
            if (e.Position == 0)
            {
                StartActivity(typeof(AcGraficas));
            }//Si la repuesta es 1 lo mandamos al directorio
            else if (e.Position == 1)
            {
                StartActivity(typeof(AcDirectorio));
            }else //Si no es ni uno ni el otro manmos a una pregutna a responder
            {
                Intent Pregunta = new Intent(this, typeof(AcPregunta));
                Pregunta.PutExtra("id", e.Position);
                StartActivity(Pregunta);
            }

        }
    }

    internal class Rellenar : BaseAdapter
    {
        private AcPreguntas acPreguntas;
        private string[] ds;

        public Rellenar(AcPreguntas acPreguntas, string[] ds)
        {
            this.acPreguntas = acPreguntas;
            this.ds = ds;
        }

        public override int Count
        {
            get
            {
                return ds.Length;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return "";
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View vista = convertView;
            //Declaración de vista
            if (vista == null)
            {
                //Aqui aparte de declarar los valores basicos declaramos el uso del diseño TabEmpresa para la fila
                vista = acPreguntas.LayoutInflater.Inflate(Resource.Layout.ModPreguntas, null);
            }
            TextView Pregunta = vista.FindViewById<TextView>(Resource.Id.PreguntaTexto);
            Pregunta.Text = ds[position];
            return vista;
        }
    }
}