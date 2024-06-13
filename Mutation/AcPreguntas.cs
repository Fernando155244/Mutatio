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
    [Activity(Label = "AcPreguntas")]
    public class AcPreguntas : Activity
    {
        private String[] ds = { "¿Cuanto tarda la transición?", "¿Tengo que tener casa alla antes de hacer la certificación?", "¿Me regalan el departamento?", "¿Ustedes pagan la mudanza?", "¿Cuántos han sido beneficiados?", "Directorio" };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Preguntas);

            //Indexados
            ListView ListaPreguntas = this.FindViewById<ListView>(Resource.Id.lsPreguntaTtablas);

            ListaPreguntas.Adapter = new Rellenar(this,ds);
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