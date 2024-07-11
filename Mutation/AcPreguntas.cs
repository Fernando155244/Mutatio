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
        private String[] ds = 
        {
            "¿Cuántos han sido beneficiados?",
            "¿La página https://cambiosinterestatales.sep.gob.mx es para Cambios dentro del mismo Estado?", 
            "¿Hay diferencia entre el Proceso de Cambios y en el Proceso de Permutas de Adscripción de Estado a Estado?", 
            "¿El participar en los procesos garantiza que mi cambio o permuta sea autorizado?", 
            "¿En qué consiste el Proceso de Cambios de Adscripción de Estado a Estado?", 
            "¿En qué consiste el Proceso de Permutas de Adscripción de Estado a Estado?", 
            "¿Puedo elegir la ubicación en el Proceso de Cambios o Permutas de Adscripción de Estado a Estado?", 
            "¿Es posible que el sistema me registre en el proceso incorrecto, es decir, que inicie mi registro en cambios y me pase a permutas?", 
            "Me equivoque en el registro, seleccione incorrectamente Cambios cuando quiero solicitar Permuta o viceversa, ¿Qué puedo hacer para corregir?", 
            "No tengo el número de solicitud ya que no concluí el registro, ¿Cómo lo obtengo?", "¿No encuentro mi categoría en el combo del nivel educativo en el que me registré?", 
            "¿Cómo consulto o como se cuál es mi categoría?", 
            "Ya consulté mi categoría, revisando mi talón de pago y el Sistema me indica que no se encontraron resultados, ¿qué procede?", 
            "No puedo imprimir, ya que me aparece que no tengo nombramientos registrados, ¿qué hago?", 
            "Descargue mi solicitud, pero está en blanco ¿cómo lo soluciono?", "No ubico los datos para registrar mi unidad y subunidad, ¿Cómo o de donde los obtengo?", 
            "¿Participo en los Procesos de Cambios y Permutas de Adscripción de Estado a Estado solo con el registro?",
            "¿Que documentos debo anexar a mi solicitud?",
            "¿En dónde entrego mi solicitud y documentos?",
            "No me llega el correo de registro, ¿Qué pasa en este caso, como lo obtengo?",
            "Deseo cancelar mi participación en el Proceso de Cambios de Adscripción de Estado a Estado ¿Qué debo hacer?",
            "Deseo cancelar mi participación en el Proceso de Permutas de Adscripción de Estado a Estado ¿Qué debo hacer?"

        };
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