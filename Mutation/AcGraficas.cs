using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxyPlot.Xamarin.Android;
using System.Runtime.CompilerServices;

namespace Mutation
{
    [Activity(Label = "AcGraficas")]
    /*En esta pantalla podemos ver en graficas cuantos han sido veneficiados en este sistema*/
    public class AcGraficas : Activity
    {
        String[] ds = {"CDMX", "EDOMEX", "Veracruz" };
Int32[] nds = {300,100,400};
        protected override void OnCreate(Bundle savedInstanceState)
        {
            /*Variables para pruebas*/
            
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Graficas);
            //Indexados
            PlotView Grafica = this.FindViewById<PlotView>(Resource.Id.GrafGraficas);
            ListView ListaPreguntas = this.FindViewById<ListView>(Resource.Id.lisGraficas);
            Spinner spTipo = this.FindViewById<Spinner>(Resource.Id.spGraficasTipoSolicitud);
            Spinner spEstatus = this.FindViewById<Spinner>(Resource.Id.spGraficasEstatus);
            Spinner spReferencias = this.FindViewById<Spinner>(Resource.Id.spGraficasReferencia);

            Grafica.Model = Estados();
            //Mandamos a llenar la lista
            ListaPreguntas.Adapter = new RellenarGraficas(this,ds,nds);
            
            //Función automatica del spinner
            LlenarTipo();
            LlenarEstatus();
            LlenarGraficas();

        }
        private void LlenarTipo()
        {
            Spinner spTipo = this.FindViewById<Spinner>(Resource.Id.spGraficasTipoSolicitud);
            ArrayAdapter spInteres = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem);

            //Agregamos los valores de la tabla al arrayAdapter 
            spInteres.Add("Todos");
            spInteres.Add("Cambios");
            spInteres.Add("Permutas");
            //Ingresamos el adapter al spinner
            spTipo.Adapter = spInteres;
        }
        private void LlenarEstatus()
        {
            Spinner spTipo = this.FindViewById<Spinner>(Resource.Id.spGraficasEstatus);
            ArrayAdapter spInteres = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem);

            //Agregamos los valores de la tabla al arrayAdapter 
            spInteres.Add("Todos");
            spInteres.Add("R");
            spInteres.Add("C");
            //Ingresamos el adapter al spinner
            spTipo.Adapter = spInteres;
        }
        private void LlenarGraficas()
        {
            Spinner spTipo = this.FindViewById<Spinner>(Resource.Id.spGraficasReferencia);
            ArrayAdapter spInteres = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem);

            //Agregamos los valores de la tabla al arrayAdapter 
            spInteres.Add("Todos");
            spInteres.Add("Estado");
            spInteres.Add("Nivel Educativo");
            //Ingresamos el adapter al spinner
            spTipo.Adapter = spInteres;
        }
        private PlotModel Estados()
        {
            CategoryAxis ejex = new CategoryAxis();
            LinearAxis ejey = new LinearAxis();
            //al valor x le damos los nombres de los valores
            ejex.Position = AxisPosition.Bottom;
            //Le damos valores minimo y maximo
            ejey.Minimum = 0;
            ejey.Maximum = 600;
            ejey.Position = AxisPosition.Left;
            //Creamos los valores de las graficas
            ColumnSeries s1 = new ColumnSeries();

            for (int i = 0; i <3; i++)
            {
                ejex.Labels.Add(ds[i]);
                s1.Items.Add(new ColumnItem(nds[i]));
            }
            //Creamos el modelo de graficas
            PlotModel m1 = new PlotModel();
            //Le damos titulo a la grafica
            m1.Title = "Productos";
            //Le damos los formatos de x e y
            m1.Axes.Add(ejey);
            m1.Axes.Add(ejex);
            //Le damos los valores a la grafica
            m1.Series.Add(s1);
            //Regresamos los valores ingresados 
            return m1;
        }
    }

    internal class RellenarGraficas : BaseAdapter
    {
        private AcGraficas acGraficas;
        private string[] ds;
        private int[] nds;

        public RellenarGraficas(AcGraficas acGraficas, string[] ds, int[] nds)
        {
            this.acGraficas = acGraficas;
            this.ds = ds;
            this.nds = nds;
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
                vista = acGraficas.LayoutInflater.Inflate(Resource.Layout.ModGraficos, null);
            }
            TextView Estado = vista.FindViewById<TextView>(Resource.Id.txtTabEstado);
            TextView Numeros = vista.FindViewById<TextView>(Resource.Id.txtTabNumeros);
            Estado.Text = ds[position];
            Numeros.Text = nds[position].ToString();
            return vista;
        }
    }
}